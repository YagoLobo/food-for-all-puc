using System.ComponentModel.DataAnnotations;

namespace FoodforAll.Models.ViewModels
{
    public enum TipoLogin
    {
        Doador,
        Receptor
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public TipoLogin Tipo { get; set; }
    }
}
