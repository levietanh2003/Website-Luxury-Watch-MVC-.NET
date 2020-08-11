namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDonDatHang")]
    public partial class ChiTietDonDatHang
    {
        [Key]
        [DisplayName("Mã chi tiết đơn đặt hàng")]
        public int MaCTDDT { get; set; }
        [DisplayName("Đồng hồ")]
        public int? MaDDH { get; set; }
        [DisplayName("Sản phẩm")]
        public int? MaSP { get; set; }
        [DisplayName("Đơn giá")]
        public decimal? DonGia { get; set; }
        [DisplayName("Số lượng")]
        public int? SoLuong { get; set; }
        [DisplayName("Thành tiền")]
        public decimal? ThanhTien { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
