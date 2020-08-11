using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "QUANTRI")]
    public class QuanLyKhachHangController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyKhachHang
        public ActionResult DanhSachKhachHang(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listKhachHang = db.KhachHangs.ToList();
            if (search != null)
            {
                listKhachHang = db.KhachHangs.Where(x => x.TenKH.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listKhachHang.OrderBy(n => n.Makh).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SuaKhachHang(int? Makh)
        {
            if (Makh == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.KhachHangs.SingleOrDefault(x => x.Makh == Makh);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTV = new SelectList(db.ThanhViens.OrderBy(n => n.MaTV), "MaTV", "Hoten");
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaKhachHang(KhachHang KhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(KhachHang).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachKhachHang");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaKhachHang(int? Makh)
        {
            if (Makh == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.KhachHangs.SingleOrDefault(x => x.Makh == Makh);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.KhachHangs.Remove(model);
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