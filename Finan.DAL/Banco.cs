using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.DAL
{
    [Table("bancos")]
    public class banco
    {
        [Key]
        public int id_banco { get; set; }

        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres!")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string nome { get; set; }

        public virtual ICollection<conta> Contas{ get; set; }

    }
}
