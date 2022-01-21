using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.AccountViewModels;

namespace WebApp.Controllers
{
    public class PwaController : Controller
    {
        private static IHostingEnvironment _hostingEnvironment;
        public PwaController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //private string _path_view = "/Views/Pwa/";

        public IActionResult Index()
        {
            return View();
        }
        #region User Data
        [HttpPost]
        public JsonResult InfoUser()
        {
            InfoUser data = SecurityHelper.GetInfoUser(HttpContext);
            return Json(data);
        }
        #endregion
        #region LOGIN
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Login(LoginViewModel model, string returnUrl = null)
        {
            ProsesResult result = SecurityHelper.SignIn(model.Username, model.Password, true, HttpContext);
            if (result.status == 1)
            {
                return Json(result);
            }
            else
            {
                return Json(result);
            }
        }

        public async Task<IActionResult> Logout()
        {
            SecurityHelper.Logout(HttpContext);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Pwa");

        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("Views/Pwa/AccessDenied.cshtml");
        }
        [HttpGet]
        public IActionResult SessionExpired()
        {
            return View("Views/Pwa/SessionExpired.cshtml");
        }
        #endregion
    }
}