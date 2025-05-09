using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodforAll.Models
{
    [Table("EstabelecimentoDoador")]
    public class EstabelecimentoDoador
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o CNPJ!")]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o nome fantasia!")]
        public required string? NomeFantasia { get; set; }
        [Required]
        public bool? TransporteProprio { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
