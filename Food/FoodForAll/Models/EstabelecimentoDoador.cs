using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class EstabelecimentoDoador
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? NomeFantasia { get; set; }
        [Required, MaxLength(18)]
        public string? CNPJ { get; set; }
        [Required, MaxLength(200)]
        public string? Endereco { get; set; }
        [MaxLength(20)]
        public string? Telefone { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [MaxLength(100)]
        public string? Categoria { get; set; }
    }
} 