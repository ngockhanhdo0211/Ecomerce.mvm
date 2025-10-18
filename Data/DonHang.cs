using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class DonHang
{
    public int MaDh { get; set; }

    public int? MaKh { get; set; }

    public DateTime? NgayDat { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
