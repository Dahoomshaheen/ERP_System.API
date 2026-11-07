using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System.Domain.Entities
{
    public class SalesDetail
    {
        public int Id { get; set; }
        public int SalesHeaderId {  get; set; }
        public SalesHeader? SalesHeader { get; set; }

        public int ProductId { get; set; }
         public Product? Product { get; set; }
        public int Quantity { get; set; }
         public decimal UnitPrice { get; set; }
         public decimal CostPrice { get; set; }



    }
}
