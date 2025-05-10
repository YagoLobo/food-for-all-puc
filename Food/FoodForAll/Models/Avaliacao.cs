using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        [Required]
        public int DoacaoId { get; set; }
        public Doacao? Doacao { get; set; }
        [Required]
        [Range(1, 5)]
        public int Nota { get; set; } // 1 a 5
        [MaxLength(300)]
        public string? Comentario { get; set; }
        [Required]
        public DateTime DataAvaliacao { get; set; }
    }
} 