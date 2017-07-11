using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;

namespace Daithanh.Controllers.Display.Header
{
    public class headerController : Controller
    {
        //
        // GET: /header/
        private DaithanhContext db = new DaithanhContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult headerPartial()
        {
            var listMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listMenu.Count; i++)
            {
                result.Append("<li class=\"li2\">");
                result.Append("<a href=\"/" + listMenu[i].Tag + ".html\" title=\"" + listMenu[i].Name + "\">" + listMenu[i].Name + "</a>");
                int id = listMenu[i].id;
                var listChild = db.tblGroupProducts.Where(p => p.ParentID == id && p.Active == true).OrderBy(p => p.Ord).ToList();
                if (listChild.Count > 0)
                {
                    result.Append("<ul class=\"ul3\">");
                    for (int j = 0; j < listChild.Count; j++)
                    {
                        result.Append("<li class=\"li3\"><a href=\"/" + listChild[j].Tag + ".html\" title=\"" + listChild[j].Name + "\">" + listChild[j].Name + "</a></li>");
                    }
                    result.Append("</ul>");
                }
                result.Append(" </li>");
            }
            ViewBag.result = result.ToString();
            var listHotline = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder resultHotline = new StringBuilder();
            for (int i = 0; i < listHotline.Count;i++ )
            {
                resultHotline.Append("" + listHotline[i].Name + ":  <span>" + listHotline[i].Mobile + " - " + listHotline[i].Hotline + "</span>");
            }
            ViewBag.resultHotline = resultHotline.ToString();
                return PartialView(db.tblConfigs.FirstOrDefault());
        }
        public ActionResult CommandSearch(FormCollection collection)
        {
            Session["Search"] = collection["txtSearch"];

            return Redirect("/Product/productSearch");
        }
	}
}