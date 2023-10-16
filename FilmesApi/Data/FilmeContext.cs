using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data;

public class FilmeContext : DbContext
{
    public FilmeContext(DbContextOptions<FilmeContext> dbOptions) : base(dbOptions)
    {
        
    }

    public DbSet<Filme> Filmes { get; set; }

}
