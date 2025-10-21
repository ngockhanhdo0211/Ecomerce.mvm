using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Data
{
    public partial class DonHang
    {
        [Key] // ✅ Khai báo đây là khóa chính cho Entity Framework
        public int MaDh { get; set; }

        // 🔧 Sửa lại kiểu dữ liệu MaKh sang string cho khớp với bảng KhachHang
        public string? MaKh { get; set; }

        public DateTime? NgayDat { get; set; }

        public decimal? TongTien { get; set; }

        public string? TrangThai { get; set; }

        // ✅ Liên kết tới KhachHang (1-n)
        public virtual KhachHang? MaKhNavigation { get; set; }

        // ✅ Liên kết tới ChiTietDonHang (1-n)
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}
