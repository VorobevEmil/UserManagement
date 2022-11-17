using System.ComponentModel.DataAnnotations;

namespace UserManagement.Shared.Models.Account
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; } = default!;
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = default!;
    }
}
