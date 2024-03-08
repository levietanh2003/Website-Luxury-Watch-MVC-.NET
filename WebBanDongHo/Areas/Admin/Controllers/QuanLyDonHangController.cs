using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebBanDongHo.Models.Entities;

namespace WebBanDongHo.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuanLyDonHangController : AdminBaseController
    {
        WebBanDongHoDbContext db = new WebBanDongHoDbContext();

        // GET: Admin/QuanLyDonHang
        public ActionResult DonHangMoi(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == false && x.HoanThanh == false && x.DaHuy == false);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == false && x.HoanThanh == false && x.DaHuy == false && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult DonHangDangXuLy(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.HoanThanh == false && x.DaHuy == false);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.HoanThanh == false && x.DaHuy == false && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DonHangDaHoanThanh(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.DaThanhToan == true && x.DaHuy == false && x.HoanThanh == true);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.TinhTrangGiaoHang == true && x.DaThanhToan == true && x.DaHuy == false && x.HoanThanh == true && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DonHangDaHuy(int? page, string search)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var listDDM = db.DonDatHangs.Where(x => x.DaHuy == true);
            if (search != null)
            {
                listDDM = db.DonDatHangs.Where(x => x.DaHuy == true && x.KhachHang.TenKH.Contains(search));
                ViewBag.search = search;
            }
            return View(listDDM.OrderBy(x => x.NgayDat).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CapNhatThongTin(int? MaDDH, string diachi, string ghichu)
        {
            if(MaDDH == null)
            {
                Response.StatusCode = 404;
            }
            var model = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == MaDDH);
            if(model == null)
            {
                return HttpNotFound();
            }
            model.DiaChiNhanHang = diachi;
            model.GhiChu = ghichu;
            db.SaveChanges();
            return Content("<script>window.location.reload();</script>");
        }
        public ActionResult DuyetDonHang(int? MaDDH)
        {
            if (MaDDH == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            DonDatHang model = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == MaDDH);
            if (model == null)
            {
                return HttpNotFound();
            }
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == MaDDH).ToList();
            ViewBag.ListChiTietDH = listChiTietDH;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DuyetDonHang(DonDatHang dDH, string email)
        {
            DonDatHang dDHUpdate = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == dDH.MaDDH);
            dDHUpdate.TinhTrangGiaoHang = dDH.TinhTrangGiaoHang;
            dDHUpdate.DaThanhToan = dDH.DaThanhToan;
            dDHUpdate.DaHuy = dDH.DaHuy;
            dDHUpdate.NgayGiao = dDH.NgayGiao;
            dDHUpdate.DaHuy = dDH.DaHuy;
            dDHUpdate.HoanThanh = dDH.HoanThanh;
            db.SaveChanges();
            DonDatHang ddhkh = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == dDH.MaDDH);
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == dDH.MaDDH).ToList();
            ViewBag.ListChiTietDH = listChiTietDH;
            ViewBag.email = email;
            if (dDH.TinhTrangGiaoHang == true && dDH.HoanThanh == false && dDH.DaHuy == false)
            {
                GuiEmail("Xác nhận đơn hàng của hệ thống", ddhkh.KhachHang.Email, "avanh090@gmail.com", "01082003md.", "Đơn hàng của bạn sẽ được giao và ngày: " + dDHUpdate.NgayGiao.Value.ToString("dd/MM/yyyy") + email);
                return RedirectToAction("DonHangDangXuLy");
            }
            else if(dDH.HoanThanh == true)
            {
                return RedirectToAction("DonHangDaHoanThanh");
            }else if(dDH.DaHuy == true)
            {
                foreach(var item in listChiTietDH)
                {
                    SanPham SP = db.SanPhams.SingleOrDefault(x => x.MaSP == item.MaSP);
                    if (SP != null)
                    {
                        SP.SoLuongTon += item.SoLuong;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("DonHangDaHuy");
            }
            else
            {
                return View(dDHUpdate);
            }
            
        }
        public void GuiEmail(string title, string toEmail, string fromEmail, string passWord, string content)
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
        }
        public ActionResult XoaDonHang(int? MaDDH)
        {
            if (MaDDH == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var model = db.DonDatHangs.SingleOrDefault(x => x.MaDDH == MaDDH);
            if (model == null)
            {
                return HttpNotFound();
            }
            if(model.DaHuy == true)
            {
                db.DonDatHangs.Remove(model);
                db.SaveChanges();
            }
            
            return Content("<script>window.location.reload();</script>");
        }
    }
}