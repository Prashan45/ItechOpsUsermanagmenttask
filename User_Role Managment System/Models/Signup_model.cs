using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Role_Managment_System.Models
{
    public class Signup_model
    {
        [Key]
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Password { get; set; }
        public int? RoleID { get; set; }
        public byte[]? Image { get; set; }

        [ForeignKey("RoleID")] 
        public Role_Model? Role { get; set; }
    }
}
