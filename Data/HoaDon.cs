using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMVC.Data
{
    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [Column("MaHD")]
        public int MaHd { get; set; }

        [Column("MaKH")]
        public string MaKh { get; set; } = null!;

        public DateTime NgayDat { get; set; }
        public DateTime? NgayCan { get; set; }
        public DateTime? NgayGiao { get; set; }

        public string? HoTen { get; set; }
        public string DiaChi { get; set; } = null!;
        public string CachThanhToan { get; set; } = null!;
        public string CachVanChuyen { get; set; } = null!;
        public double PhiVanChuyen { get; set; }
        public int MaTrangThai { get; set; }

        public string? MaNv { get; set; }
        public string? GhiChu { get; set; }

        [ForeignKey(nameof(MaKh))]
        public virtual KhachHang MaKhNavigation { get; set; } = null!;

        [ForeignKey(nameof(MaNv))]
        public virtual NhanVien? MaNvNavigation { get; set; }

        [ForeignKey(nameof(MaTrangThai))]
        public virtual TrangThai MaTrangThaiNavigation { get; set; } = null!;

        [InverseProperty(nameof(ChiTietHd.HoaDon))]
        public virtual ICollection<ChiTietHd> ChiTietHds { get; set; } = new List<ChiTietHd>();
    }
}
