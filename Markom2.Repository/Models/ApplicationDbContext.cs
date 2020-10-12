using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Markom2.Repository.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MCompany>(buildAction =>
            {
                buildAction.Property(item => item.Code)
                    .IsRequired();

                buildAction.Property(item => item.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
            });

            builder.Entity<MEmployee>(buildAction =>
            {
                buildAction.HasIndex(item => item.Code)
                    .IsUnique();

                buildAction.Property(item => item.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
            });

            builder.Entity<MRole>(buildAction =>
            {
                buildAction.Property(item => item.Code)
                    .IsRequired();

                buildAction.Property(item => item.CreatedDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");
            })
        }

        public DbSet<MCompany> MCompanies { get; set; }

        public DbSet<MEmployee> MEmployees { get; set; }

        public DbSet<MRole> MRoles { get; set; }
    }
}
