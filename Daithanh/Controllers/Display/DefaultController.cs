using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;
namespace Daithanh.Controllers.Display
{
    public class DefaultController : Controller
    {
        private DaithanhContext db = new DaithanhContext();
        //
        // GET: /Default/
        public ActionResult Index()
        {
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + " giá sốc tháng "+DateTime.Now.Month+"/"+DateTime.Now.Year+"</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + " giá sốc tháng " + DateTime.Now.Month + "/" + DateTime.Now.Year + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";
            ViewBag.h1 = "<h1 class=\"h1\">" + config.Title + "</h1>";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctanadaithanh.vn\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + config.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + config.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctanadaithanh.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + config.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Bonnuoctanadaithanh.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctanadaithanh.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + config.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            return View();
        }
        public PartialViewResult partialdefault()
        {

            return PartialView(db.tblConfigs.First());
        }
        public PartialViewResult partialTransport()
        {
            return PartialView();
        }
        public ActionResult Action()
        {
        
            var listAgency = db.tblAgencies.ToList();
            for (int i = 0; i < listAgency.Count; i++)
            {
                int id = listAgency[i].id;
                tblAgency agencys = db.tblAgencies.Find(id);
                agencys.Tag =StringClass.NameToTag(agencys.Tag);
                db.SaveChanges();
               
               
            }
            
                return View();
        }
        public ActionResult slidePartial()
        {
            var listimageslide = db.tblImages.Where(p => p.Active == true && p.idCate == 2).OrderByDescending(p => p.Ord).Take(4).ToList();
            StringBuilder chuoislide = new StringBuilder();
            for (int i = 0; i < listimageslide.Count; i++)
            {
                if (i == 0)
                {
                    chuoislide.Append("url(" + listimageslide[i].Images + ") " + (910 * i) + "px 0 no-repeat");
                }
                else
                {
                    chuoislide.Append(", url(" + listimageslide[i].Images + ") " + (910 * i) + "px 0 no-repeat");
                }
            }
            ViewBag.chuoislide = chuoislide;
            return PartialView(listimageslide);
        }
	}
}