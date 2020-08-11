namespace WebBanDongHo.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            ChiTietDonDatHangs = new HashSet<ChiTietDonDatHang>();
        }

        [Key]
        [DisplayName("Mã đơn đặt hàng")]
        public int MaDDH { get; set; }
        [DisplayName("Khách hàng")]
        public int? MAKH { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày đặt")]
        public DateTime? NgayDat { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày giao")]
        public DateTime? NgayGiao { get; set; }
        [DisplayName("Hình thức thanh toán")]
        public int? MaHTTT { get; set; }
        [DisplayName("Đối tác thanh toán")]
        public int? MaDTTT { get; set; }
        [DisplayName("Mã đối tác thanh toán")]
        public bool? TinhTrangGiaoHang { get; set; }
        [DisplayName("Đã thanh toán")]
        public bool? DaThanhToan { get; set; }
        [DisplayName("Đã hủy")]
        public bool? DaHuy { get; set; }
        [DisplayName("Tổng thanh toán")]
        public decimal? TongThanhToan { get; set; }

        [StringLength(500)]
        [DisplayName("Địa chỉ nhận hàng")]
        public string DiaChiNhanHang { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [DisplayName("Hoàn thành")]
        public bool? HoanThanh { get; set; }
        [DisplayName("Ưu đãi")]
        public double? UuDai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDatHang> ChiTietDonDatHangs { get; set; }

        public virtual DoiTacThanhToan DoiTacThanhToan { get; set; }

        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
