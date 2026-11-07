using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IGenericRepository<Warehouse> _WarehouseRepo;

        // The constructor that receives the repository
        public WarehouseController(IGenericRepository<Warehouse> WarehouseRepo)
        {
            _WarehouseRepo = WarehouseRepo;
        }

        // Screen for retrieving all warehouses
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var Warehouse = await _WarehouseRepo.GetAllAsync();
            return Ok(Warehouse);
        }

        // Screen for adding a new warehouse
        [HttpPost]
        public async Task<IActionResult> AddUnit(Warehouse Warehouse)
        {
            await _WarehouseRepo.AddAsync(Warehouse);
            return Ok(new { Message = "The Warehouse has been added successfully" });
        }
    }
}