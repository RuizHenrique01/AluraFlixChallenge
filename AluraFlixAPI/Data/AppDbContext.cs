using AluraFlixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraFlixAPI.Data{

    public class AppDbContext : DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){

        }

        public DbSet<Video> Videos { get; set; }

    }
}