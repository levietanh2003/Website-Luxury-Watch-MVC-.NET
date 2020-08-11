using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "QUANTRI")]
    public class QuanLyThanhVienController :AdminBaseController 
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyThanhVien
        public ActionResult DanhSachThanhVien(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listThanhVien = db.ThanhViens.ToList();
            if (search != null)
            {
                listThanhVien = db.ThanhViens.Where(x => x.Hoten.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listThanhVien.OrderBy(n => n.MaTV).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemThanhVien()
        {
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiTV), "MaLoaiTV", "TenLoaiTV");
            return View();
        }
        [HttpPost]
        public ActionResult ThemThanhVien(ThanhVien ThanhVien)
        {
            if (ModelState.IsValid)
            {
                ThanhVien.MatKhau = MaHoa.MD5Hash("123456");
                db.ThanhViens.Add(ThanhVien);
                db.SaveChanges();
                return RedirectToAction("DanhSachThanhVien");
            }
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiTV), "MaLoaiTV", "TenLoaiTV");
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaThanhVien(int? MaTV)
        {
            if (MaTV == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.ThanhViens.SingleOrDefault(x => x.MaTV == MaTV);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiTV = new SelectList(db.LoaiThanhViens.OrderBy(n => n.MaLoaiTV), "MaLoaiTV", "TenLoaiTV");
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaThanhVien(ThanhVien ThanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ThanhVien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachThanhVien");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaThanhVien(int? MaTV)
        {
            if (MaTV == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.ThanhViens.SingleOrDefault(x => x.MaTV == MaTV);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.ThanhViens.Remove(model);
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