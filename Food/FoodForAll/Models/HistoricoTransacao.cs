using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class HistoricoTransacao
    {
        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        [Required, MaxLength(100)]
        public string? Acao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [MaxLength(300)]
        public string? Detalhes { get; set; }
    }
} 