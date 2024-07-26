using EmployeeManagementSystem.Entities.Enitites;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystemDBContext
{
    public class TeleDBContext : DbContext
    {
        public TeleDBContext() { }
        public TeleDBContext(DbContextOptions<TeleDBContext> options) : base(options) { }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<SystemUsers> SystemUsers { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoles>().HasKey(entity => new { entity.SystemUserId, entity.RoleId });
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.HasOne(s => s.ModifiedBy)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<SystemUsers>().HasData(new SystemUsers()
            {
                Id = 1,
                Email = "admin@test.com",
                FullName = "Admin",
                PhoneNumber = "1234567890",
                Username = "admin",
                Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("123456"))
            });

            modelBuilder.Entity<Roles>().HasData(new Roles()
            {
                Id = 1,
                Name = "Admin"
            }, new Roles()
            {
                Id = 2,
                Name = "Normal"
            });

            modelBuilder.Entity<UserRoles>().HasData(new UserRoles()
            {
                RoleId = 1,
                SystemUserId = 1
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
