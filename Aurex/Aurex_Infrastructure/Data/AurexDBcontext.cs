using Aurex_Core.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aurex_Infrastructure.Data
{
    public class AurexDBcontext:IdentityDbContext
    {
        public AurexDBcontext(DbContextOptions<AurexDBcontext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
          .HasOne(u => u.Employee)
          .WithOne(e => e.User)
          .HasForeignKey<Employee>(e => e.UserId)
          .IsRequired(false);

            // Deal 1:1 Project (Project has FK DealId)
            builder.Entity<Deal>()
                .HasOne(d => d.Project)
                .WithOne(p => p.Deal)
                .HasForeignKey<Project>(p => p.DealId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmployeeProject composite PK + relations (many-to-many)
            builder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            builder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);

            // Project Lead (optional) — if lead deleted set null
            builder.Entity<Project>()
                .HasOne(p => p.TeamLeader)
                .WithMany(e => e.LeadingProjects)
                .HasForeignKey(p => p.TeamLeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmployeeTask -> Employee (1:m)
            builder.Entity<EmployeeTask>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.EmployeeTasks)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);


            // Employee -> Department (m:1)
            builder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            // Enums type
            builder.Entity<Deal>()
                .Property(d => d.Currency)
                .HasConversion<string>();
            builder.Entity<Project>()
                .Property(p => p.Status)
                .HasConversion<string>();
            builder.Entity<Project>()
                .Property(p => p.Negotiation)
                .HasConversion<string>();
            builder.Entity<Deal>()
                .Property(d => d.Status)
                .HasConversion<string>();
            builder.Entity<EmployeeTask>()
                .Property(t => t.Status)
                .HasConversion<string>();
            builder.Entity<EmployeeTask>()
                .Property(t => t.Priority)
                .HasConversion<string>();
            builder.Entity<Activity>()
                .Property(a => a.Type)
                .HasConversion<string>();
            builder.Entity<Client>()
                .Property(c => c.Status)
                .HasConversion<string>();
            builder.Entity<Invoice>()
                .Property(i => i.Status)
                .HasConversion<string>();

            //decmail 
            builder.Entity<Deal>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Project>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");
            builder.Entity<Project>()
                .Property(p => p.Probability)
                .HasColumnType("int");
            builder.Entity<Invoice>()
                .Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)");
           
        }

    }
 }
