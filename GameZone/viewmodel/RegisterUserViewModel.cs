using System.ComponentModel.DataAnnotations;

namespace GameZone.viewmodel
{
    public class RegisterUserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword {  get; set; }
        public string Country {  get; set; }
    }
}
