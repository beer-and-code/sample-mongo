using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleMongo.Api.Models
{
    public abstract class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }

        public void LoadId(string id) 
        {
            Id = id;
        }
    }
}