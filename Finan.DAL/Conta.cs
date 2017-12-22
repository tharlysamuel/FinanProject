using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finan.DAL
{
    [Table("contas")]
    public class conta
    {
        [Key]
        public int id_conta { get; set; }

        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatório!")]
        public string descricao { get; set; }

        [Display(Name = "Saldo Inicial")]
        [Required(ErrorMessage = "Saldo Inicial é obrigatório!")]
        [Range(0,9999999999999,ErrorMessage ="Saldo Inicial não pode ser menor que 0!")]
        public double saldo_inicial { get; set; }

        [Display(Name = "Saldo Inicial")]
        public bool ativa { get; set; } = true;

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "Banco é obrigatório!")]
        public int id_banco { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Cliente é obrigatório!")]
        public int id_cliente { get; set; }

        public virtual banco Banco { get; set; }

        public virtual cliente Cliente { get; set; }

        public virtual ICollection<conta_movto> Movimentacao { get; set; }

    }
}
