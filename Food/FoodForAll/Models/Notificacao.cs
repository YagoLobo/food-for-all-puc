using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class Notificacao
    {
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        [Required, MaxLength(200)]
        public string? Mensagem { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
        public bool Lida { get; set; }
    }
} 