using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;
using PagedList;
namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "QUANTRI")]
    public class QuanLyHinhThucThanhToanController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyNhaSanXuat
        public ActionResult DanhSachHinhThucThanhToan(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listHinhThucThanhToan = db.HinhThucThanhToans.ToList();
            if (search != null)
            {
                listHinhThucThanhToan = db.HinhThucThanhToans.Where(x => x.TenHTTT.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listHinhThucThanhToan.OrderBy(n => n.MaHTTT).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemHinhThucThanhToan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemHinhThucThanhToan(HinhThucThanhToan hinhThucThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.HinhThucThanhToans.Add(hinhThucThanhToan);
                db.SaveChanges();
                return RedirectToAction("DanhSachHinhThucThanhToan");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaHinhThucThanhToan(int? MaHTTT)
        {
            if (MaHTTT == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.HinhThucThanhToans.SingleOrDefault(x => x.MaHTTT == MaHTTT);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaHinhThucThanhToan(HinhThucThanhToan hinhThucThanhToan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhThucThanhToan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachHinhThucThanhToan");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaHinhThucThanhToan(int? MaHTTT)
        {
            if (MaHTTT == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.HinhThucThanhToans.SingleOrDefault(x => x.MaHTTT == MaHTTT);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.HinhThucThanhToans.Remove(model);
            db.SaveChanges();
            return Content("<script>window.location.reload();</script>");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}