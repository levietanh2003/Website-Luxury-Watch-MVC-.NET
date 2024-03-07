using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;
using PagedList;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyNhaSanXuatController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyNhaSanXuat
        public ActionResult DanhSachNhaSanXuat(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listNhaSanXuat = db.NhaSanXuats.ToList();
            if (search != null)
            {
                listNhaSanXuat = db.NhaSanXuats.Where(x => x.TenNSX.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listNhaSanXuat.OrderBy(n => n.MaNSX).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemNhaSanXuat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhaSanXuat(NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.NhaSanXuats.Add(nhaSanXuat);
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaSanXuat");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaNhaSanXuat(int? MaNSX)
        {
            if (MaNSX == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.NhaSanXuats.SingleOrDefault(x => x.MaNSX == MaNSX);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaNhaSanXuat(NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaSanXuat).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaSanXuat");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaNhaSanXuat(int? MaNSX)
        {
            if (MaNSX == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.NhaSanXuats.SingleOrDefault(x => x.MaNSX == MaNSX);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.NhaSanXuats.Remove(model);
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