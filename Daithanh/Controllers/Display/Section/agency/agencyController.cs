using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using PagedList;
using PagedList.Mvc;
namespace Daithanh.Controllers.Display.Section.agency
{
    public class agencyController : Controller
    {
        //
        // GET: /agency/
        DaithanhContext db = new DaithanhContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult listAgency( int? page)
        {
            ViewBag.Title = "<title>Hệ thống phân phối tân á đại thành trên toàn quốc</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"Hệ thống phân phối  bồn nước Tân Á Đại Thành trên toàn quốc dành cho khách hàng\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"Bồn nước Tân Á Đại thành\" /> ";
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;
            var agency = db.tblAgencies.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            return View(agency.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult agencyDetail(string tag,int id=0)
        {
            
            var tblagency = db.tblAgencies.FirstOrDefault(p => p.Tag == tag);
            if(id>0)
            {
                return Redirect("/agency/"+tag);
            }
          

            ViewBag.Title = "<title>" + tblagency.Name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblagency.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblagency.Name + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblagency.Name + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblagency.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctanadaithanh.vn/agency/" + StringClass.NameToTag(tag) + "\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblagency.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblagency.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctanadaithanh.vn" + tblagency.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblagency.Name + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Bonnuoctanadaithanh.vn" + tblagency.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctanadaithanh.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblagency.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Descriptionss = tblagency.Description;
            ViewBag.Meta = meta;
            int ids = int.Parse(tblagency.id.ToString());
            if (tblagency.Tabs != null)
            {
                string Chuoi = tblagency.Tabs;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tabs = Mang[i].ToString();
                    var listnew = db.tblAgencies.Where(p => p.Tabs.Contains(tabs) && p.id != ids && p.Active == true).ToList();
                    for (int j = 0; j < listnew.Count; j++)
                    {
                        araylist.Add(listnew[j].id);
                    }
                }
                var Lienquan = db.tblAgencies.Where(p => araylist.Contains(p.id) && p.Active == true && p.id != ids).OrderByDescending(p => p.Ord).Take(3).ToList();
                string chuoinew = "";
                if (Lienquan.Count > 0)
                {

                    chuoinew += " <div class=\"Lienquan\">";
                    for (int i = 0; i > Lienquan.Count; i++)
                    {
                        chuoinew += "<a href=\"/agency/" + Lienquan[i].Tag + "\" title=\"" + Lienquan[i].Name + "\"> " + Lienquan[i].Name + "</a>";
                    }
                    chuoinew += "</div>";
                }
                ViewBag.chuoinew = chuoinew;


                //Load tin mới

            }

            string chuoinewnew = "";
            var NewsNew = db.tblAgencies.Where(p => p.Active == true && p.id != ids).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/agency/" + NewsNew[i].Tag + "\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;

            //load tag
            string chuoitag = "";
            if (tblagency.Tabs != null)
            {
                string Chuoi = tblagency.Tabs;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/tagAgency/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;

            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>"  ;
            return View(tblagency);
         }
	}
}