using System.ComponentModel.DataAnnotations;

namespace User_Role_Managment_System.Models
{
    public class Role_Model
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
