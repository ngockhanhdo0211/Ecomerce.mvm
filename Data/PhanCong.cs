using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class PhanCong
{
    public int MaPc { get; set; } // ✅ Khóa chính

    public string? MaNv { get; set; }

    public string? MaPb { get; set; }

    public string? NhiemVu { get; set; } // ✅ Bổ sung lại thuộc tính này

    public DateTime? NgayPhanCong { get; set; } // ✅ Bổ sung lại thuộc tính này

    // Navigation
    public virtual NhanVien? MaNvNavigation { get; set; }

    public virtual PhongBan? MaPbNavigation { get; set; }
}
