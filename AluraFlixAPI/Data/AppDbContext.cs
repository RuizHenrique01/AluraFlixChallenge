using AluraFlixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraFlixAPI.Data{

    public class AppDbContext : DbContext {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        { 
            base.OnModelCreating(builder);

            Categoria categoria = new Categoria{
                Id = 1,
                Titulo = "Livre",
                Cor = "#008000"
            };

            builder.Entity<Categoria>().HasData(categoria);

            builder.Entity<Categoria>()
                .HasMany(categoria => categoria.Videos)
                .WithOne(video => video.Categoria)
                .HasForeignKey(video => video.CategoriaId);
        }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Categoria> Categorias {  get; set; }

    }
}