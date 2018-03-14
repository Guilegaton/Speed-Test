using Speed_Test.Models;
using Speed_Test.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Speed_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*
         * Get list of urls from sitemap
         */
        [HttpPost]
        public ActionResult JsonPrs(string id)
        {
            SiteMapParser siteMapParser = new SiteMapParser();
            return Json(siteMapParser.Test(id));
        }

        /*
         * Get timeots for each url
         */
        [HttpPost]
        public ActionResult JsonPrsTimeOuts(string url)
        {
            SiteMapParser siteMapParser = new SiteMapParser();
            return Json(siteMapParser.steedTest(url));
        }
        /*
        * Sort data and add to DB
        */
        [HttpPost]
        public JsonResult FinalSort(List<CheckUrl> SortList)
        {

            SiteMapParser siteMapParser = new SiteMapParser();
            return Json(siteMapParser.SortList(SortList));
        }

        public ActionResult History()
        {
            using (TestUrlModel _db = new TestUrlModel())
            {
                return View(_db.CheckUrls.ToList());
            }
        }

        [HttpPost]
        public ActionResult JsonGetUrls(string url)
        {
            using (TestUrlModel _db = new TestUrlModel())
            {
                var Base = _db.CheckUrls.ToList();
                if (!string.IsNullOrEmpty(url))
                {
                    var SortedBase = Base.Where(item => item.Host.Contains(url));
                    return Json(SortedBase);
                }
                return Json(Base);
            }
        }
    }
}