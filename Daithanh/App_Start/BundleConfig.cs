using System.Web;
using System.Web.Optimization;

namespace Daithanh
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/Display/style/css").Include(
                    "~/Content/Display/awesome/css/font-awesome.min.css", "~/Content/bootstrap.min.css", 
                    "~/Content/Display/style/default.css",
                    "~/Content/Display/style/default_Rs.css",
                           "~/Content/Display/style/slide.css",
                    "~/Content/Display/style/news.css",
                    "~/Content/Display/style/news_Rs.css",
                    "~/Content/Display/style/product.css",
                       "~/Content/Display/style/Popup.css",
                         "~/Content/Display/style/Order.css",
                         "~/Content/PagedList.css",
                           "~/Content/Display/style/Order_Rs.css",
                           "~/Content/Display/style/contact.css",
                           "~/Content/Display/style/contact_Rs.css",
                           "~/Content/Display/style/baogia.css",
                           "~/Content/Display/style/baogia_Rs.css",
                    "~/Content/Display/style/product_Rs.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
