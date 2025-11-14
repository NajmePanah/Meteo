using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Main.Meteo.Models
{
    public class UserRole : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
