using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;
using PagedList;
using PagedList.Mvc;
namespace Daithanh.Controllers.Display.Section.product
{
    public class productController : Controller
    {
        //
        // GET: /product/
        private DaithanhContext db = new DaithanhContext();
        public ActionResult Index()
        {
            return View();
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = "<li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" title=\"" + ListMenu[i].Title + "\" href=\"/" + ListMenu[i].Tag + ".html\"> <span itemprop=\"name\">" + ListMenu[i].Name + "</span></a> <meta itemprop=\"position\" content=\"" + (ListMenu[i].Level + 2) + "\" /> </li> ›" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        public PartialViewResult listProductHomesPartial()
        {
            var listGroup = db.tblGroupProducts.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < listGroup.Count;i++ )
            {
                result.Append(" <div class=\"trearProduct\">");
                result.Append(" <h2><a href=\"/" + listGroup[i].Tag + ".html\" title=\"" + listGroup[i].Name + "\">" + listGroup[i].Name + "</a></h2>");
                result.Append("<div class=\"contentTearProduct\">");
                int id = listGroup[i].id;
                List<string> mangID = new List<string>();
                mangID = Arrayid(id);
                mangID.Add(id.ToString());
                var listProduct = db.tblProducts.Where(p => p.Active == true && mangID.Contains(p.idCate.ToString()) && p.ViewHomes==true).OrderBy(p => p.idCate).ToList();
                for (int j = 0; j < listProduct.Count;j++ )
                {
                    result.Append("<div class=\"tear1\">");
                    if(listProduct[j].New==true)
                    {
                      result.Append("<div class=\"new\"><span>New</span></div>");
                    }
                    result.Append(" <div class=\"img\">");
                    result.Append("<a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                    result.Append("</div>");
                    result.Append("<h3><a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\">" + listProduct[j].Name + "</a></h3>");
                    result.Append("<div class=\"boxPrice\">");
                    result.Append("<span class=\"price\">" + string.Format("{0:#,#}",listProduct[j].Price) + "đ</span>");
                    result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                    result.Append(" </div>");
                    result.Append("<div class=\"boxOrder\">");
                    result.Append(" <div class=\"detail\"><a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\">Chi tiết</a></div>");
                    result.Append(" <div class=\"order\"><a  rel=\"miendatwebPopup\" href=\"#popup_content\"  onclick=\"CreateOrder("+listProduct[j].id+")\" title=\"\"><i class=\"fa fa-shopping-cart\" aria-hidden=\"true\"></i> </a></div>");
                    result.Append(" </div>");
                    result.Append("  </div> ");
                }      
                result.Append("</div>");
                result.Append("  </div>");
                Mangphantu.Clear();
            }
            ViewBag.result = result.ToString();
                return PartialView();
        }
        public ActionResult productDetail(string tag, int id = 0)
        {
            var products = db.tblProducts.FirstOrDefault(p => p.Tag == tag);
            if (products == null)
            {
                products = db.tblProducts.FirstOrDefault(p => p.id == id);
            }
            if(id>0)
            {
                return Redirect("/"+tag+".htm");
            }
            ViewBag.Title = "<title>" + products.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + products.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + products.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + products.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://Bonnuoctanadaithanh.vn/" + StringClass.NameToTag(tag) + ".htm\" />";
            string meta = "";
            products.Visit = products.Visit + 1;
            db.SaveChanges();
            meta += "<meta itemprop=\"name\" content=\"" + products.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + products.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://Bonnuoctanadaithanh.vn" + products.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + products.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://Bonnuoctanadaithanh.vn" + products.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctanadaithanh.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + products.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; int idcate = int.Parse(products.idCate.ToString());
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›" + UrlProduct(idcate) + "</ol><h1>" + products.Title + "</h1>";
            //var tblManu = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b).Take(1).ToList();
            //int idManu = 0;
            //if (tblManu.Count > 0)
            //{
            //    ViewBag.manu = tblManu[0].Name;
            //    ViewBag.urlmanu = tblManu[0].Images;
            //    idManu = tblManu[0].id;
            //    ViewBag.Baohanh = tblManu[0].Tag;

            //}
            string chuoitag = "";
            if (products.Keyword != null)
            {
                string Chuoi = products.Keyword;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag += "<h2><a href=\"/Tag/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            float Price = float.Parse(products.Price.ToString());
            float PriceSale = float.Parse(products.PriceSale.ToString());
            ViewBag.tietkiem = string.Format("{0:#,#}", Convert.ToInt32(Price - PriceSale));
            int idcap = 0;
            if (products.Capacity.ToString() != null && products.Capacity.ToString() != "")
            {
                idcap = int.Parse(products.Capacity.ToString());
                var tblcapacity = db.tblCapacities.Find(idcap);
                ViewBag.capa = " <a href=\"/bon-nuoc/"+tblcapacity.Tag+"\" title=\"" + tblcapacity.Capacity + "\">" + tblcapacity.Capacity + "</a>"; 
                ViewBag.cappacity = tblcapacity.Capacity;
                ViewBag.songuoisd = tblcapacity.Note;
            }
            //Load tính năng
            ViewBag.tendanhmuc = db.tblGroupProducts.Find(idcate).Name;
            ViewBag.video = "<iframe width = \"100%\" height=\"680\" src=\"https://www.youtube.com/embed/" + db.tblGroupProducts.Find(idcate).Video + "?rel=0&amp;showinfo=0\" frameborder=\"0\" allowfullscreen></iframe>";
            var ListGroupCri = db.tblGroupCriterias.Where(p => p.idCate == idcate).ToList();
            List<int> Mang1 = new List<int>();
            for (int i = 0; i < ListGroupCri.Count; i++)
            {
                Mang1.Add(int.Parse(ListGroupCri[i].idCri.ToString()));
            }

            int idp = int.Parse(products.id.ToString());
            var ListCri = db.tblCriterias.Where(p => Mang1.Contains(p.id) && p.Active == true).OrderBy(p => p.Ord).ToList();
            string chuoi = "";
            #region[Lọc thuộc tính]
            for (int i = 0; i < ListCri.Count; i++)
            {
                int idCre = int.Parse(ListCri[i].id.ToString());
                var ListCr = (from a in db.tblConnectCriterias
                              join b in db.tblInfoCriterias on a.idCre equals b.id
                              where a.idpd == idp && b.idCri == idCre && b.Active == true
                              select new
                              {
                                  b.Name,
                                  b.Url,
                                  b.Ord
                              }).OrderBy(p => p.Ord).ToList();
                if (ListCr.Count > 0)
                {
                    if (ListCri[i].Style == true)
                        chuoi += "<tr class=\"bold\">";
                    else
                        chuoi += "<tr>";
                    chuoi += "<td>" + ListCri[i].Name + "</td>";
                    chuoi += "<td>";
                    int dem = 0;
                    string num = "";
                    if (ListCr.Count > 1)
                        num = "⊹ ";
                    foreach (var item in ListCr)
                        if (item.Url != null && item.Url != "")
                        {
                            chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">";
                            if (dem == 1)
                                chuoi += num + item.Name;
                            else
                                chuoi += num + item.Name;
                            dem = 1;
                            chuoi += "</a>";
                        }
                        else
                        {
                            if (dem == 1)
                                chuoi += num + item.Name + "</br> ";
                            else
                                chuoi += num + item.Name + "</br> "; ;
                            dem = 1;
                        }
                    chuoi += "</td>";
                    chuoi += " </tr>";
                }
            }
            #endregion
            ViewBag.chuoi = chuoi;
            var listimage = db.tblImageProducts.Where(p => p.idProduct == idp).ToList();
            if (listimage.Count > 0)
            {
                StringBuilder chuoiimages = new StringBuilder();
                chuoiimages.Append(" <a title=\"" + products.Name + "\" onclick=\"javascript:return SetIMG('" + products.ImageLinkDetail + "','an0" + products.id + "');\"><img class=\"an0" + products.id + "\" src=\"" + products.ImageLinkThumb + "\" /></a> ");
                for (int i = 0; i < listimage.Count; i++)
                {
                    chuoiimages.Append(" <a title=\"" + listimage[i].Name + "\" onclick=\"javascript:return SetIMG('" + listimage[i].Images + "','an" + listimage[i].idProduct + "');\"><img class=\"an" + listimage[i].idProduct + "\" src=\"" + listimage[i].Images + "\" /></a> ");
                }
                ViewBag.chuoiimage = chuoiimages;
            }
            ViewBag.hotline = db.tblConfigs.First().Hotline;
            var listImages = db.tblImages.Where(p => p.Active == true && p.idCate == 11).OrderByDescending(p => p.Ord).ToList();
            StringBuilder chuoiimage = new StringBuilder();
            for (int i = 0; i < listImages.Count; i++)
            {
                chuoiimage.Append("<a href=\"" + listImages[i].Url + "\" title=\"" + listImages[i].Name + "\"><img src=\"" + listImages[i].Images + "\" alt=\"" + listImages[i].Name + "\" /></a>");
            }
            ViewBag.chuoiimages = chuoiimage;
           
            return View(products);
        }
        public ActionResult productList(string tag, int? page,int id=0)
        {
            var Groupproduct = db.tblGroupProducts.FirstOrDefault(p => p.Tag == tag);
            if(id>0)
            {
                return Redirect("/" + tag+".html");
            }            
            id = Groupproduct.id;
            ViewBag.h1 = "<h2>" + Groupproduct.Name + "</h2>";
            ViewBag.content = Groupproduct.Content;
            ViewBag.Headshort = Groupproduct.Content;
            ViewBag.Title = "<title>" + Groupproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Groupproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + Groupproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Groupproduct.Keyword + "\" /> ";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoctanadaithanh.vn/" + StringClass.NameToTag(tag) + ".html\" />";
            meta += "<meta itemprop=\"name\" content=\"" + Groupproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + Groupproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + Groupproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://http://bonnuoctanadaithanh.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + Groupproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›" + UrlProduct(id) + "</ol><h1>" + Groupproduct.Title + "</h1>";
            var listProduct = db.tblProducts.Take(0).ToList();
            //Phân trang
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
            //Kết thúc phân trang
            StringBuilder result = new StringBuilder();
            var listGroup = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == id).OrderBy(p => p.Ord).ToList();
            if(listGroup.Count>0)
            {
                
                for (int i = 0; i < listGroup.Count; i++)
                {
                    result.Append(" <div class=\"trearProduct\">");
                    result.Append(" <h2><a href=\"/" + listGroup[i].Tag + ".html\" title=\"" + listGroup[i].Name + "\">" + listGroup[i].Name + "</a></h2>");
                    result.Append("<div class=\"contentTearProduct\">");
                    int ids = listGroup[i].id;
                    List<string> mangID = new List<string>();
                    mangID = Arrayid(ids);
                    mangID.Add(ids.ToString());
                      listProduct = db.tblProducts.Where(p => p.Active == true && mangID.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).Take(12).ToList();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        result.Append("<div class=\"tear1\">");
                        if (listProduct[j].New == true)
                        {
                            result.Append("<div class=\"new\"><span>New</span></div>");
                        }
                        result.Append(" <div class=\"img\">");
                        result.Append("<a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\"><img src=\"" + listProduct[j].ImageLinkThumb + "\" alt=\"" + listProduct[j].Name + "\" /></a>");
                        result.Append("</div>");
                        result.Append("<h3><a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\">" + listProduct[j].Name + "</a></h3>");
                        result.Append("<div class=\"boxPrice\">");
                        result.Append("<span class=\"price\">" + string.Format("{0:#,#}", listProduct[j].Price) + "đ</span>");
                        result.Append("<span class=\"priceSale\">" + string.Format("{0:#,#}", listProduct[j].PriceSale) + "đ</span>");
                        result.Append(" </div>");
                        result.Append("<div class=\"boxOrder\">");
                        result.Append(" <div class=\"detail\"><a href=\"/" + listProduct[j].Tag + ".htm\" title=\"" + listProduct[j].Name + "\">Chi tiết</a></div>");
                        result.Append(" <div class=\"order\"><a  rel=\"miendatwebPopup\" href=\"#popup_content\"  onclick=\"CreateOrder(" + listProduct[j].id + ")\" title=\"\"><i class=\"fa fa-shopping-cart\" aria-hidden=\"true\"></i> </a></div>");
                        result.Append(" </div>");
                        result.Append("  </div> ");
                    }
                    result.Append("</div>");
                    result.Append("  </div>");
                    Mangphantu.Clear();
                }
             }
            else
            {
                result.Append(" <div class=\"trearProduct\">");
                result.Append(" <h2><a href=\"/" + Groupproduct.Tag + ".html\" title=\"" + Groupproduct.Name + "\">" + Groupproduct.Name + "</a></h2>");
                result.Append("<div class=\"contentTearProduct\">");
                 List<string> mangID = new List<string>();
                mangID = Arrayid(id);
                mangID.Add(id.ToString());
                  listProduct = db.tblProducts.Where(p => p.Active == true && mangID.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).ToList();
               var listProducts=listProduct.ToPagedList(pageNumber, pageSize);
                for (int j = 0; j < listProducts.Count; j++)
                {
                    result.Append("<div class=\"tear1\">");
                    if (listProduct[j].New == true)
                    {
                        result.Append("<div class=\"new\"><span>New</span></div>");
                    }
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
                Mangphantu.Clear();
            }
            ViewBag.result = result.ToString();

            return View(listProduct.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult productTag(string tag)
        {
            var tbltags = db.tblProductTags.Where(p => p.Tag == tag).ToList();
            ViewBag.Title = "<title>" + tbltags[0].Name + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tbltags[0].Name + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tbltags[0].Name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tbltags[0].Name + "\" /> ";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + tbltags[0].Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tbltags[0].Name + "\" />";
            meta += "<meta itemprop=\"image\" content=\"\" />";
            meta += "<meta property=\"og:title\" content=\"" + tbltags[0].Name + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctanadaithanh.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + tbltags[0].Name + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            StringBuilder result = new StringBuilder();   
            var listId = db.tblProductTags.Where(p => p.Tag == tag).Select(p => p.idp).ToList();
            var listProducts = db.tblProducts.Where(p => p.Active == true && listId.Contains(p.id)).OrderBy(p => p.PriceSale).ToList();
            ViewBag.h1 = tbltags[0].Name;
            result.Append(" <div class=\"trearProduct\">");
            result.Append(" <h2></h2>");
            result.Append("<div class=\"contentTearProduct\">");
            for (int j = 0; j < listProducts.Count; j++)
            {
                result.Append("<div class=\"tear1\">");
                if (listProducts[j].New == true)
                {
                    result.Append("<div class=\"new\"><span>New</span></div>");
                }
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
            ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›<li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"" + Request.Url + "\"> <span itemprop=\"name\">" + tbltags[0].Name + "</span></a> <meta itemprop=\"position\" content=\"2\" /> </li> </ol><h1>" + tbltags[0].Name + "</h1";

            return View();
        }
        public ActionResult productSearch( )
        {
            if (Session["Search"] != null && Session["Search"] != "")
            {
                string tag = Session["Search"].ToString();
                var product = db.tblProducts.Where(p => p.Name.Contains(tag) || p.Description.Contains(tag) && p.Active == true).OrderBy(p => p.Ord).ToList();
                if (product.Count > 0)
                {
                    ViewBag.Title = "<title>" + tag + "</title>";
                    ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tag + "\" />";
                    ViewBag.Description = "<meta name=\"description\" content=\"" + tag + "\"/>";
                    ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
                    string meta = "";
                    meta += "<meta itemprop=\"name\" content=\"" + tag + "\" />";
                    meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                    meta += "<meta itemprop=\"description\" content=\"" + tag + "\" />";
                    meta += "<meta itemprop=\"image\" content=\"\" />";
                    meta += "<meta property=\"og:title\" content=\"" + tag + "\" />";
                    meta += "<meta property=\"og:type\" content=\"product\" />";
                    meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                    meta += "<meta property=\"og:image\" content=\"\" />";
                    meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoctanadaithanh.vn\" />";
                    meta += "<meta property=\"og:description\" content=\"" + tag + "\" />";
                    meta += "<meta property=\"fb:admins\" content=\"\" />";
                    StringBuilder result = new StringBuilder();
                    var listId = db.tblProductTags.Where(p => p.Tag == tag).Select(p => p.idp).ToList();
                    var listProducts = product;
                    ViewBag.h1 = tag;

                    result.Append(" <div class=\"trearProduct\">");
                    result.Append(" <h2></h2>");
                    result.Append("<div class=\"contentTearProduct\">");
                    for (int j = 0; j < listProducts.Count; j++)
                    {
                        result.Append("<div class=\"tear1\">");
                        if (listProducts[j].New == true)
                        {
                            result.Append("<div class=\"new\"><span>New</span></div>");
                        }
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
                    ViewBag.result = result.ToString(); ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li> ›<li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"> <a itemprop=\"item\" href=\"" + Request.Url + "\"> <span itemprop=\"name\">" + tag + "</span></a> <meta itemprop=\"position\" content=\"2\" /> </li> </ol><h1>" + tag + "</h1";
                    Session["Search"] = "";
                }
            }
            else
            {
                ViewBag.nUrl = " <ol itemscope itemtype=\"http://schema.org/BreadcrumbList\" >  <li itemprop=\"itemListElement\" itemscope itemtype=\"http://schema.org/ListItem\"><a itemprop=\"item\" href=\"/\"> <span itemprop=\"name\">Trang chủ</span></a> <meta itemprop=\"position\" content=\"1\" />  </li>";

            }

            return View();
        }
	}
}