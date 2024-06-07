using System.ComponentModel.DataAnnotations;

namespace User_Role_Managment_System.Models
{
    public class Login_Model
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}