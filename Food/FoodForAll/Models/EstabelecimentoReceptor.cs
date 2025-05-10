using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class EstabelecimentoReceptor
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Nome { get; set; }
        [Required, MaxLength(100)]
        public string? Responsavel { get; set; }
        [Required, MaxLength(200)]
        public string? Endereco { get; set; }
        [MaxLength(20)]
        public string? Telefone { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [MaxLength(50)]
        public string? RegistroGoverno { get; set; }
        public bool Aprovada { get; set; }
        public int? RefeicoesDiarias { get; set; }
    }
} 