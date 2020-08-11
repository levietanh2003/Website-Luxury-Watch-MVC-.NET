﻿namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiThanhVien")]
    public partial class LoaiThanhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiThanhVien()
        {
            LoaiThanhVien_Quyen = new HashSet<LoaiThanhVien_Quyen>();
            ThanhViens = new HashSet<ThanhVien>();
        }

        [Key]
        [DisplayName("Mã loại thành viên")]
        public int MaLoaiTV { get; set; }

        [StringLength(255)]
        [DisplayName("Tên loại thành viên")]
        public string TenLoaiTV { get; set; }
        [DisplayName("Ưu đãi")]
        public double? uuDai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoaiThanhVien_Quyen> LoaiThanhVien_Quyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThanhVien> ThanhViens { get; set; }
    }
}
