using System.ComponentModel.DataAnnotations;

namespace FoodforAll.Models.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        // -------------------
        // Campos do Usuario
        // -------------------
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        public UserType UserType { get; set; }


        public string? CNPJ { get; set; }

        public string? NomeFantasia { get; set; }

        public bool? TransporteProprio { get; set; }

        public string? NomeReceptor { get; set; }


        public string? RegistroGoverno { get; set; }

        public bool? Aprovada { get; set; }

        public int? RefeicoesDiarias { get; set; }

        public string? ResponsavelTecnico { get; set; }

        public string? HorarioRecebimentoDoacoes { get; set; }
    }

}
