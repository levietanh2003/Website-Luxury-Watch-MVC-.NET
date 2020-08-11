namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            PhieuNhaps = new HashSet<PhieuNhap>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [DisplayName("Mã nhà cung cấp")]
        public int MaNCC { get; set; }

        [StringLength(255)]
        [DisplayName("Tên nhà cung cấp")]
        public string TenNCC { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(255)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [StringLength(20)]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
