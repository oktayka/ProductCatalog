using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductService<Book> _bookService;
        private IProductService<Phone> _phoneService;
        public ProductController(IProductService<Book> bookService, IProductService<Phone> phoneService)
        {
            _bookService = bookService;
            _phoneService = phoneService;
        }


        [HttpPost]
        [Route("books/add")]
        public async Task CreateAsync(Book book)
        {
            await _bookService.CreateAsync(book);
        }

        [HttpPost]
        [Route("phones/add")]
        public async Task CreateAsync(Phone phone)
        {
            await _phoneService.CreateAsync(phone);
        }

        [HttpGet]
        [Route("phones")]
        public async Task<List<Phone>> GetAllPhonesAsync()
        {
            return await _phoneService.GetAllAsync();
        }

        
        [HttpGet]
        [Route("books")]
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookService.GetAllAsync();
        }
    }
}