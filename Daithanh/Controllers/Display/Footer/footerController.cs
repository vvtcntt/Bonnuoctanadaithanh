using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;
namespace Daithanh.Controllers.Display.Footer
{
    public class footerController : Controller
    {
        //
        // GET: /footer/
        private DaithanhContext db = new DaithanhContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult footerPartial()
        {
            var listHotline = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            StringBuilder resultSuport = new StringBuilder();
            StringBuilder resultHotine = new StringBuilder();
            for (int i = 0; i < listHotline.Count;i++ )
            {
                result.Append(" <li><span>Chi nhánh "+listHotline[i].Name+":</span>");
                result.Append(" <div class=\"desc\">");
                result.Append("<p>" + listHotline[i].Address + " (<a class=\"map-hcm pointer\" title=\"\"><i class=\"fa fa-map-marker\" aria-hidden=\"true\"></i>xem bản đồ</a>)  </p>");
                result.Append("<p>Tel: " + listHotline[i].Mobile + " -" + listHotline[i].Hotline + "  -  E-mail: " + listHotline[i].Email + "</p>");
                result.Append("  </div>");
                result.Append(" </li>");
                resultSuport.Append("<p>Hỗ trợ khách hàng "+(i+1)+" <a class=\"icon\" href=\"\"></a> <span class=\"email\">"+listHotline[i].Email+"</span></p>");
                resultHotine.Append("<div class=\"Tear_Sp\">");
                resultHotine.Append(" <div class=\"Left_Tear_Sp\">");
                resultHotine.Append("</div>");
                resultHotine.Append("<div class=\"Right_Tear_Sp\">");
                resultHotine.Append(" <span class=\"sp1\">"+listHotline[i].Name+"</span>");
                resultHotine.Append("<span class=\"sp2\">"+listHotline[i].Hotline+" </span>");
                resultHotine.Append(" </div>");
                resultHotine.Append(" </div>");
            }
            ViewBag.result = result.ToString();
            ViewBag.resultSupport = resultSuport.ToString();
            ViewBag.resultHotline = resultHotine.ToString();
            var listGroup = db.tblGroupProducts.Where(p=>p.Active==true && p.ParentID==null).ToList();
            StringBuilder resultMenu = new StringBuilder();
            StringBuilder resultBaogia = new StringBuilder();
            for (int i = 0; i < listGroup.Count;i++ )
            {
                resultMenu.Append("<li><a href=\"/" + listGroup[i].Tag + ".html\" title=\"" + listGroup[i].Name + "\">" + listGroup[i].Name + "</a></li>");
                resultBaogia.Append("<li><a href=\"/Bao-gia/" + listGroup[i].Tag + "\" title=\"Báo giá " + listGroup[i].Name + "\">Báo giá " + listGroup[i].Name + "</a></li>");
            }
            ViewBag.resultMenu = resultMenu.ToString();
            ViewBag.resultBaogia = resultBaogia.ToString();
            return PartialView(db.tblConfigs.FirstOrDefault());
        }
	}
}