using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Unifying protection with the rest of the controllers
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IGenericRepository<Product> _genericRepo;
        private readonly IGenericRepository<Unit> _unitRepo;
        private readonly IGenericRepository<Group> _groupRepo;
        private readonly IGenericRepository<Warehouse> _warehouseRepo;
        private readonly ISalesRepository _salesRepo;

        // Injecting required repositories via DI
        public ProductsController(
            IProductRepository productRepo,
            IGenericRepository<Product> genericRepo,
            IGenericRepository<Unit> unitRepo,
            IGenericRepository<Group> groupRepo,
            IGenericRepository<Warehouse> warehouseRepo,
            ISalesRepository salesRepo)
        {
            _productRepo = productRepo;
            _genericRepo = genericRepo;
            _unitRepo = unitRepo;
            _groupRepo = groupRepo;
            _warehouseRepo = warehouseRepo;
            _salesRepo = salesRepo;
        }

        // Get all products with related entities (Unit, Group, Warehouse)
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepo.GetAllProductsAsync();
            return Ok(products);
        }

        // Get single product by ID with clear 404 message
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _genericRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} was not found." });
            }

            return Ok(product);
        }

        // Add new product with Relational Validation & Clear Return Message
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            // 1. Business Logic Validation
            if (product.Price < product.CostPrice)
            {
                return BadRequest(new { Message = "Selling price cannot be less than the cost price." });
            }

            // 2. Validate Related Entities (Foreign Keys) exist before saving
            var unit = await _unitRepo.GetByIdAsync(product.UnitId);
            if (unit == null)
            {
                return BadRequest(new { Message = $"Invalid UnitId: Unit with ID {product.UnitId} does not exist." });
            }

            var group = await _groupRepo.GetByIdAsync(product.GroupId);
            if (group == null)
            {
                return BadRequest(new { Message = $"Invalid GroupId: Group with ID {product.GroupId} does not exist." });
            }

            var warehouse = await _warehouseRepo.GetByIdAsync(product.Warehouseid);
            if (warehouse == null)
            {
                return BadRequest(new { Message = $"Invalid WarehouseId: Warehouse with ID {product.Warehouseid} does not exist." });
            }

            // 3. Save & Return standardized response
            await _genericRepo.AddAsync(product);

            return StatusCode(201, new
            {
                Message = "The product has been added successfully!",
                ProductId = product.Id,
                Data = product
            });
        }

        // Safe Delete: Prevent deleting products linked to existing sales invoices
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _genericRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} was not found." });
            }

            // Check relation with Sales before deleting
            var invoices = await _salesRepo.GetAllInvoicesAsync();
            bool isLinkedToSales = invoices.Any(inv => inv.detail != null && inv.detail.Any(d => d.ProductId == id));

            if (isLinkedToSales)
            {
                return BadRequest(new { Message = "Cannot delete this product because it is already used in existing sales invoices." });
            }

            await _genericRepo.DeleteAsync(product); 
            return Ok(new { Message = $"Product '{product.EnName}' has been deleted successfully." });
        }
    }
}