using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System.Domain.Entities
{
    public class SalesHeader
    {
        public int Id { get; set; }

        public DateTime SalesDate { get; set; } = DateTime.UtcNow;

        [Range(0, double.MaxValue, ErrorMessage = "Total amount cannot be negative.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Invoice details are required.")]
        [MinLength(1, ErrorMessage = "The invoice must contain at least one item.")]
        public List<SalesDetail> detail { get; set; } = new List<SalesDetail>();
    }
}
