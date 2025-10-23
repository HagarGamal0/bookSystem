using System.ComponentModel.DataAnnotations;

namespace bookSystem.Services
{
    public class RegisterService
    {
        public string User_name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [Display(Name ="confirm password")]
        [DataType(DataType.Password)]
        public string Conffirm_password  { get; set; }

        public string Address { get; set; }
    }
}
