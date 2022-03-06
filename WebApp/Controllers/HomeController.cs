using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebApp.Areas.Ref.Models ;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private string _path_view = "/Views/Home/";

        public IActionResult Index()
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
            //if (isMobile == true && MobileDevice == true)
            if (isMobile == true)
            {
                return RedirectToAction("Index", "Pwa");
            }
            else {
                //if (SecurityHelper.onPageInit(HttpContext))
                //{
                //    string baseUrl = WebHelper.GetBaseUrl(HttpContext);
                //    ViewData["baseUrl"] = baseUrl;
                //    ViewData["tahun"] = DateTime.Now.Year.ToString();
                //    PersonData personData = SecurityHelper.GetPersonData(HttpContext);
                //    ViewData["CarouselData"] = HomeModel.GetCarouselData(HttpContext);
                //    return View(_path_view + "Index.cshtml");
                //}
                //else
                //{
                //    return RedirectToAction("Login", "Account");
                //}
                string ConnString = Settings.GetConnectionString("MainConnection").ToString();
                return Ok(ConnString);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
