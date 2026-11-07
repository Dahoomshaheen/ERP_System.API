using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System.Domain.Entities
{
    public class SalesHeader
    { public int Id { get; set; }
        public DateTime  SalesDate { get; set; }
        public decimal TotalAmount { get; set; }

        public List<SalesDetail> detail { get; set; }=new List<SalesDetail>();
    }
}
