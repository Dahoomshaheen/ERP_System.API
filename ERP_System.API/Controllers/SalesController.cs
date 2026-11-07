using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepo;

        public SalesController(ISalesRepository salesRepo)
        {
            _salesRepo = salesRepo;
        }

        // Invoice Save Screen (Header + Details)
        [HttpPost]
        public async Task<IActionResult> CreateInvoice(SalesHeader invoice)
        {
            // Check that the invoice contains items.
            if (invoice.detail == null || !invoice.detail.Any())
            {
                return BadRequest("The invoice must contain at least one item.");
            }

            //Calculate the total invoice amount
            invoice.TotalAmount = invoice.detail.Sum(d => d.Quantity * d.UnitPrice);

            // Save the invoice to the database
            await _salesRepo.AddInvoiceAsync(invoice);

            return Ok(new
            {
                Message = "The invoice has been saved successfully.!",
                InvoiceId = invoice.Id,
                Total = invoice.TotalAmount
            });
        }
    }
}