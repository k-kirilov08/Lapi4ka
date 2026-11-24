using Lapi4ka.Data.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lapi4ka.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<VetProcedure> VetProcedures { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Application> Applications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>()
            .HasOne(s => s.AnimalType)            
            .WithMany(t => t.Animals)          
            .HasForeignKey(s => s.TypeId);

            modelBuilder.Entity<Adoption>()
            .HasOne(s => s.Animal)
            .WithMany(t => t.AdoptionRequests)
            .HasForeignKey(s => s.AnimalId);

            modelBuilder.Entity<Adoption>()
            .HasOne(s => s.Adopter)
            .WithMany(t => t.AdoptionRequests)
            .HasForeignKey(s => s.AdopterId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adoption>()
            .HasOne(s => s.Employee)
            .WithMany(t => t.ProcessedRequests)
            .HasForeignKey(s => s.EmployeeId)                 
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VetProcedure>()
            .HasOne(s => s.Animal)
            .WithMany(t => t.Procedures)
            .HasForeignKey(s => s.AnimalId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
            .HasOne(s => s.User)
            .WithMany(t => t.Applications)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityRole>().HasData(
           new IdentityRole
           {
               Id = "1",
               Name = Lapi4ka.Data.Constants.UserRoles.Admin,
               NormalizedName = Lapi4ka.Data.Constants.UserRoles.Admin.ToUpper()
           },
           new IdentityRole
           {
               Id = "2",
               Name = Lapi4ka.Data.Constants.UserRoles.Employee,
               NormalizedName = Lapi4ka.Data.Constants.UserRoles.Employee.ToUpper()
           },
           new IdentityRole
           {
               Id = "3",
               Name = Lapi4ka.Data.Constants.UserRoles.User,
               NormalizedName = Lapi4ka.Data.Constants.UserRoles.User.ToUpper()
           });
        }
    }
}
