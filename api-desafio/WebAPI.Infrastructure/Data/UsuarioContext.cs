using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Models;

namespace WebAPI.Infrastructure.Data
{
    public class UsuarioContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sexo> Sexos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-IB22G03\\SQLEXPRESS; Initial Catalog=WebApi; Integrated Security=True; Connect Timeout=30");
        }
    }
}
