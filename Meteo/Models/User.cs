using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace Main.Meteo.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        //public string? Password { get; set; }
        public string? H256Password { get; set; }
        public string? NationalCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? EmployeeCode { get; set; }

        public List<UserRole> UserRoles { get; set; } = new();
    }
}
