using Microsoft.EntityFrameworkCore;
using peliculas_api.entidades;

namespace peliculas_api
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions option
            ) : base(option)
        {

        }

        public DbSet<Genero> Generos { get; set; }
    }
}
