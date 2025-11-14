using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Meteo.Models
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
        public bool HasAccess { get; set; } = true;
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = default!;

       
    }
}
