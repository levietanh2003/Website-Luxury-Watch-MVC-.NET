namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongTrinhKhuyenMai")]
    public partial class ChuongTrinhKhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhKhuyenMai()
        {
            SanPhamKhuyenMais = new HashSet<SanPhamKhuyenMai>();
        }

        [Key]
        [DisplayName("Mã chương trình khuyến mãi")]
        public int MaCTKM { get; set; }

        [StringLength(255)]
        [DisplayName("Tên chương trình khuyến mãi")]
        public string TenCTKM { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày bắt đầu")]
        public DateTime? NgayBatDau { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày kết thúc")]
        public DateTime? NGgayKetThuc { get; set; }
        [DisplayName("Số lượng sản phẩm")]
        public int? SoLuongSanPham { get; set; }
        [DisplayName("Áp dụng")]
        public bool? ApDung { get; set; }
        [DisplayName("Ảnh")]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
    }
}
