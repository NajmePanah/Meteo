using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Main.Meteo
{
    public abstract class BaseEntity
    {
        protected BaseEntity(HttpContext context, ClaimsPrincipal user)
        {
            CreateTime = DateTime.Now;
            CreatorUsername =user.Identity.IsAuthenticated? user.Identity.Name: "لاگین نشده";
            CreatorIp = context?.Connection?.RemoteIpAddress?.ToString();
            CreatorFullName = user.Identity.IsAuthenticated ? user.FindFirst("FullName").Value : "لاگین نشده";
            CreatorUserId = user.Identity.IsAuthenticated && !string.IsNullOrEmpty(user.FindFirst("UserId").Value) ? int.Parse(user.FindFirst("UserId").Value) : 0;

        }
        protected BaseEntity(Controller controller)
        {
            CreateTime = DateTime.Now;
            CreatorUsername =controller.User.Identity.IsAuthenticated?controller.User.Identity.Name:"لاگین نشده";
            CreatorIp = controller.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            CreatorFullName = controller.User.Identity.IsAuthenticated ? controller.User.FindFirst("FullName").Value : "لاگین نشده";
            CreatorUserId = controller.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(controller.User.FindFirst("UserId").Value) ? int.Parse(controller.User.FindFirst("UserId").Value) : 0;


        }
        protected BaseEntity()
        {
            CreateTime = DateTime.Now;
            CreatorClientName = "BaseEntity()";
        }
        public DateTime CreateTime { get; set; } = DateTime.MinValue;
        public string CreatorUsername { get; set; } = "System";
        public int CreatorUserId { get; set; } = 0;
        public string CreatorFullName { get; set; } = "فاقد نام";
        public string CreatorIp { get; set; } = "127.0.0.1";
        public string CreatorClientName { get; set; } = "Unknown";
        public string Creator => !string.IsNullOrEmpty(CreatorFullName.Trim()) ? CreatorFullName.Trim() : !string.IsNullOrEmpty(CreatorUsername.Trim()) ? CreatorUsername.Trim() : "نامشخص";



        public DateTime? ModifyTime { get; set; }
        public string? ModifierUsername { get; set; }
        public int? ModifierUserId { get; set; }
        public string? ModifierFullName { get; set; }
        public string? ModifierIp { get; set; }
        public string? ModifierClientName { get; set; }
        public string Modifier => ModifyTime.HasValue && !string.IsNullOrEmpty(ModifierFullName?.Trim()) ? ModifierFullName.Trim() : ModifyTime.HasValue && !string.IsNullOrEmpty(ModifierUsername?.Trim()) ? ModifierUsername.Trim() : ModifyTime.HasValue? "نامشخص":"فاقد اطلاعات";

        public DateTime LastUpdateOrCreateTime => ModifyTime.HasValue ? ModifyTime.Value : CreateTime;

        public bool IsActive { get; set; }=true;
        public string IsActiveLable => IsActive ? "فعال" : "غیر فعال";
        public string IsActiveClass => IsActive ? "success" : "danger";
        public bool IsDeleted { get; set; } =false;
        public DateTime? DeleteTime { get; set; }
        public string IsDeletedLable => IsDeleted ? "حذف شده" : "حذف نشده";
        public string IsDeletedClass => IsDeleted ?  "success" : "danger";

        public void SetCreateInfo(Controller controller)
        {
            CreateTime = DateTime.Now;
            CreatorUsername =controller.User.Identity.IsAuthenticated? controller.User.Identity.Name: "لاگین نشده";
            CreatorUserId = controller.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(controller.User.FindFirst("UserId").Value) ? int.Parse(controller.User.FindFirst("UserId").Value) : 0;
            CreatorIp = controller.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            CreatorFullName = controller.User.Identity.IsAuthenticated ? controller.User.FindFirst("FullName").Value : "لاگین نشده";

        }
        public void SetUpdateInfo(ClaimsPrincipal? user,HttpContext? context)
        {
            ModifyTime = DateTime.Now;
            ModifierUsername =user.Identity.IsAuthenticated? user.Identity.Name: "لاگین نشده";
            ModifierUserId = user.Identity.IsAuthenticated && !string.IsNullOrEmpty(user.FindFirst("UserId").Value) ? int.Parse(user.FindFirst("UserId").Value) : 0;
            ModifierIp = context?.Connection?.RemoteIpAddress?.ToString();
            ModifierFullName = user.Identity.IsAuthenticated ? user.FindFirst("FullName").Value : "لاگین نشده";

        }
        public void SetUpdateInfo(Controller controller)
        {
            ModifyTime = DateTime.Now;
            ModifierUsername =controller.User.Identity.IsAuthenticated? controller.User.Identity.Name: "لاگین نشده";
            ModifierIp = controller.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            ModifierFullName = controller.User.Identity.IsAuthenticated ? controller.User.FindFirst("FullName").Value : "لاگین نشده";
        }
       
    }

}
