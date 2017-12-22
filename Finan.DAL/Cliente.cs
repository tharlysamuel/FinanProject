using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.DAL
{
    [Table("clientes")]
    public class cliente
    {
        [Key]
        public int id_cliente { get; set; }

        [StringLength(250, ErrorMessage = "Máximo de 250 caracteres!")]
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string nome { get; set; }

        [StringLength(15, ErrorMessage = "Máximo de 15 caracteres!")]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        public virtual ICollection<conta> Contas { get; set; }



    }
}
