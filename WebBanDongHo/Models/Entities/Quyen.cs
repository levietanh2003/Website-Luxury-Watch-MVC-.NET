namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quyen")]
    public partial class Quyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quyen()
        {
            LoaiThanhVien_Quyen = new HashSet<LoaiThanhVien_Quyen>();
        }

        [Key]
        [StringLength(100)]
        [DisplayName("Mã quyền")]
        public string MaQuyen { get; set; }

        [StringLength(255)]
        [DisplayName("Tên quyền")]
        public string TenQuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoaiThanhVien_Quyen> LoaiThanhVien_Quyen { get; set; }
    }
}
