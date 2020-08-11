namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        [Key]
        [DisplayName("Mã slider")]
        public int MaSlider { get; set; }
        [DisplayName("Ảnh")]
        public string Anh { get; set; }
    }
}
