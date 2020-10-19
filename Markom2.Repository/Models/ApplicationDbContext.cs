using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Markom2.Repository.Models
{
    //public class AppUser : IdentityUser
    //{
    //    public 
    //}
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
            });

            builder.Entity<MUser>(buildAction =>
            {
                // kolom database harus unik (tidak boleh sama)
                buildAction.HasIndex(item => item.Username)
                    .IsUnique();

                buildAction.HasOne<MRole>(item => item.MRole_Navigation)
                    .WithMany(item => item.MUsers)
                    .HasForeignKey(item => item.MRoleId)
                    .OnDelete(DeleteBehavior.NoAction);

                buildAction.HasOne<MEmployee>(item => item.MEmployee_Navigation)
                    .WithOne(item => item.Muser)
                    .HasForeignKey<MUser>(item => item.MEmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        public DbSet<MCompany> MCompanies { get; set; }

        public DbSet<MEmployee> MEmployees { get; set; }

        public DbSet<MRole> MRoles { get; set; }

        public DbSet<MUser> MUsers { get; set; }
    }
}
