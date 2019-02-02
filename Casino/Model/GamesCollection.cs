using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Casino.Model
{
    public class GamesCollection
    {
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Games")]
        public string[] Games { get; set; }

        [BsonElement("SubCollections")]
        public string SubCollections { get; set; }

    }
}
