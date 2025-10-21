using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class PhongBan
{
    public string MaPb { get; set; } = null!;

    public string TenPb { get; set; } = null!;

    public string? ThongTin { get; set; }

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<PhanQuyen> PhanQuyens { get; set; } = new List<PhanQuyen>();

    // ✅ Thêm dòng này để ánh xạ ngược tới NhanVien
    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
