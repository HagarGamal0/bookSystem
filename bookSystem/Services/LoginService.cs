using System.ComponentModel.DataAnnotations;

namespace bookSystem.Services
{
    public class LoginService
    {

        [Required (ErrorMessage ="*")]
        public string User_name { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display (Name ="remember me")]
        public bool remember_me { get; set; }

    }
}
