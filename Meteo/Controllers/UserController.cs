using Main.Meteo.Controllers;
using Main.Meteo.Models;
using Meteo.Models;
using Meteo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Main.Meteo;
using Portal.xAuth;
using Meteo.ViewModels.Filter;


namespace Meteo.Controllers
{
    public class UserController : BaseController
    {
        private readonly MeteoDbContext _db;

        public UserController(MeteoDbContext db)
        {
            _db = db;
        }

        [DynamicAuthorize]
        public async Task<IActionResult> Index(int? id, PaginationViewModel<User, UserFilter> pvm)
        {
            id ??= 1;
            pvm ??= new PaginationViewModel<User, UserFilter>();
            var cuser = await _db.Users.AsNoTracking()
              .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(p => p.Username == User.Identity.Name);
            var allData = _db.Users.AsNoTracking()
              .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .AsQueryable();
            if (!cuser.UserRoles.Any(r => r.Role.Title.ToLower().Contains("administrator")) && cuser.Username.ToLower() != "administrator")
            {
                allData = allData.Where(p => !p.Username.ToLower().Equals("administrator") && !p.UserRoles.Any(r => r.Role.Title.ToLower().Contains("administrator"))).AsQueryable();
            }
            pvm.Filter ??= new UserFilter();
            pvm.Filter.Roles = await _db.Roles.AsNoTracking().ToListAsync();
            allData = pvm.Filter.ApplyFilter(allData);
            pvm.Pagination.TotalCount = allData.Count();
            pvm.Pagination.CurrentPage = id.Value;
            id = id > pvm.Pagination.TotalPage ? 1 : id;
            pvm.Data = await allData.Skip((id.Value - 1) * pvm.Pagination.ItemsPerPage).Take(pvm.Pagination.ItemsPerPage).ToListAsync();
            return View(pvm);
        }

    }
}
