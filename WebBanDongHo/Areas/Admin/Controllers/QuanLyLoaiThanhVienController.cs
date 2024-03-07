using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyLoaiThanhVienController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/Quanlyloaithanhvien
        public ActionResult DanhSachLoaiThanhVien(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listLoaiThanhVien = db.LoaiThanhViens.ToList();
            if (search != null)
            {
                listLoaiThanhVien = db.LoaiThanhViens.Where(x => x.TenLoaiTV.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listLoaiThanhVien.OrderBy(n => n.MaLoaiTV).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemLoaiThanhVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiThanhVien(LoaiThanhVien loaiThanhVien)
        {
            if (ModelState.IsValid)
            {
                db.LoaiThanhViens.Add(loaiThanhVien);
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiThanhVien");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaLoaiThanhVien(int? MaLoaiTV)
        {
            if (MaLoaiTV == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == MaLoaiTV);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaLoaiThanhVien(LoaiThanhVien loaiThanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiThanhVien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachLoaiThanhVien");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaLoaiThanhVien(int? MaLoaiTV)
        {
            if (MaLoaiTV == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == MaLoaiTV);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.LoaiThanhViens.Remove(model);
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