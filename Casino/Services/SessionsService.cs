using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Casino.Model;
using Casino.Helpers;
using Casino.IServices;
using System.Web;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace Casino.Services
{
    public class SessionsService : ServiceBase, ISessionsService
    {

        private readonly IMongoCollection<Session> _sessions;
       

        public SessionsService(IConfiguration config, IOptions<AppSettings> appSettings) : base(config)
        {
            
            _sessions = _database.GetCollection<Session>("Sessions");
        }

        public Session CreateSession(Games game, User user)
        {
            // If user already has created a session, avoid creating new
            if (_sessions.Find(item => item.UserId == user.Id).Any())
                return null;

            var session = new Session
            {
                GameId = game.Id,
                GameName = game.Name,
                UserId = user.Id,
                UserName = user.Username,
                Url = game.Category + "/" + game.Name + "/path"
            };

            _sessions.InsertOne(session);

            return session;
        }
    }
}
