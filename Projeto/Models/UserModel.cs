using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class UserModel
    {
        //public int id { get; set; }
        //public string username { get; set; }
        //public string password { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
