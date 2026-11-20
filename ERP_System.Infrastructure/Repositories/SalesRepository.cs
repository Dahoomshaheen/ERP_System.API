using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.EntityFrameworkCore; 

namespace ERP_System.Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext _context;
        public SalesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesHeader>> GetAllInvoicesAsync()
        {
            return await _context.salesHeaders
                .Include(s => s.detail) 
                .ThenInclude(d => d.Product)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }
        public async Task AddInvoiceAsync(SalesHeader invoice)
        {
            await _context.salesHeaders.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

    }
}
