using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodforAll.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? Password { get; set; }
        
        [Required(ErrorMessage = "Obrigatorio  informar o telefone para contato!")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Obrigatorio  informar o endereco!")]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public UserType UserType { get; set; } 

        public int? EstabelecimentoDoadorId { get; set; }
        public int? EstabelecimentoReceptorId { get; set; }

        [ForeignKey("DoadorId")]
        public EstabelecimentoDoador? Doador { get; set; }

        [ForeignKey("ReceptorId")]
        public EstabelecimentoReceptor? Receptor { get; set; }
    }

    public enum UserType
    {
        Admin,
        EstabelecimentoDoador,
        EstabelecimentoReceptor
    }
}
