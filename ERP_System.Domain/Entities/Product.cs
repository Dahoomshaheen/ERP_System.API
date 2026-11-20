using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ERP_System.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "English name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "English name must be between 2 and 100 characters.")]
        public string EnName { get; set; }

        [Required(ErrorMessage = "Arabic name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Arabic name must be between 2 and 100 characters.")]
        public string ArName { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Selling price must be greater than 0.")]
        public decimal Price { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than 0.")]
        public decimal CostPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Unit.")]
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Group.")]
        public int GroupId { get; set; }
        public Group? Group { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Warehouse.")]
        public int Warehouseid { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}