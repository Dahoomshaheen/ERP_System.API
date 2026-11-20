using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepo;
        private readonly IGenericRepository<Product> _productRepo;
        
        public SalesController(ISalesRepository salesRepo, IGenericRepository<Product> productRepo)
        {
            _salesRepo = salesRepo;
            _productRepo = productRepo;
        }
       
        [HttpGet]
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _salesRepo.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(SalesHeader invoice)
        {
            // 1. Validate invoice has items
            if (invoice.detail == null || !invoice.detail.Any())
            {
                return BadRequest(new { Message = "The invoice must contain at least one item." });
            }

            // 2. Validate that all related Products exist in the database
            foreach (var item in invoice.detail)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null)
                {
                    return BadRequest(new { Message = $"Cannot save invoice: Product with ID {item.ProductId} does not exist." });
                }
            }

            // 3. Calculate total & set timestamp
            invoice.SalesDate = DateTime.UtcNow;
            invoice.TotalAmount = invoice.detail.Sum(d => d.Quantity * d.UnitPrice);

            await _salesRepo.AddInvoiceAsync(invoice);

            return StatusCode(201, new
            {
                Message = "The invoice has been saved successfully!",
                InvoiceId = invoice.Id,
                Total = invoice.TotalAmount
            });
        }
    }
}