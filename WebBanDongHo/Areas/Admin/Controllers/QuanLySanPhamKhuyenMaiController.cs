using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "QUANTRI")]
    public class QuanLySanPhamKhuyenMaiController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        public ActionResult DanhSachKhuyenMai(int? page, string search)
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
        public ActionResult DanhSachSanPhamKhuyenMai(int? MaCTKM)
        {
            if (MaCTKM == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<SanPhamKhuyenMai> list = db.SanPhamKhuyenMais.Where(x => x.MACTKM == MaCTKM).ToList();
            ViewBag.MaCTKM = MaCTKM;
            return View(list);
        }
        public ActionResult ThemSanPhamKhuyenMai(int? MaCTKM)
        {
            if (MaCTKM == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var result = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
            IEnumerable<SanPhamKhuyenMai> list = db.SanPhamKhuyenMais.Where(x => x.MACTKM == MaCTKM).ToList();
            List<SanPham> listSanPham = db.SanPhams.ToList();
            if(list.Count() > 0)
            {
                foreach (SanPhamKhuyenMai item in list)
                {
                    SanPham model = db.SanPhams.SingleOrDefault(x => x.MaSP == item.MaSP);
                    if (model != null)
                    {
                        listSanPham.Remove(model);
                    }
                }
                ViewBag.listSanPham = listSanPham;
            }
            else
            {
                ViewBag.listSanPham = db.SanPhams;
            }
            
            return View(result);
        }
        [HttpPost]
        public ActionResult ThemSanPhamKhuyenMai(int? MaCTKM, IEnumerable<SanPhamKhuyenMai> sanPhamKhuyenMais)
        {
            ChuongTrinhKhuyenMai ctkm = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
            int soluongsp = (int)ctkm.SoLuongSanPham;
            List<SanPhamKhuyenMai> listSPKM = db.SanPhamKhuyenMais.Where(x => x.MACTKM == MaCTKM).ToList();
            if(listSPKM.Count > 0)
            {
                soluongsp = soluongsp - listSPKM.Count;
            }
            if((soluongsp - sanPhamKhuyenMais.Count()) > 0)
            {
                foreach (var item in sanPhamKhuyenMais)
                {
                    var sanphamkhuyenmai = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MACTKM == MaCTKM && x.MaSP == item.MaSP);
                    if (sanphamkhuyenmai == null)
                    {
                        item.MACTKM = MaCTKM;
                        db.SanPhamKhuyenMais.Add(item);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("DanhSachSanPhamKhuyenMai", new{ MaCTKM = MaCTKM });
            }
            else
            {
                ViewBag.ThongBao("Số lượng sản phẩm vượt quá số lương sản phẩm tối đa của chương trình!");
                var result = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.MaCTKM == MaCTKM);
                IEnumerable<SanPhamKhuyenMai> list = db.SanPhamKhuyenMais.Where(x => x.MACTKM == MaCTKM).ToList();
                List<SanPham> listSanPham = db.SanPhams.ToList();
                if (list.Count() > 0)
                {
                    foreach (SanPhamKhuyenMai item in list)
                    {
                        SanPham model = db.SanPhams.SingleOrDefault(x => x.MaSP != item.MaSP);
                        if (model != null)
                        {
                            listSanPham.Remove(model);
                        }
                    }
                    ViewBag.listSanPham = listSanPham;
                }
                else
                {
                    ViewBag.listSanPham = db.SanPhams;
                }
                return View(result);
            }
            
        }
        public ActionResult SuaSanPhamKhuyenMai(int? MaSPKM)
        {
            if (MaSPKM == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MaSPKM == MaSPKM);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaSanPhamKhuyenMai(SanPhamKhuyenMai spkm)
        {
            if (ModelState.IsValid)
            {
                var model = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MaSPKM == spkm.MaSPKM);
                if(model != null)
                {
                    model.GiaTriGiam = spkm.GiaTriGiam;
                    db.SaveChanges();
                }
                return RedirectToAction("DanhSachSanPhamKhuyenMai", new { MaCTKM = model.MACTKM });
            }
            ViewBag.ThongBao = "Có lỗi xảy ra!";
            return View();
        }
        public ActionResult XoaSanPhamKhuyenMai(int? MaSPKM)
        {
            if (MaSPKM == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.SanPhamKhuyenMais.SingleOrDefault(x => x.MaSPKM == MaSPKM);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.SanPhamKhuyenMais.Remove(model);
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