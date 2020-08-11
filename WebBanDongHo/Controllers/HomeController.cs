using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Entities;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;

namespace WebBanDongHo.Controllers
{
    public class HomeController : Controller
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        public ActionResult Index()
        {
            DateTime date = DateTime.Now;
            ChuongTrinhKhuyenMai CTKM = db.ChuongTrinhKhuyenMais.SingleOrDefault(x => x.NGgayKetThuc > date && x.ApDung == true);
            List<SanPham> LSP = db.SanPhams.ToList();
            if(CTKM != null)
            {
                ViewBag.CTKM = CTKM;
                List<SanPhamKhuyenMai> LSPKM = db.SanPhamKhuyenMais.Where(x => x.MACTKM == CTKM.MaCTKM).ToList();
                ViewBag.listSPKM = db.SanPhamKhuyenMais.Where(x => x.MACTKM == CTKM.MaCTKM).ToList();
                foreach(var item in LSPKM)
                {
                    SanPham sp = LSP.SingleOrDefault(x => x.MaSP == item.MaSP);
                    LSP.Remove(sp);
                }
            }
            else
            {
                ViewBag.listSPKM = null;
            }
            ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
            ViewBag.SanPhamMoi = LSP.Where(x => x.Moi == true).ToList();
            ViewBag.SanPhamNoiBat = LSP.OrderBy(x => x.SoLanMua).ToList();
            ViewBag.LoaiSanPham = db.LoaiSanPhams.ToList();
            ViewBag.SanPhamTheoLoai = LSP.ToList();
            return View();
        }
        public ActionResult MenuPartial()
        {
            ViewBag.listDMSP = db.LoaiSanPhams.ToList();
            ViewBag.listMenu = db.SanPhams.ToList();
            return PartialView();
        }

        public ActionResult TaiKhoanPartial()
        {
            return PartialView();
        }

        public ActionResult HienThiSanPhamKhuyenMaiPartial()
        {
           
            return PartialView();
        }

        public ActionResult HienThiSanPhamMoiPartial()
        {
            return PartialView();
        }

        public ActionResult HienThiSanPhamTheoLoaiPartial()
        {
            return PartialView();
        }

        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
            if (TaiKhoan == null || MatKhau == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            MatKhau = MaHoa.MD5Hash(MatKhau);
            var result = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == TaiKhoan && x.MatKhau == MatKhau);
            if (result == null)
            {
                return Content("Tài khoản hoặc mật khẩu không chính xác!");
            }
            Session["TaiKhoan"] = result;
            return Content("<script>window.location.reload();</script>");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien model)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    var member = db.ThanhViens.SingleOrDefault(x => x.Email.Contains(model.Email));
                    if (member != null)
                    {
                        ViewBag.loi = "Tài khoản đã tồn tại!";
                        return View();
                    }
                    model.MatKhau = MaHoa.MD5Hash(model.MatKhau);
                    model.MaLoaiTV = 2;
                    db.ThanhViens.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.loi = "Sai mã Captcha";
            return View();
        }
        public ActionResult BanerPartial()
        {
            List<Slider> LS = db.Sliders.ToList();
            return PartialView(LS);
        }
    }
}