using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodforAll.Models
{
    [Table("EstabelecimentoReceptor")]
    public class EstabelecimentoReceptor
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o email!")]
        public required string? NomeReceptor { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o Resgistro do Governo!")]
        public string? RegistroGoverno { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar se esta aprovada no programa do governo!")]
        public bool? Aprovada { get; set; }

        public int? RefeicoesDiarias { get; set; }

        [Required(ErrorMessage = "Obrigatorio informar o responsavel tecnico!")]
        public string? ResponsavelTecnico { get; set; }

        public string? HorarioRecebimentoDoacoes { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
