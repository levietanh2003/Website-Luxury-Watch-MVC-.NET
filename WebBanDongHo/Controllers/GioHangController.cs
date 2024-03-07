using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Controllers
{
    public class GioHangController : Controller
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();
        // GET: GioHang
        public ActionResult GioHangPartial()
        {
            ViewBag.TongTien = TinhTongThanhTien();
            List<ItemGioHang> listGioHang = LayGioHang();
            return PartialView(listGioHang);
        }
        public ActionResult HienThiGioHang()
        {
            ViewBag.AnhSanPham = db.AnhSanPhams.ToList();
            List<ItemGioHang> listGioHang = LayGioHang();
            if (Session["TaiKhoan"] == null)
            {
                ViewBag.uuDai = 0;
            }
            else
            {
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                LoaiThanhVien ltv = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == tv.MaLoaiTV);
                ViewBag.Hoten = tv.Hoten;
                ViewBag.Email = tv.Email;
                ViewBag.SDT = tv.SoDienThoai;
                ViewBag.uuDai = ltv.uuDai;
            }
            return View(listGioHang);
        }
        public List<ItemGioHang> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (listGioHang == null)
            {
                //Nếu giỏ hàng chư tồn tại
                listGioHang = new List<ItemGioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }


        public ActionResult ThemGioHang(int MaSP, string strUrl)
        {
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == MaSP);
            if (sp == null)
            {
                //Đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            List<ItemGioHang> listGioHang = LayGioHang();
            ItemGioHang checkSP = listGioHang.SingleOrDefault(x => x.MaSP == MaSP);
            if (checkSP != null)
            {
                if(sp.SoLuongTon <= 0)
                {
                    return Redirect(strUrl);
                }
                if (sp.SoLuongTon <= checkSP.SoLuong)
                {
                    return Redirect(strUrl);
                }
                checkSP.SoLuong++;
                checkSP.ThanhTien = checkSP.DonGia * checkSP.SoLuong;
                return Redirect(strUrl);
            }
            if(sp.SoLuongTon > 0)
            {
                ItemGioHang itemGioHang = new ItemGioHang(MaSP);
                listGioHang.Add(itemGioHang);
            }
            
            return Redirect(strUrl);
        }
        public int TinhTongSoLuong()
        {
            List<ItemGioHang> listGioHang = LayGioHang();
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.SoLuong);
        }
        public decimal? TinhTongThanhTien()
        {
            List<ItemGioHang> listGioHang = LayGioHang();
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.ThanhTien);
        }
        public ActionResult XoaItemGioHang(int MaSP)
        {
            List<ItemGioHang> list = LayGioHang();
            ItemGioHang item = list.SingleOrDefault(x => x.MaSP == MaSP);
            if (item == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            list.Remove(item);
            return RedirectToAction("HienThiGioHang");
        }
        public ActionResult DatHang(string HoTen, string SDT, string Email, string DiaChi, string GhiChu)
        {
            KhachHang KH = new KhachHang();
            LoaiThanhVien ltv = null;
            double uudai = 0;
            if (Session["TaiKhoan"] == null)
            {
                KhachHang NKH = new KhachHang();
                NKH.TenKH = HoTen;
                NKH.SoDienThoai = SDT;
                NKH.MaTV = null;
                NKH.DiaChi = null;
                NKH.Email = Email;
                db.KhachHangs.Add(NKH);
                db.SaveChanges();
                KH = NKH;
            }
            else
            {
                ThanhVien tv = Session["TaiKhoan"] as ThanhVien;
                ltv = db.LoaiThanhViens.SingleOrDefault(x => x.MaLoaiTV == tv.MaLoaiTV);
                //Kiểm tra khách hàng đã tồn tại trong csdl hay chưa
                KhachHang checkKH = db.KhachHangs.SingleOrDefault(x => x.MaTV == tv.MaTV);
                KH = checkKH;
                if (checkKH == null)
                {
                    KhachHang nkh = new KhachHang();
                    nkh.TenKH = tv.Hoten;
                    nkh.Email = tv.Email;
                    nkh.DiaChi = tv.DiaChi;
                    nkh.SoDienThoai = tv.SoDienThoai;
                    nkh.MaTV = tv.MaTV;
                    db.KhachHangs.Add(nkh);
                    db.SaveChanges();
                    KH = nkh;
                }
            }
            if(ltv == null)
            {
                uudai = 0;
            }
            else
            {
                uudai = (double)ltv.uuDai;
            }
            //Tạo mới đơn hàng
            DonDatHang dDH = new DonDatHang();
            List<ItemGioHang> list = LayGioHang();
            dDH.NgayDat = DateTime.Now;
            dDH.TinhTrangGiaoHang = false;
            dDH.NgayGiao = null;
            dDH.DaThanhToan = false;
            dDH.DaHuy = false;
            dDH.HoanThanh = false;
            dDH.MAKH = KH.Makh;
            dDH.DiaChiNhanHang =DiaChi;
            dDH.GhiChu = GhiChu;
            dDH.UuDai = uudai;
            dDH.TongThanhToan = list.Sum(x => x.ThanhTien).Value - ((decimal?)((double)list.Sum(x => x.ThanhTien).Value * uudai) / 100);
            db.DonDatHangs.Add(dDH);
            db.SaveChanges();
            //Thêm chi tiết đơn đặt hàng
            foreach (var item in list)
            {
                SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == item.MaSP);
                sp.SoLanMua++;
                sp.SoLuongTon -= item.SoLuong;
                db.SaveChanges();
                ChiTietDonDatHang cTDDH = new ChiTietDonDatHang();
                cTDDH.MaDDH = dDH.MaDDH;
                cTDDH.MaSP = item.MaSP;
                cTDDH.SoLuong = item.SoLuong;
                cTDDH.DonGia = item.DonGia;
                cTDDH.ThanhTien = item.ThanhTien;
                db.ChiTietDonDatHangs.Add(cTDDH);
            }
            db.SaveChanges();
            Session["gioHang"] = null;
            //GuiEmail("Xác nhận đơn hàng của hệ thống", KH.Email, "hoanganhnguyenkfe99@gmail.com", "Anhhoang@123", "Đơn hàng của bạn đang được xử lý!");
            return RedirectToAction("HienThiGioHang");
        }
        /*public void GuiEmail(string title, string toEmail, string fromEmail, string passWord, string content)
        {
            //Gọi Email
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail); //Địa chỉ nhận
            mail.From = new MailAddress(toEmail); //Địa chỉ gửi
            mail.Subject = title; //Tiêu đề gửi
            mail.Body = content; //Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";//Host gửi của Gmail
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(fromEmail, passWord);//Tài khoản và mật khẩu người gửi
            smtp.EnableSsl = true;//Kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail);//Gửi email
        }*/
    }
}