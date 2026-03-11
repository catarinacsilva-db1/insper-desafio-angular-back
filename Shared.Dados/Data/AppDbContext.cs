using CadastroUsuarios.Models;
using System.Data.Entity;

namespace CadastroUsuarios.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}