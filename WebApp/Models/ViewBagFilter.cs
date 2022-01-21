using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Models
{
    public class ViewBagFilter : IActionFilter
    {
        private static readonly string Enabled = "Enabled";
        private static readonly string Disabled = string.Empty;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                // SmartAdmin Toggle Features
                controller.ViewBag.AppSidebar = Enabled;
                controller.ViewBag.AppHeader = Enabled;
                controller.ViewBag.AppLayoutShortcut = Enabled;
                controller.ViewBag.AppFooter = Enabled;
                controller.ViewBag.ShortcutMenu = Enabled;
                controller.ViewBag.ChatInterface = Enabled;
                controller.ViewBag.LayoutSettings = Enabled;

                // SmartAdmin Default Settings
                controller.ViewBag.App = "EHSPantau";
                controller.ViewBag.AppName = "EHS Pantau";
                controller.ViewBag.AppFlavor = "EHS Pantau";
                controller.ViewBag.AppFlavorSubscript =  "";
                controller.ViewBag.BaseUrl = WebHelper.GetBaseUrl(context.HttpContext);
                controller.ViewBag.User =  SecurityHelper.CurrentUsername(context.HttpContext);
                controller.ViewBag.UserId = SecurityHelper.CurrentUserId(context.HttpContext);
                controller.ViewBag.Username = SecurityHelper.CurrentUsername(context.HttpContext);
                controller.ViewBag.Fullname = SecurityHelper.CurrentFullname(context.HttpContext);
                controller.ViewBag.RoleKode = SecurityHelper.CurrentRoleCode(context.HttpContext);
                controller.ViewBag.RoleName = SecurityHelper.CurrentRoleName(context.HttpContext);
                PersonData pdata = SecurityHelper.GetPersonData(context.HttpContext);
                controller.ViewBag.person_id = pdata.id;
                controller.ViewBag.person_nrp = pdata.nrp;
                controller.ViewBag.person_nama = pdata.nama;
                controller.ViewBag.area_id = pdata.area_id;
                controller.ViewBag.area_kode = pdata.area_kode;
                controller.ViewBag.area_nama = pdata.area_nama;
                controller.ViewBag.ba_id = pdata.ba_id;
                controller.ViewBag.ba_kode = pdata.ba_kode;
                controller.ViewBag.ba_nama = pdata.ba_nama;
                controller.ViewBag.pa_id = pdata.pa_id;
                controller.ViewBag.pa_kode = pdata.pa_kode;
                controller.ViewBag.pa_nama = pdata.pa_nama;
                controller.ViewBag.psa_id = pdata.psa_id;
                controller.ViewBag.psa_kode = pdata.psa_kode;
                controller.ViewBag.psa_nama = pdata.psa_nama;
                controller.ViewBag.Email =  "";
                controller.ViewBag.Twitter =  "codexlantern";
                controller.ViewBag.Avatar =  "avatar-admin.png";
                controller.ViewBag.Version =  "4.0.2";
                controller.ViewBag.Bs4v =  "4.3";
                controller.ViewBag.Logo = "logo.png";
                controller.ViewBag.LogoM = "logo.png";
                controller.ViewBag.Copyright =  "2019 © EHS Pantau";
                controller.ViewBag.CopyrightInverse = "2019 © EHS Pantau";
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
