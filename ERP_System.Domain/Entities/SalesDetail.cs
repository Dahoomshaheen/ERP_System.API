using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ERP_System.Domain.Entities
{
    public class SalesDetail
    {
        public int Id { get; set; }

        public int SalesHeaderId { get; set; }

        [JsonIgnore]
        public SalesHeader? SalesHeader { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Product.")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost price cannot be negative.")]
        public decimal CostPrice { get; set; }
    }
}