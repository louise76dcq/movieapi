using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;


namespace MovieAPI.Data
{
    public class APIContext : DbContext
    {

        public DbSet<Movie> Movies { get; set; }

        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

        }

    }
}