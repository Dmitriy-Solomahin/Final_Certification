using ApiUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiUsers.DB
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .LogTo(Console.WriteLine)
            .UseNpgsql("Host=localhost;Username=postgres;Password=2402;Database=UserLoginDb");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("users_pkey");
                entity.HasIndex(x => x.Email).IsUnique();

                entity.ToTable("users");

                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.Email)
                    .HasMaxLength(255)
                    .HasColumnName("Email");

                entity.Property(x => x.Password).HasColumnName("Password");
                entity.Property(x => x.Salt).HasColumnName("Salt");

                entity.Property(x => x.RoleId).HasConversion<int>();
            });

            modelBuilder
                .Entity<Role>()
                .Property(x => x.RoleId)
                .HasConversion<int>();

            modelBuilder
                .Entity<Role>().HasData(Enum.GetValues(typeof(Roles))
                .Cast<Roles>().Select(x => new Role() 
                {
                    RoleId = x,
                    RoleName = x.ToString()
                }));

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
