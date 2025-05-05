using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodforAll.Models
{
    [Table("EstabelecimentoDoador")]
    public class EstabelecimentoDoador
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o CNPJ!")]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o nome fantasia!")]
        public required string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar a  categoria!")]
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar a senha")]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o telefone para contato!")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o endereco!")]
        public string? Endereco { get; set; }

    }
}
