using System.ComponentModel.DataAnnotations;

namespace GameZone.viewmodel
{
    public class LoginUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool rememberMe { get; set; }=false;
    }
}
