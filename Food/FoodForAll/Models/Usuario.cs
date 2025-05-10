using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public enum TipoUsuario
    {
        Admin,
        EstabelecimentoDoador,
        EstabelecimentoReceptor
    }

    public class Usuario
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public TipoUsuario TipoUsuario { get; set; }
        [Required, MaxLength(200)]
        public string Endereco { get; set; }
        [Required, MaxLength(50)]
        public string Contato { get; set; }
    }
} 