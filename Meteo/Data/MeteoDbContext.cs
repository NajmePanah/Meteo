using Main.Meteo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Main.Meteo
{
    public class MeteoDbContext : DbContext
    {
        public MeteoDbContext(DbContextOptions<MeteoDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---- Composite Keys ----
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // ---- Relationships ----
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId);

            // ---- Seed Data ----

            //modelBuilder.Entity<User>().HasData(
            //new User
            //{
            //    Id = 1,
            //    Username = "Administrator",
            //    H256Password = "$2a$11$C6UzMDM.H6dfI/f/IKcEeOaTn5MZcZr1r4y0fUlOCV9f5cVHgZ7di",
            //    CreateTime = DateTime.MinValue,
            //    CreatorIp = "127.0.0.1",
            //    CreatorUsername = "modelBuilder",
            //    CreatorFullName = "فاقد نام",
            //    CreatorClientName = "modelBuilder",
            //});


            //    var adminRole = new Role { Id = 1, Title = "Administrator" };
            //    var adminUser = new User
            //    {
            //        Id = 1,
            //        Username = "admin",
            //        H256Password = BCrypt.Net.BCrypt.HashPassword("admin123")
            //    };

            //    var permissions = new List<Permission>
            //    {
            //        new Permission { Id = 1, AreaName = "Main", ControllerName = "Dashboard", ActionName = "Index" },
            //        new Permission { Id = 2, AreaName = "Main", ControllerName = "User", ActionName = "Manage" }
            //    };

            //    var userRole = new UserRole { UserId = adminUser.Id, RoleId = adminRole.Id };
            //    var rolePermissions = new List<RolePermission>
            //    {
            //        new RolePermission { RoleId = adminRole.Id, PermissionId = 1, HasAccess = true },
            //        new RolePermission { RoleId = adminRole.Id, PermissionId = 2, HasAccess = true }
            //    };

            //    modelBuilder.Entity<Role>().HasData(adminRole);
            //    modelBuilder.Entity<User>().HasData(adminUser);
            //    modelBuilder.Entity<Permission>().HasData(permissions);
            //    modelBuilder.Entity<UserRole>().HasData(userRole);
            //    modelBuilder.Entity<RolePermission>().HasData(rolePermissions);
        }
    }
}
