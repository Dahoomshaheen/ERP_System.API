using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP_System.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Group name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Group name must be between 2 and 50 characters.")]
        public string Name { get; set; }
    }
}
