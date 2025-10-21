using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class GopY
{
    public string MaGy { get; set; } = null!;

    public int MaCd { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateTime NgayGy { get; set; }

    public string? HoTen { get; set; }

    public string? Email { get; set; }

    public string? DienThoai { get; set; }

    public bool CanTraLoi { get; set; }

    public string? TraLoi { get; set; }

    public DateTime? NgayTl { get; set; }

    // 👉 Thêm khóa ngoại tới KhachHang
    public string? MaKh { get; set; }

    // Navigation property tới bảng ChuDe (đã có sẵn)
    public virtual ChuDe MaCdNavigation { get; set; } = null!;

    // 👉 Thêm navigation property tới bảng KhachHang
    public virtual KhachHang? MaKhNavigation { get; set; }
}
