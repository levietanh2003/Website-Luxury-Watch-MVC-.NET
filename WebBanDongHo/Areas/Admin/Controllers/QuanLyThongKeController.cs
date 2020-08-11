using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "QUANTRI")]
    public class QuanLyThongKeController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyThongKe
        public ActionResult Index()
        {
            ViewBag.ThanhVien = db.ThanhViens.Count();
            List<DonDatHang> list = db.DonDatHangs.Where(x => x.HoanThanh == true && x.DaHuy == false && x.DaThanhToan == true).ToList();
            decimal doanhso = 0;
            foreach(var item in list)
            {
                doanhso += (decimal)item.TongThanhToan;
            }
            ViewBag.DoanhSo = doanhso.ToString("#,##");
            ViewBag.DonDatHang = db.DonDatHangs.Count();
            ViewBag.Online = HttpContext.Application["Online"];
            return View();
        }
    }
}