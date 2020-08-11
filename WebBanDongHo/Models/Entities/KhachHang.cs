namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonDatHangs = new HashSet<DonDatHang>();
        }

        [Key]
        [DisplayName("Mã khách hàng")]
        public int Makh { get; set; }

        [StringLength(255)]
        [DisplayName("Tên khách hàng")]
        public string TenKH { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(255)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [StringLength(20)]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }
        [DisplayName("Thành viên")]
        public int? MaTV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }

        public virtual ThanhVien ThanhVien { get; set; }
    }
}
