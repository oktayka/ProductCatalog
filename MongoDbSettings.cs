namespace ProductCatalog
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string ProductCollectionName { get; set; }
    }
}