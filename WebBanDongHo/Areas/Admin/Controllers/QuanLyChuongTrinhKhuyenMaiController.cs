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
    public class QuanLyChuongTrinhKhuyenMaiController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: Admin/QuanLyChuongTrinhKhuyenMai
        public ActionResult DanhSachChuongTrinhKhuyenMai(int? page, string search)
        {
            //Tạo biến số phần tử trên trang
            int pageSize = 5;
            //Tạo biến số trang
            int pageNumber = (page ?? 1);
            var listChuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.ToList();
            if (search != null)
            {
                listChuongTrinhKhuyenMai = db.ChuongTrinhKhuyenMais.Where(x => x.TenCTKM.Contains(search)).ToList();
                ViewBag.search = search;
            }
            return View(listChuongTrinhKhuyenMai.OrderBy(n => n.MaCTKM).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemChuongTrinhKhuyenMai()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai model)
        {
            if (ModelState.IsValid)
            {
                if (model.ApDung == true)
                {
                    List<ChuongTrinhKhuyenMai> list = db.ChuongTrinhKhuyenMais.Where(x => x.ApDung == true).ToList();
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.ApDung = false;
                            db.SaveChanges();
                        }
                    }
                }
                db.ChuongTrinhKhuyenMais.Add(model);
                db.SaveChanges();
                return RedirectToAction("DanhSachChuongTrinhKhuyenMai");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult SuaChuongTrinhKhuyenMai(int? MaCTKM)
        {
            if (MaCTKM == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaChuongTrinhKhuyenMai(ChuongTrinhKhuyenMai model)
        {
            if (ModelState.IsValid)
            {
                if(model.ApDung == true)
                {
                    List<ChuongTrinhKhuyenMai> list = db.ChuongTrinhKhuyenMais.Where(x => x.ApDung == true && x.MaCTKM != model.MaCTKM).ToList();
                    if(list.Count > 0)
                    {
                        foreach(var item in list)
                        {
                            item.ApDung = false;
                            db.SaveChanges();
                        }
                    }
                }
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachChuongTrinhKhuyenMai");
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaChuongTrinhKhuyenMai(int? MaCTKM)
        {
            if (MaCTKM == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.ChuongTrinhKhuyenMais.Remove(model);
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