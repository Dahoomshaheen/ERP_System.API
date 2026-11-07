using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERP_System.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
    
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>>GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Unit)
                .Include(p => p.Group)
                .Include(p => p.Warehouse)
                .ToListAsync();

        }


        public async Task<Product>GetProductByIdAsync(int id)
        {
            return await _context.Products
              
                    
                .Include(p => p.Unit)
                .Include(p => p.Group)
                .Include(p => p.Warehouse) 
                .FirstOrDefaultAsync(p=>p.Id ==id);
        }





    }
}



