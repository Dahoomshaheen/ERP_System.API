using System.ComponentModel.DataAnnotations;

namespace ERP_System.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters.")]
        public string Password { get; set; }

        //  "Admin" OR "Cashier"
        public string Role { get; set; } = "Cashier";
    }
}