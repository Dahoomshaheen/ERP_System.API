using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP_System.Domain.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Warehouse name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Warehouse name must be between 2 and 50 characters.")]
        public string Name { get; set; }
    }
}