using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductCatalog.Models
{
    public class Product : IProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }
    }
}