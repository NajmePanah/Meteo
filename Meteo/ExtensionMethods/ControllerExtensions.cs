using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Main.Meteo.ExtensionMethods
{
    public static class ControllerExtensions
    {
        public static string CurrentModuleContentPath(this Controller controller)
        {
            var assemblyName = Assembly.GetCallingAssembly().GetName().Name;
            return $"\\_content\\{assemblyName}";
        }
        public static string Name(this Controller controller)
        {
            return controller.ToString().Remove(controller.ToString().IndexOf("Controller"));
        }
        public static IActionResult RedirectToActionWithArea(this Controller controller, string action, string controllerName="", object routeValues = null)
        {
            var currentArea = controller.RouteData.Values["area"]?.ToString();
            var newRouteValues = new RouteValueDictionary(routeValues);
            if (!string.IsNullOrEmpty(currentArea))
            {
                newRouteValues["area"] = currentArea;
            }
            if (!string.IsNullOrEmpty(controllerName))
            {
                newRouteValues["controller"] = currentArea;
            }
            return controller.RedirectToAction(action, newRouteValues);
        }
        public static void SetMessage(this Controller ctr, MessageEnum type, string message)
        {
            try
            {
                if (ctr.TempData["Message"] == null || ctr.TempData["Class"] == null)
                {
                    ctr.TempData["Message"] = new List<string>();
                    ctr.TempData["Class"] = new List<string>();
                }
                //((List<string>)ctr.TempData["Message"]).Add(message);
                //((List<string>)ctr.TempData["Class"]).Add(type.ToString());
                if (ctr.TempData["Message"] is string[])
                {
                    ctr.TempData["Message"] = new List<string>((string[])ctr.TempData["Message"]);
                    ctr.TempData["Class"] = new List<string>((string[])ctr.TempData["Class"]);
                }
                ((List<string>)ctr.TempData["Message"]).Add(message);
                ((List<string>)ctr.TempData["Class"]).Add(type.ToString());
            }
            catch(Exception ex) {
                ctr.TempData["Message"] = new List<string>();
                ctr.TempData["Class"] = new List<string>();
            }
        }
        public static string GetModelStateErrors(this Controller ctr,string seprator= ";")
        {
            return string.Join(seprator, ctr.ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
        }
    }
    public enum MessageEnum
    {
        None = 0,
        success = 1,
        danger = 3,
        warning = 4,
        info = 5
    }
}
