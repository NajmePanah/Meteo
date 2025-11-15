using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Portal.xAuth
{
    public class DynamicAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        public async void OnAuthorization(AuthorizationFilterContext context)
        {

            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }
            else
            {
                if (user.Identity.Name.ToLower() == "administrator") return;
                if (user.Claims.FirstOrDefault(c => c.Type == "UserRoles").ToString().ToLower().Contains("administrator")) return;
                var needUpdateContactClaimValue = user.Claims.FirstOrDefault(c => c.Type == "NeedUpdateContactInfo")?.Value;
                var needUpdateContact = needUpdateContactClaimValue != null && !string.IsNullOrEmpty(needUpdateContactClaimValue.ToString()) ? bool.Parse(needUpdateContactClaimValue.ToString()) : false;
                if (needUpdateContact)
                {
                    context.Result = new RedirectToActionResult("Contact", "CurrentUser", new { area = "", path = context.HttpContext.Request.Path });
                    return;
                }
                else
                {
                    var routeData = context.HttpContext.GetRouteData();
                    var areaName = !string.IsNullOrEmpty(routeData.Values["area"]?.ToString()) ? routeData.Values["area"]?.ToString(): "";
                    var controllerName = routeData.Values["controller"]?.ToString();
                    var actionName = routeData.Values["action"]?.ToString();
                    var currentUrl = string.Empty;
                    currentUrl += areaName + "/" + controllerName + "/" + actionName;
                    if (string.IsNullOrEmpty(controllerName) || string.IsNullOrEmpty(actionName))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                    if (user.Claims.FirstOrDefault(c => c.Type == "UserId") != null && !string.IsNullOrEmpty(user.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value))
                    {
                        var premessions = user.Claims.FirstOrDefault(c => c.Type == "Permissions").ToString().Replace("Permissions: ", "").Split(",");
                        if (premessions.Any(p => p.ToLower().Equals(currentUrl.ToLower())))
                        {
                            return;
                        }

                        //using (var _context = new AuthDbContext())
                        //{
                        //    var userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
                        //    var positionId = !string.IsNullOrEmpty(user.Claims.FirstOrDefault(c => c.Type == "CurrentPositionId")?.Value) ? int.Parse(user.Claims.FirstOrDefault(c => c.Type == "CurrentPositionId")?.Value) : 0;
                        //    var userRoles = _context.Set<UserRole>()
                        //            .Where(ur => ur.UserId == userId)
                        //            .Select(ur => ur.RoleId)
                        //            .ToList();
                        //    if (user.Identity.Name.ToLower().Equals("administrator")) return;

                        //    if (_context.Set<RolePermission>()
                        //        .Include(ur => ur.Premission)
                        //            .Any(rp => userRoles.Contains(rp.RoleId) && rp.Premission.AreaName.ToLower() == areaName && rp.Premission.ControllerName.ToLower() == controllerName && rp.Premission.ActionName.ToLower() == actionName && rp.HasAccess))
                        //    {
                        //        return;
                        //    }
                        //    if (positionId > 0 && _context.Set<PositionPermission>()
                        //        .Include(ur => ur.Premission)
                        //            .Where(p => p.PositionId == positionId).Any(rp => rp.Premission.AreaName.ToLower() == areaName && rp.Premission.ControllerName.ToLower() == controllerName && rp.Premission.ActionName.ToLower() == actionName && rp.HasAccess))
                        //    {
                        //        return;
                        //    }

                        //}
                    }
                    context.Result = new ForbidResult();
                }
            }
        }
    }

}
