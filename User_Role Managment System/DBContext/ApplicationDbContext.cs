using Microsoft.EntityFrameworkCore;
using User_Role_Managment_System.Models;

namespace User_Role_Managment_System.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Signup_model> Signup_Tbl { get; set; }
        public DbSet<Role_Model> Role_Tbl { get; set; }
        public DbSet<User_Model> User_Tbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Signup_model>()
                        .HasKey(s => s.ID);
        }
    }
}
