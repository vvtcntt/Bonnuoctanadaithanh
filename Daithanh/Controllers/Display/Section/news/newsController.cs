using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
 using PagedList;
using PagedList.Mvc;
namespace Daithanh.Controllers.Display.Section.news
{
    public class newsController : Controller
    {
        //
        // GET: /news/
        private DaithanhContext db = new DaithanhContext();
        public ActionResult Index()
        {
            return View();
        }
        string nUrl = "";
        public string UrlNews(int idCate)
        {
            var ListMenu = db.tblGroupNews.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/" + ListMenu[i].Tag + "\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlNews(id);
                }

            }
            return nUrl;
        }
        public ActionResult newsDetail(string tag, int id = 0)
        {
            var tblnews = db.tblNews.FirstOrDefault(p => p.Tag == tag);
            if (tblnews == null)
                tblnews = db.tblNews.FirstOrDefault(p => p.id == id);
            if(id>0)
            {
                return Redirect("/news/"+tag);
            }
            int idUser = int.Parse(tblnews.idUser.ToString());
            ViewBag.Username = db.tblUsers.Find(idUser).UserName;
            int idCate = int.Parse(tblnews.idCate.ToString());
            var groupnews = db.tblGroupNews.First(p => p.id == idCate);
            ViewBag.NameMenu = groupnews.Name;
            ViewBag.Title = "<title>" + tblnews.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblnews.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblnews.Keyword + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblnews.Title + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblnews.Description + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctanadaithanh.vn/news/" + StringClass.NameToTag(tag) + "\" />";

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
            if (tblnews.Keyword != null)
            {
                string Chuoi = tblnews.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tabs = Mang[i].ToString();
                    var listnew = db.tblNews.Where(p => p.Keyword.Contains(tabs) && p.id != ids && p.Active == true).ToList();
                    for (int j = 0; j < listnew.Count; j++)
                    {
                        araylist.Add(listnew[j].id);
                    }
                }
                var Lienquan = db.tblNews.Where(p => araylist.Contains(p.id) && p.Active == true && p.id != ids).OrderByDescending(p => p.Ord).Take(3).ToList();
                string chuoinew = "";
                if (Lienquan.Count > 0)
                {

                    chuoinew += " <div class=\"Lienquan\">";
                    for (int i = 0; i > Lienquan.Count; i++)
                    {
                        chuoinew += "<a href=\"/news/" + Lienquan[i].Tag + "\" title=\"" + Lienquan[i].Name + "\"> " + Lienquan[i].Name + "</a>";
                    }
                    chuoinew += "</div>";
                }
                ViewBag.chuoinew = chuoinew;


                //Load tin mới

            }

            string chuoinewnew = "";
            var NewsNew = db.tblNews.Where(p => p.Active == true && p.id != ids).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/news/" + NewsNew[i].Tag + "\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;

            //load tag
            string chuoitag = "";
            if (tblnews.Keyword != null)
            {
                string Chuoi = tblnews.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/TagNews/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;

            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlNews(idCate);
            return View(tblnews);
        }
        public PartialViewResult leftNewsPartial()
        {
            return PartialView();
        }
        public ActionResult newsList(string tag, int? page)
        {
            var groupnew = db.tblGroupNews.First(p => p.Tag == tag);
            int idcate = groupnew.id;
            var listnews = db.tblNews.Where(p => p.idCate == idcate && p.Active == true).OrderByDescending(p => p.Ord).ToList();
            string chuoinewnew = "";
            var NewsNew = db.tblNews.Where(p => p.Active == true && p.idCate != idcate).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/news/" + NewsNew[i].Tag + "\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;
            string chuoichinhsach = "";
            var Chinhsach = db.tblNews.Where(p => p.Active == true && p.idCate == 3).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < Chinhsach.Count; i++)
            {
                chuoichinhsach += "<li><a href=\"/news/" + Chinhsach[i].Tag + "\" title=\"" + Chinhsach[i].Name + "\" rel=\"nofollow\"> " + Chinhsach[i].Name + " <span>" + Chinhsach[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoichinhsach = chuoichinhsach;
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
            ViewBag.Name = groupnew.Name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlNews(groupnew.id);
            ViewBag.Title = "<title>" + groupnew.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + groupnew.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + groupnew.Keyword + "\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TagNews(string tag, int? page)
        {
            var tbltags = db.tblNewsTags.Where(p => p.Tag == tag).ToList();
            string name = tbltags[0].Name;
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
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
            var listId = db.tblNewsTags.Where(p => p.Tag == tag).Select(p => p.idn).ToList();
            var listnews = db.tblNews.Where(p => p.Active == true && listId.Contains(p.id)).OrderByDescending(p => p.DateCreate).ToList();
            ViewBag.Name = name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> " + name + "";
            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }
	}
}