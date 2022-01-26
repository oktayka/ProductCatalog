using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public class ProductService<T> : IProductService<T> where T : Product, IProduct
    {
        private IMongoCollection<T> _productCollection;
        public ProductService(IMongoCollection<T> productCollection)
        {
            _productCollection = productCollection;
        }
        public async Task CreateAsync(T product)
        {
            ((Product)product).Type = typeof(T).Name;
            await _productCollection.InsertOneAsync(product);
        }

        public async Task<List<T>> GetAllAsync()
        {
            // {Type : "Book"}
            return await _productCollection.Find($"{{Type: \"{typeof(T).Name}\"}}").ToListAsync();
        }

        public async Task<T> GetAsync(string id)
        {
            return await _productCollection.Find($"{{_id: \"{id}\"}}").FirstOrDefaultAsync();            
        }
    }
}