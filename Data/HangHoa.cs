using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMVC.Data
{
    [Table("HangHoa")]
    public partial class HangHoa
    {
        [Key]
        public int MaHh { get; set; }

        [Required, StringLength(100)]
        public string TenHh { get; set; }

        [StringLength(100)]
        public string? TenAlias { get; set; } // 👈 Thêm dòng này để fix lỗi

        [StringLength(255)]
        public string? MoTaDonVi { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double DonGia { get; set; }

        [StringLength(255)]
        public string? Hinh { get; set; }

        public DateTime? NgaySx { get; set; }

        public double? GiamGia { get; set; }

        [StringLength(255)]
        public string? MoTa { get; set; }

        // 🔹 Khóa ngoại loại hàng
        
        public int? MaLoai { get; set; }
        
        public virtual Loai? MaLoaiNavigation { get; set; }
        [ForeignKey("MaLoai")]
        public virtual Loai? Loai { get; set; }

        // 🔹 Khóa ngoại nhà cung cấp
        // 🔹 Khóa ngoại nhà cung cấp
        public string? MaNcc { get; set; } // chú ý kiểu string vì trong DbContext cột MaNcc có .HasMaxLength(50)
        [ForeignKey("MaNcc")]
        public virtual NhaCungCap? MaNccNavigation { get; set; }


        // 🔹 Quan hệ 1-n
        public virtual ICollection<ChiTietHd>? ChiTietHds { get; set; } = new List<ChiTietHd>();
        public virtual ICollection<BanBe>? BanBes { get; set; } = new List<BanBe>();
        public virtual ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
        public virtual ICollection<YeuThich>? YeuThiches { get; set; } = new List<YeuThich>();

    }
}
