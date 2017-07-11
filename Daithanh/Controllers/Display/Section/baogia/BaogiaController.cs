﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Daithanh.Models;
using System.Text;
namespace Bonnuoc.Controllers.Display.Section.Baogia
{
    public class BaogiaController : Controller
    {
        DaithanhContext db = new DaithanhContext();
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
        // GET: Baogia
        public ActionResult BaogiaDetail(string tag)
        {
            tblGroupProduct tblgroupproduct = db.tblGroupProducts.FirstOrDefault(p => p.Tag == tag);
            ViewBag.Title = "<title> Bảng báo giá " + tblgroupproduct.Name + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"Bảng báo giá mới nhất dành cho sản phẩm " + tblgroupproduct.Name + " của Tân Á Đại Thành dành cho quý khách hàng\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblgroupproduct.Name + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblgroupproduct.Name + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + tblgroupproduct.Description + "\" />";
            int idCate = int.Parse(tblgroupproduct.id.ToString());
            ViewBag.name = tblgroupproduct.Name;
            List<string> Mang = new List<string>();
            Mang = Arrayid(idCate);
            if (Mang.Count == 0)
                Mang.Add(idCate.ToString());
            var listProduct = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString())).OrderBy(p => p.idCate).ToList();
            string chuoi = "";
            for (int i = 0; i < listProduct.Count; i++)
            {

                chuoi += "<tr>";
                chuoi += "<td class=\"Ords\">" + (i + 1) + "</td>";
                chuoi += "<td class=\"Names\"><a href=\"/" + listProduct[i].Tag + ".htm\" title=\"" + listProduct[i].Name + "\">" + listProduct[i].Name + "</a> </td>";
                chuoi += "<td class=\"Codes\">" + listProduct[i].Code + "</td>";
                chuoi += "<td class=\"Prices\">" + string.Format("{0:#,#}", listProduct[i].PriceSale) + "đ</td><td class=\"Qualitys\">01</td><td class=\"SumPrices\">" + string.Format("{0:#,#}", listProduct[i].PriceSale) + "đ</td>";
                chuoi += " <td class=\"Images\"><a href=\"/" + listProduct[i].Tag + ".htm\" title=\"" + listProduct[i].Name + "\"><img src=\"" + listProduct[i].ImageLinkThumb + "\" alt=\"" + listProduct[i].Name + "\" title=\"" + listProduct[i].Name + "\"></a></td>";
                chuoi += "</tr>";
            }
            ViewBag.chuoi = chuoi;
            //int id = tblgroupproduct.id;
            //var manufacture = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idCate select b).Take(1).ToList();
            //if(manufacture.Count>0)
            //{
            //    ViewBag.Namemanu = manufacture[0].Name;
            //    ViewBag.image = manufacture[0].Images;
            //}
           
            return View(db.tblConfigs.First());
        }
        public ActionResult baogiaList()
        {
            ViewBag.Title = "<title> Tổng hợp báo giá sản phẩm Tân Á Đại Thành tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year+"</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"bảng tổng hợp báo giá các sản phẩm tân á đại thành mới nhất dành cho khách hàng tham khảo\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"Báo giá tân á đại thành, báo giá bồn nước đại thành\" /> ";
            StringBuilder result = new StringBuilder();
           var listGroup = db.tblGroupProducts.Where(p=>p.Active==true && p.ParentID==null).ToList();
            StringBuilder resultBaogia = new StringBuilder();
            for (int i = 0; i < listGroup.Count;i++ )
            {
                result.Append(" <div class=\"Tear_News\">");
                result.Append("<a href=\"/bao-gia/" + listGroup[i].Tag + "\" title=\"Báo giá sản phẩm " + listGroup[i].Name + "\"><img src=\"/content/display/icon/iconFiles.jpg\" alt=\"Báo giá " + listGroup[i].Name + "\" title=\"Báo giá " + listGroup[i].Name + "\" /></a>");
                result.Append(" <a href=\"/bao-gia/" + listGroup[i].Tag + "\" title=\"Báo giá sản phẩm" + listGroup[i].Name + "\">Báo giá " + listGroup[i].Name + " mới nhất tháng "+DateTime.Now.Month+" năm "+DateTime.Now.Year+"  </a>");
                result.Append("<span class=\"date\"><span class=\"iCon\"></span>Thời gian gửi : "+DateTime.Now+"</span>");
                result.Append("</div>");
            }
            ViewBag.result = result.ToString();
            return View();
        }
    }
}