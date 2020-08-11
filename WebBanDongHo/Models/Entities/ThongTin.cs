namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTin")]
    public partial class ThongTin
    {
        [Key]
        [DisplayName("Mã thông tin")]
        public int MaTT { get; set; }

        [StringLength(50)]
        [DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [StringLength(250)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Địa chỉ shop")]
        public string Map { get; set; }
        [DisplayName("Giới thiệu")]
        public string GioiThieu { get; set; }
    }
}
