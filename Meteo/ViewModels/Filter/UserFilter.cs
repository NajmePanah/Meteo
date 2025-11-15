
using Main.Meteo.Models;

namespace Meteo.ViewModels.Filter
{
    public class UserFilter
    {

        public List<Role> Roles { get; set; } = new List<Role>();
        public List<string> PlaceOfWorks { get; set; } = new List<string>();

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public string? Username { get; set; }
        public int? RoleId { get; set; }

        public IQueryable<User> ApplyFilter(IQueryable<User> allData)
        {
            if (!string.IsNullOrEmpty(FirstName))
            {
                allData = allData.Where(p => p.FirstName.ToLower().Contains(FirstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                allData = allData.Where(p => p.LastName.ToLower().Contains(LastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(NationalCode))
            {
                allData = allData.Where(p => p.NationalCode.ToLower().Contains(NationalCode.ToLower()));
            }
            if (!string.IsNullOrEmpty(Username))
            {
                allData = allData.Where(p => p.Username.ToLower().Contains(Username.ToLower()));
            }
            if (RoleId != null)
            {
                allData = allData.Where(p => p.UserRoles.Any(u => u.RoleId == RoleId));
            }
            return allData;
        }
    }
}
