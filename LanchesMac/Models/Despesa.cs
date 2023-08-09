using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Despesas")]
    public class Despesa
    {
        [Key]
        public int DespesaId { get; set; }

        [Required(ErrorMessage = "O nome da despesa deve ser informado")]
        [Display(Name = "Nome da Despesa")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da despesa deve ser informada")]
        [Display(Name = "Descrição da Despesa")]
        [MinLength(5, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição detalhada da despesa deve ser informada")]
        [Display(Name = "Descrição detalhada da Despesa")]
        [MinLength(5, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada pode exceder {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o valor da despesa")]
        [Display(Name = "Valor")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 9999.99, ErrorMessage = "O valor deve estar entre 1 e 9999,99")]
        public decimal Valor { get; set; }

        [Display(Name = "Data da Despesa")]
        [DataType(DataType.Date)]
        public DateTime DataDespesa { get; set; }

    }
}