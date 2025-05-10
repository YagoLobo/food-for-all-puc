using System.ComponentModel.DataAnnotations;

namespace FoodForAll.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string UserType { get; set; } // "Supermercado" ou "CozinhaSolidaria"
    }
} 