using Daithanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 namespace Daithanh.Controllers.Display.Section.introductions
{
    public class introductionsController : Controller
    {
        //
        // GET: /introductions/
        public ActionResult Index()
        {
            return View();
        }
        private DaithanhContext db = new Models.DaithanhContext();
        public ActionResult introductionDetail()
        {
            string tag = "gioi-thieu";
            tblGroupNew groupnews = db.tblGroupNews.FirstOrDefault(p => p.Tag == tag);
            int idCate = groupnews.id;

            var tblnews = db.tblNews.FirstOrDefault(p => p.idCate == idCate);
            if (tblnews!= null)
            {
                int idUser = int.Parse(tblnews.idUser.ToString());
                ViewBag.Username = db.tblUsers.Find(idUser).UserName;
                ViewBag.NameMenu = groupnews.Name;
                ViewBag.Title = "<title>" + tblnews.Title + "</title>";
                ViewBag.Description = "<meta name=\"description\" content=\"" + tblnews.Description + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblnews.Keyword + "\" /> ";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblnews.Title + "\" />";
                ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblnews.Description + "\" />";
                string meta = "";
                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctanadaithanh.vn/gioi-thieu\" />";

                meta += "<meta itemprop=\"name\" content=\"" + tblnews.Name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + tblnews.Description + "\" />";
                meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctanadaithanh.vn" + tblnews.Images + "\" />";
                meta += "<meta property=\"og:title\" content=\"" + tblnews.Title + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"http://Bonnuoctanadaithanh.vn" + tblnews.Images + "\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctanadaithanh.vn\" />";
                meta += "<meta property=\"og:description\" content=\"" + tblnews.Description + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />";
                ViewBag.Descriptionss = tblnews.Description;
                ViewBag.Meta = meta;
                int ids = int.Parse(tblnews.id.ToString());
           

            }
          
         
            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> giới thiệu" ;
            return View(tblnews);
        }

	}
}