using CustomerService.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discount>()
                .HasOne(d => d.Agent)
                .WithMany()
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Agent>()
                .HasIndex(a => a.Email)
                .IsUnique();

            var passwordHasher = new PasswordHasher<Agent>();
            var agent1 = new Agent { AgentId = 1, Name = "Agent One", Username = "agent1", Email = "agent1@example.com", Password = null };
            var agent2 = new Agent { AgentId = 2, Name = "Agent Two", Username = "agent2", Email = "agent2@example.com", Password = null };
            var agent3 = new Agent { AgentId = 3, Name = "Agent Three", Username = "agent3", Email = "agent3@example.com", Password = null };
            var agent4 = new Agent { AgentId = 4, Name = "Agent Four", Username = "agent4", Email = "agent4@example.com", Password = null };
            var agent5 = new Agent { AgentId = 5, Name = "Agent Five", Username = "agent5", Email = "agent5@example.com", Password = null };

            agent1.Password = passwordHasher.HashPassword(agent1, "Password123!");
            agent2.Password = passwordHasher.HashPassword(agent2, "Password123!");
            agent3.Password = passwordHasher.HashPassword(agent3, "Password123!");
            agent4.Password = passwordHasher.HashPassword(agent4, "Password123!");
            agent5.Password = passwordHasher.HashPassword(agent5, "Password123!");

            modelBuilder.Entity<Agent>().HasData(agent1, agent2, agent3, agent4, agent5);
        }
    }
}
