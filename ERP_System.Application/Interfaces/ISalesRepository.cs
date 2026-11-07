using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System.Application.Interfaces
{
    public interface ISalesRepository
    {
         Task AddInvoiceAsync(SalesHeader invoice);
    }
}
