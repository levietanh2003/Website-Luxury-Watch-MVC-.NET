namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        [DisplayName("Mã chi tiết phiếu nhập")]
        public int MaCTPN { get; set; }
        [DisplayName("Phiếu nhập")]
        public int? MaPN { get; set; }
        [DisplayName("Sản phẩm")]
        public int? MaSP { get; set; }
        [DisplayName("Đơn giá nhập")]
        public decimal? DonGiaNhap { get; set; }
        [DisplayName("Số lượng nhập")]
        public int? SoLuongNhap { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
