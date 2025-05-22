    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ToDoListAPI.Data.Config;
    using ToDoListAPI.Models;
 
    namespace ToDoListAPI.Data
    {
        public class AppDbContext : IdentityDbContext<AppUser>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Todo> Todos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration(new TodoConfig());

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                     Id = "Admin", Name = "Admin", NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                     Id = "User", Name = "User", NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
    }
