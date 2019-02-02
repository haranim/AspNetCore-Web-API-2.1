using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casino.Model
{
    public class Session
    {
        public ObjectId Id { get; set; }

        [BsonElement("GameId")]
        public ObjectId GameId { get; set; }

        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }

        [BsonElement("Gamename")]
        public string GameName { get; set; }

        [BsonElement("Username")]
        public string UserName { get; set; }

        public string Url{ get; set; }

    }
}
