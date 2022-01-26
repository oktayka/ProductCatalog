using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public interface IProductService<T> where T : Product, IProduct
    {
         Task CreateAsync(T product);

         Task<List<T>> GetAllAsync();

         Task<T> GetAsync(string id);
    }
}