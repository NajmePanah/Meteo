using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
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
            // بازیابی پیام‌های قبلی
            var messages = ctr.TempData.Get<List<string>>("Messages") ?? new List<string>();
            var classes = ctr.TempData.Get<List<string>>("MessageClasses") ?? new List<string>();

            messages.Add(message);
            classes.Add(type.ToString());

            // ذخیره مجدد به صورت JSON
            ctr.TempData.Put("Messages", messages);
            ctr.TempData.Put("MessageClasses", classes);
        }

        // متد کمکی برای TempData
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key)
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? default : JsonConvert.DeserializeObject<T>((string)o);
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


