using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace WebApp.Controllers
{
    //[Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        
        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(string returnUrl = null)
        public IActionResult Login(string returnUrl = null)
        {
            string strUA = HttpContext.Request.Headers["User-Agent"].ToString().Trim().ToLower();
            bool isMobile = false;
            string[] mobile = { "iphone", "ipad", "android", "blackberry", "nokia", "opera mini", "windows mobile", "windows phone", "iemobile", "tablet", "mobi" };
            foreach (string item in mobile)
            {
                if (strUA.Contains(item))
                {
                    isMobile = true;
                    break;
                }
            }
            if (isMobile == true)
            {
                return RedirectToAction("Index", "Pwa");
            }

            SecurityHelper.Logout(HttpContext);
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            SecurityHelper.onPageLoginInit(HttpContext);
            // Clear the existing external cookie to ensure a clean login process
            ViewData["Layout"] = "_LayoutLogin";
            ViewData["ReturnUrl"] = returnUrl;            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LoginProses(LoginViewModel model, string returnUrl = null)
        {
            ProsesResult result = SecurityHelper.SignIn(model.Username, model.Password, model.RememberMe, HttpContext);
            if (result.status==1)
            {
                //if (model.RememberMe == true)
                //{

                //}
                //var claims = new List<Claim>
                //    {
                //        new Claim(ClaimTypes.Name,  model.Username),
                //        new Claim(ClaimTypes.Role, "Administrator"),
                //        new Claim(ClaimTypes.Role, "Operator")
                //    };

                //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //var authProperties = new AuthenticationProperties
                //{
                //    //AllowRefresh = <bool>,
                //    // Refreshing the authentication session should be allowed.

                //    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //    // The time at which the authentication ticket expires. A 
                //    // value set here overrides the ExpireTimeSpan option of 
                //    // CookieAuthenticationOptions set with AddCookie.

                //    //IsPersistent = true,
                //    // Whether the authentication session is persisted across 
                //    // multiple requests. When used with cookies, controls
                //    // whether the cookie's lifetime is absolute (matching the
                //    // lifetime of the authentication ticket) or session-based.

                //    //IssuedUtc = <DateTimeOffset>,
                //    // The time at which the authentication ticket was issued.

                //    //RedirectUri = <string>
                //    // The full path or absolute URI to be used as an http 
                //    // redirect response value.
                //};

                //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                
                //return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("Views/Account/AccessDenied.cshtml");
        }
        [HttpGet]
        public IActionResult SessionExpired()
        {
            return View("Views/Account/SessionExpired.cshtml");
        }
    }
}