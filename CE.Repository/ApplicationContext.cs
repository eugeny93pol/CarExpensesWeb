using System;
using CE.DataAccess;
using CE.DataAccess.Constants;
using Microsoft.EntityFrameworkCore;

namespace CE.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CarAction> Actions { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarSettings> CarsSettings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserSettings> UsersSettings { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<ActionType> ActionTypes { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), Name = RolesConstants.Admin }, 
                new Role { Id = Guid.NewGuid(), Name = RolesConstants.User });

            modelBuilder.Entity<ActionType>().HasData(
                new ActionType { Id = Guid.NewGuid(), Name = ActionTypesConstants.Mileage },
                new ActionType { Id = Guid.NewGuid(), Name = ActionTypesConstants.Purchases },
                new ActionType { Id = Guid.NewGuid(), Name = ActionTypesConstants.Refill },
                new ActionType { Id = Guid.NewGuid(), Name = ActionTypesConstants.Repair });

            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.Role)
                .HasPrincipalKey(r => r.Name);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<CarAction>()
                .HasOne<ActionType>()
                .WithMany(a => a.Actions)
                .HasForeignKey(a => a.Type)
                .HasPrincipalKey(t => t.Name);

            base.OnModelCreating(modelBuilder);
        }
    }
}
