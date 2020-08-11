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
    public class QuanLyNhaCungCapController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyDoiTac
        public ActionResult DanhSachNhaCungCap(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listNhaCungCap = db.NhaCungCaps.ToList();
            if (search != null)
            {
                listNhaCungCap = db.NhaCungCaps.Where(x => x.TenNCC.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listNhaCungCap.OrderBy(n => n.MaNCC).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ThemNhaCungCap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemNhaCungCap(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(nhaCungCap);
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaCungCap");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaNhaCungCap(int? MaNCC)
        {
            if (MaNCC == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.NhaCungCaps.SingleOrDefault(x => x.MaNCC == MaNCC);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaNhaCungCap(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCungCap).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachNhaCungCap");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaNhaCungCap(int? MaNCC)
        {
            if (MaNCC == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.NhaCungCaps.SingleOrDefault(x => x.MaNCC == MaNCC);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.NhaCungCaps.Remove(model);
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