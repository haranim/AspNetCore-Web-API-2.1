using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Casino.Services
{
    public class ServiceBase
    {
        public MongoClient _client;
        public IMongoDatabase _database;

        public ServiceBase(IConfiguration config)
        {
            _client = new MongoClient(config.GetConnectionString("CasinoDb"));
            _database = _client.GetDatabase("CasinoDb");
        }
    }
}
