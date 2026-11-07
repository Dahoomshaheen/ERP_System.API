using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IGenericRepository<Unit> _unitRepo;

        // The constructor that receives the repository
        public UnitsController(IGenericRepository<Unit> unitRepo)
        {
            _unitRepo = unitRepo;
        }

        //  Screen for retrieving all units
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var units = await _unitRepo.GetAllAsync();
            return Ok(units);
        }

        // 2. Screen for adding a new warehouse
        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            await _unitRepo.AddAsync(unit);
            return Ok(new { Message = "The unit has been added successfully" });
        }
    }
}