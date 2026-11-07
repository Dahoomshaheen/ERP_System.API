using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGenericRepository<Group> _GroupRepo;

        //   The constructor that receives the repository
        public GroupController(IGenericRepository<Group> GroupRepo)
        {
            _GroupRepo = GroupRepo;
        }

        // Screen for retrieving all warehouses
        [HttpGet]
        public async Task<IActionResult> GetAllUnits()
        {
            var Group = await _GroupRepo.GetAllAsync();
            return Ok(Group);
        }

        //  Screen for adding a new warehouse
        [HttpPost]
        public async Task<IActionResult> AddUnit(Group Group)
        {
            await _GroupRepo.AddAsync(Group);
            return Ok(new { Message = "The Group has been added successfully" });
        }
    }
}