using MySql.Data.Entity;
using System.Data.Entity;

namespace Finan.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class Contexto : DbContext
    {
        public Contexto()
            : base("Contexto")
        {

        }

        public DbSet<banco> Bancos{ get; set; }
        public DbSet<cliente> Clientes { get; set; }
        public DbSet<conta> Contas{ get; set; }
        public DbSet<conta_movto> Movimentacoes{ get; set; }
        public DbSet<tipo_movto> TiposMovtos{ get; set; }

    }
}
