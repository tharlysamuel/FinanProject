using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finan.DAL
{
    [Table("tipos_movtos")]
    public class tipo_movto
    {
        [Key]
        public int id_tipo_movto { get; set; }

        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatório!")]
        public string descricao { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Tipo é obrigatório!")]
        public string tipo_lancamento { get; set; }

        public virtual ICollection<conta_movto> Movimentacoes{ get; set; }

    


    }
}
