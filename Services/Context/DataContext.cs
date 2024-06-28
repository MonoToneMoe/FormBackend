using FormBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace FormBackend.Services.Context
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<UserModel> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
    }
}