using Microsoft.EntityFrameworkCore;
using WebAppInfoStud2.Models;

namespace WebAppInfoStud2.Context
{
    public class InfoStudDB : DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Curriculum> Curriculums => Set<Curriculum>();
        public DbSet<Contact> Contacts => Set<Contact>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Student;Username=postgres;Password=1423");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}