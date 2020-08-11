namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPhamKhuyenMai")]
    public partial class SanPhamKhuyenMai
    {
        [Key]
        [DisplayName("Mã sản phẩm khuyến mãi")]
        public int MaSPKM { get; set; }
        [DisplayName("Mã sản phẩm ")]
        public int? MaSP { get; set; }
        [DisplayName("Mã chương trình khuyến mãi")]
        public int? MACTKM { get; set; }
        [DisplayName("Giá trị giảm")]
        public decimal? GiaTriGiam { get; set; }

        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
