namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoaiThanhVien_Quyen
    {
        [Key]
        public int MaLTVQ { get; set; }
        [DisplayName("Loại thành viên")]
        public int? MaLoaiTV { get; set; }

        [StringLength(100)]
        [DisplayName("Quyền")]
        public string MaQuyen { get; set; }

        public virtual LoaiThanhVien LoaiThanhVien { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
