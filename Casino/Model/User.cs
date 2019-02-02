using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Casino.Model
{
    public class User
    {
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("UserName")]
        public string Username { get; set; }

        [Required]
        [BsonElement("Password")]
        public string Password { get; set; }

        public string Token { get; set; }

    }
}