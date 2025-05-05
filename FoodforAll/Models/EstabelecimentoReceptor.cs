using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodforAll.Models
{
    [Table("EstabelecimentoReceptor")]
    public class EstabelecimentoReceptor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o email!")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o Resgistro do Governo!")]
        public string? RegistroGoverno { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar se esta aprovada no programa do governo!")]
        public bool Aprovada { get; set; }

        public int? RefeicoesDiarias { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar a senha")]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o email!")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Obrigatorio  informar o telefone para contato!")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o endereco!")]
        public string? Endereco { get; set; }

    }
}
