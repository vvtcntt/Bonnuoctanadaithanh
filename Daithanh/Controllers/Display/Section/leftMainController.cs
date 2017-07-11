using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;
namespace Daithanh.Controllers.Display.Section
{
    public class leftMainController : Controller
    {
        private DaithanhContext db = new DaithanhContext();
        //
        // GET: /leftMain/
        public ActionResult Index()
        {
            
            return View();
        }
        public PartialViewResult partialMenuProduct()
        {
             var listMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listMenu.Count; i++)
            {
                result.Append("<li class=\"li1\">");
                result.Append("<a href=\"/" + listMenu[i].Tag + ".html\" title=\"" + listMenu[i].Name + "\"><img src=\"" + listMenu[i].Images + "\" alt=\"" + listMenu[i].Name + "\" /><span>" + listMenu[i].Name + "</span></a>");
                int id = listMenu[i].id;
                var listChild = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id).OrderBy(p => p.Ord).ToList();
                if(listChild.Count>0)
                {
                    result.Append("<ul class=\"ul2\">");
                    for (int j = 0; j < listChild.Count;j++ )
                    {
                        result.Append("<li class=\"li2\"><a href=\"/" + listChild[j].Tag + ".html\" title=\"" + listChild[j].Name + "\">" + listChild[j].Name + "</a></li>");
                    }
                        
                    result.Append("</ul>");
                }
               
                result.Append("</li>  ");
            }
            ViewBag.result = result.ToString();
            return PartialView();
        }
        public PartialViewResult partialHangGia()
        {
            var listImages = db.tblImages.Where(p => p.idCate == 3 && p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listImages.Count; i++)
            {
                result.Append(" <a href=\"" + listImages[i].Url + "\" title=\"" + listImages[i].Name + "\"><img src=\"" + listImages[i].Images + "\" alt=\"" + listImages[i].Name + "\" /></a>");
            }
            ViewBag.result = result.ToString();
            return PartialView();
        }
        public PartialViewResult partialNewsHomes()
        {
            var listNews = db.tblNews.Where(p => p.Active == true).OrderByDescending(p => p.DateCreate).Take(7).ToList();
            return PartialView(listNews);
        }
        public PartialViewResult adwPartial()
        {
            return PartialView(db.tblImages.Where(p=>p.Active==true && p.idCate==4).OrderBy(p=>p.Ord).ToList());
        }
	}
}