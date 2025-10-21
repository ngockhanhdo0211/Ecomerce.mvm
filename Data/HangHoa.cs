using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class HangHoa
{
    public int MaHh { get; set; }

    public string TenHh { get; set; } = null!;

    public double DonGia { get; set; }  // 👈 Sửa decimal → double cho khớp EF gốc

    public string? MoTa { get; set; }

    public string? Hinh { get; set; }

    public int? MaLoai { get; set; }

    public string? MoTaDonVi { get; set; }

    public DateTime? NgaySx { get; set; }

    public string? TenAlias { get; set; }

    public string? MaNcc { get; set; }

    // 🔹 Navigation properties
    public virtual Loai? MaLoaiNavigation { get; set; }

    public virtual NhaCungCap? MaNccNavigation { get; set; }

    public virtual ICollection<BanBe> BanBes { get; set; } = new List<BanBe>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietHd> ChiTietHds { get; set; } = new List<ChiTietHd>();

    public virtual ICollection<YeuThich> YeuThiches { get; set; } = new List<YeuThich>();
}
