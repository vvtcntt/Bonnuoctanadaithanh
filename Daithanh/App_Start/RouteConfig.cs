using Daithanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Daithanh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Product", "{tag}.html", new { controller = "product", action = "productList", tag = UrlParameter.Optional }, new { controller = "^p.*" }, new[] { "MyNamespace3" });
            routes.MapRoute("Chi_Tiet_San_Pham", "{tag}.htm", new { controller = "product", action = "productDetail", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productDetail$" });
            routes.MapRoute("NewsDetail", "news/{Tag}", new { controller = "news", action = "newsDetail", tag = UrlParameter.Optional }, new { controller = "^n.*", action = "^newsDetail$" });
            routes.MapRoute("Danh muc", "0/{Tag}-{id}.aspx", new { controller = "product", action = "productList", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productList$" });
            routes.MapRoute("chi tiet", "1/{Tag}-{id1}-{id}.aspx", new { controller = "product", action = "productDetail", tag = UrlParameter.Optional }, new { controller = "^p.*", action = "^productDetail$" });
            routes.MapRoute("chi tiet tin", "2/{Tag}-{id}.aspx", new { controller = "news", action = "newsDetail", tag = UrlParameter.Optional }, new { controller = "^n.*", action = "^newsDetail$" });
            routes.MapRoute("capacitydetail", "bon-nuoc/{Tag}", new { controller = "capacity", action = "capacityList", tag = UrlParameter.Optional }, new { controller = "^c.*", action = "^capacityList$" });

            //routes.MapRoute("Capacity1", "0/{tag}", new { controller = "Product", action = "ListCapacity", tag = UrlParameter.Optional, hang = UrlParameter.Optional }, new { controller = "^P.*" }, new[] { "MyNamespace2" });
            //routes.MapRoute("Capacity", "0/{tag}-dt", new { controller = "Product", action = "ListCapacity", tag = UrlParameter.Optional }, new { controller = "^P.*" }, new[] { "MyNamespace4" });
            //routes.MapRoute("Redrect", "Default.aspx/{Tabs}", new { controller = "Error", action = "Redriect", Tabs = UrlParameter.Optional }, new { controller = "^E.*", action = "^Redriect$" });
 
            routes.MapRoute("Baogia", "Bao-gia/{Tag}/{*catchall}", new { controller = "Baogia", action = "BaogiaDetail", tag = UrlParameter.Optional }, new { controller = "^B.*", action = "^BaogiaDetail$" });
            routes.MapRoute("Nha-phan-phoi", "Agency/{Tag}", new { controller = "agency", action = "agencyDetail", tag = UrlParameter.Optional }, new { controller = "^a.*", action = "^agencyDetail$" });
            routes.MapRoute("Nha-phan-phoi-1", "3/{Tag}-{id}.aspx", new { controller = "agency", action = "agencyDetail", tag = UrlParameter.Optional }, new { controller = "^a.*", action = "^agencyDetail$" });

            routes.MapRoute("ListNews", "2/{Tag}", new { controller = "News", action = "ListNews", tag = UrlParameter.Optional }, new { controller = "^N.*", action = "^ListNews$" });
            routes.MapRoute("TagNews", "TagNews/{Tag}/{*catchall}", new { controller = "news", action = "TagNews", tag = UrlParameter.Optional }, new { controller = "^n.*", action = "^TagNews$" });
             routes.MapRoute("TagAgency", "TagAgency/{Tag}/{*catchall}", new { controller = "Agency", action = "TagAgency", tag = UrlParameter.Optional }, new { controller = "^A.*", action = "^TagAgency$" });
            routes.MapRoute("TagProduct", "Tag/{Tag}/{*catchall}", new { controller = "Product", action = "productTag", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^productTag$" });
            routes.MapRoute("Tagcapacity", "TagCap/{Tag}/{*catchall}", new { controller = "Product", action = "TagCapacity", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^TagCapacity$" });
            routes.MapRoute("SearchProduct", "Search/{Tag}/{*catchall}", new { controller = "Product", action = "Search", tag = UrlParameter.Optional }, new { controller = "^P.*", action = "^Search$" });
            routes.MapRoute(name: "Bao-gia-tan-a-dai-thanh", url: "Bao-gia-tan-a-dai-thanh", defaults: new { controller = "Baogia", action = "baogiaList" });

            routes.MapRoute(name: "He-thong-phan-phoi", url: "He-thong-phan-phoi", defaults: new { controller = "Agency", action = "ListAgency" });
            routes.MapRoute(name: "Gioi-thieu", url: "Gioi-thieu", defaults: new { controller = "introductions", action = "introductionDetail" });
            routes.MapRoute(name: "Lien-he", url: "Lien-he", defaults: new { controller = "contact", action = "Index" });
            routes.MapRoute(name: "Ban-do", url: "Ban-do", defaults: new { controller = "Maps", action = "Index" });
            routes.MapRoute(name: "Gio-hang", url: "Gio-hang", defaults: new { controller = "Order", action = "OrderIndex" });
            routes.MapRoute(name: "Admin", url: "Admin", defaults: new { controller = "Login", action = "LoginIndex" });
            routes.MapRoute(
  name: "CmsRoute",
  url: "{*tag}",
  defaults: new { controller = "news", action = "newsList" },
  constraints: new { tag = new CmsUrlConstraint() }
);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
