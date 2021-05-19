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


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role admin = new Role { Id = 1, Name = RolesConstants.Admin };
            Role user = new Role { Id = 2, Name = RolesConstants.User };

            modelBuilder.Entity<Role>()
                .HasData(new Role[] { admin, user });

            modelBuilder.Entity<User>()
                .HasOne<Role>()
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.Role)
                .HasPrincipalKey(r => r.Name);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
