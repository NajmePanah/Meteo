using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Main.Meteo.Models
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        public List<UserRole> UserRoles { get; set; } = new();
        public List<RolePermission> RolePermissions { get; set; } = new();
    }
}
