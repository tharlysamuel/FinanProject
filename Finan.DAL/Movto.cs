using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finan.DAL
{
    [Table("contas_movtos")]
    public class conta_movto
    {
        [Key]
        public int id_movto { get; set; }

        [Display(Name = "Valor (R$)")]
        public double valor { get; set; }

        [Display(Name ="Data")]
        [Required(ErrorMessage = "Data é obrigatório!")]
        public DateTime data { get; set; } 

        [Display(Name = "Conta")]
        [Required(ErrorMessage = "Conta é obrigatório!")]
        public int id_conta { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Tipo é obrigatório!")]
        public int id_tipo_movto { get; set; }

        public virtual conta Conta{ get; set; }

        public virtual tipo_movto Tipo{ get; set; }


    }
}
