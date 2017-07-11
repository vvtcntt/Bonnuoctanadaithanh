using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;
using PagedList;
using PagedList.Mvc;
namespace Daithanh.Controllers.Display.Section.capacity
{
    public class capacityController : Controller
    {
        //
        // GET: /capacity/
        public ActionResult Index()
        {
            return View();
        }
        private DaithanhContext db = new DaithanhContext();
        public ActionResult capacityList(string tag, int? page)
        {
            tblCapacity capacitys = db.tblCapacities.FirstOrDefault(p => p.Tag == tag);
            int id = capacitys.id;
            ViewBag.h1 = "<h2>" + capacitys.Name + "</h2>";
            ViewBag.content = capacitys.Content;
            ViewBag.Headshort = capacitys.Content;
            ViewBag.Title = "<title>" + capacitys.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + capacitys.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + capacitys.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + capacitys.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://capacitys.vn/bon-nuoc/" + StringClass.NameToTag(tag) + "\" />";
            meta += "<meta itemprop=\"name\" content=\"" + capacitys.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + capacitys.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + capacitys.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://http://bonnuoctanadaithanh.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + capacitys.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›</ol><h1>" + capacitys.Title + "</h1>";
            var listProduct = db.tblProducts.Where(p=>p.Active==true && p.Capacity==id).ToList();
            StringBuilder result = new StringBuilder();
            result.Append(" <div class=\"trearProduct\">");
            result.Append(" <h2><a href=\"/bon-nuoc/" + capacitys.Tag + "\" title=\"" + capacitys.Name + "\">" + capacitys.Name + "</a></h2>");
            result.Append("<div class=\"contentTearProduct\">");
            int pageSize = 8;
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

             var listProducts = listProduct.ToPagedList(pageNumber, pageSize);
            for (int j = 0; j < listProducts.Count; j++)
            {
                result.Append("<div class=\"tear1\">");
                result.Append(" <div class=\"img\">");
                result.Append("<a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\"><img src=\"" + listProducts[j].ImageLinkThumb + "\" alt=\"" + listProducts[j].Name + "\" /></a>");
                result.Append("</div>");
                result.Append("<h3><a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\">" + listProducts[j].Name + "</a></h3>");
                result.Append("<div class=\"boxPrice\">");
                result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProducts[j].Price) + "đ</span>");
                result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProducts[j].PriceSale) + "đ</span>");
                result.Append(" </div>");
                result.Append("<div class=\"boxOrder\">");
                result.Append(" <div class=\"detail\"><a href=\"/" + listProducts[j].Tag + ".htm\" title=\"" + listProducts[j].Name + "\">Chi tiết</a></div>");
                result.Append(" <div class=\"order\"><a  rel=\"miendatwebPopup\" href=\"#popup_content\"  onclick=\"CreateOrder(" + listProducts[j].id + ")\" title=\"\"><i class=\"fa fa-shopping-cart\" aria-hidden=\"true\"></i> </a></div>");
                result.Append(" </div>");
                result.Append("  </div> ");
            }
            result.Append("</div>");
            result.Append("  </div>");
            ViewBag.result = result.ToString();

            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }
	}
}