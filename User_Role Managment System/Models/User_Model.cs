using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Role_Managment_System.Models
{
    public class User_Model
    {
        [Key]
        public int user_Id { get; set; }
        public string User_FirstName { get; set; }
        public string User_Lastname { get; set; }
        public string Usermail { get; set; }
        public string UserAddress { get; set; }
        public string User_contact { get; set; }
        public byte[] ProfiePic { get; set;}


    }
}
