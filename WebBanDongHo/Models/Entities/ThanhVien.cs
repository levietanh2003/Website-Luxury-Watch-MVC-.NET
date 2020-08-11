namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThanhVien")]
    public partial class ThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThanhVien()
        {
            KhachHangs = new HashSet<KhachHang>();
        }

        [Key]
        [DisplayName("Mã thành viên")]
        public int MaTV { get; set; }

        [StringLength(100)]
        [DisplayName("Tài khoản")]
        public string TaiKhoan { get; set; }

        [StringLength(100)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [StringLength(255)]
        [DisplayName("Họ tên")]
        public string Hoten { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(20)]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [StringLength(100)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Mã loại thành viên")]
        public int? MaLoaiTV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }
    }
}
