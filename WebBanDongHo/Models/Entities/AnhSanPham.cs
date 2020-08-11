namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhSanPham")]
    public partial class AnhSanPham
    {
        [Key]
        [DisplayName("Mã ảnh sản phẩm")]
        public int MaAnhSP { get; set; }
        [DisplayName("Sản phẩm")]
        public int? MaSP { get; set; }
        [DisplayName("Tên ảnh sản phẩm")]
        public string TenAnhSP { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
