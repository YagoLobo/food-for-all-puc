using System;
using System.ComponentModel.DataAnnotations;

namespace FoodForAll.Models
{
    public class Doacao
    {
        public int Id { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        [Required]
        public int EstabelecimentoReceptorId { get; set; }
        public EstabelecimentoReceptor? EstabelecimentoReceptor { get; set; }
        [Required]
        public DateTime DataDoacao { get; set; }
        public bool Confirmada { get; set; }
    }
} 