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
    public class GamesService : ServiceBase, IGamesService
    {
        private IMongoCollection<Games> _games;
        private IMongoCollection<GamesCollection> _gamesCollection;
         

        public GamesService(IConfiguration config) : base(config)
        {
            
        }

        public List<Games> GetGames(int skip = 0, int take = 10)
        {
            //_games = _database.GetCollection<Games>("Games");
            var _context = _database.GetCollection<Games>("Games").AsQueryable<Games>();

            var games = _context.Skip(skip).Take(take).ToList();

            return games;
        }

        public List<GamesCollection> GetGamesCollection()
        {
            _gamesCollection = _database.GetCollection<GamesCollection>("GamesCollection");
            var list = _gamesCollection.Find(collection => true).ToList();
            _games = _database.GetCollection<Games>("Games");

            foreach (var item in list)
            {
               item.Games = _games.Find(game => game.Collection.Contains(item.Id)).ToList().Select(m => m.Id.ToString()).ToArray();
            }

            return list;
        }

        public Games GetGame(string id)
        {
            var docId = new ObjectId(id);
            _games = _database.GetCollection<Games>("Games");
            return _games.Find<Games>(game => game.Id == docId).FirstOrDefault();
        }
    }
}
