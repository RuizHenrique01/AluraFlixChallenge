using AluraFlixAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;

namespace AluraFlixAPI.Data{

    public class AppDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>{
        private IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> opt, IConfiguration configuration) : base(opt) {
            _configuration = configuration;
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

            IdentityUser<int> admin = new IdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 1
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

            admin.PasswordHash = hasher.HashPassword(admin, _configuration.GetValue<string>("Admin:Password"));

            builder.Entity<IdentityUser<int>>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 2, Name = "regular", NormalizedName = "REGULAR" }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
              new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
            );
        }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Categoria> Categorias {  get; set; }

    }
}