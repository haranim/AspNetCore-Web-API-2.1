using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Casino.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Casino.Model;
using Casino.Helpers;

namespace Casino.Services
{

    public class UserService : ServiceBase, IUserService
    {
        private readonly IMongoCollection<User> _users;

        private readonly AppSettings _appSettings;

        public UserService(IConfiguration config, IOptions<AppSettings> appSettings) : base(config)
        {
            _users = _database.GetCollection<User>("Users");
            _appSettings = appSettings.Value;
        }

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public User Get(string id)
        {
            var docId = new ObjectId(id);

            return _users.Find<User>(user => user.Id == docId).FirstOrDefault();
        }

        public User Register(User user)
        {
            _users.InsertOne(user); // passwords should not be saved as direct strings in Database, the have to be encrypted. 
            
            return user;    
        }

        public User Login(User request)
        {
            var user = _users.Find(u => u.Username == request.Username && u.Password == request.Password).First();

            if (user == null)
                return new User();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = tokenHandler.WriteToken(token);

            user.Password = "*****";
            return user;
        }



    }
}