using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Meteo.Models
{
    public class Permission : BaseEntity
    {
        public int Id { get; set; }
        public string AreaName { get; set; } = default!;
        public string ControllerName { get; set; } = default!;
        public string ActionName { get; set; } = default!;
        public string? Description { get; set; }

        public List<RolePermission> RolePermissions { get; set; } = new();
    }
}
