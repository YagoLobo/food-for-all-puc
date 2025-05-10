using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Nome { get; set; }
        [MaxLength(200)]
        public string? Descricao { get; set; }
        [Required]
        public DateTime DataValidade { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public int EstabelecimentoDoadorId { get; set; }
        public EstabelecimentoDoador? EstabelecimentoDoador { get; set; }
        public bool Disponivel { get; set; }
    }
} 