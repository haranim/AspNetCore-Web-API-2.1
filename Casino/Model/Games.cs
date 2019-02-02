using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Casino.Model
{
    public class Games
    {
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; }

        [BsonElement("Thumnail")]
        public string Thumnail { get; set; }

        [BsonElement("Devices")]
        public string[] Devices { get; set; }

        [BsonElement("Collection")]
        public string[] Collection { get; set; }

    }

}
