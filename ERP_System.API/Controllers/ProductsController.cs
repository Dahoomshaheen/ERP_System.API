using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IGenericRepository<Product> _genericRepo;

        // // Injecting required repositories via DI
        public ProductsController(IProductRepository productRepo, IGenericRepository<Product> genericRepo)
        {
            _productRepo = productRepo;
            _genericRepo = genericRepo;
        }

        // The screen for each item fetch will bring up the unit and group data
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();
            return Ok(products);
        }

        // Screen for adding a new item
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _genericRepo.AddAsync(product);
            return Ok(new { Message = "The Product has been added successfully!" });
        }
    }
}