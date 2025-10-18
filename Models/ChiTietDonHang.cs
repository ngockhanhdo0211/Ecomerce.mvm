using System;
using System.Collections.Generic;

namespace ECommerceMVC.Models;

public partial class ChiTietDonHang
{
    public int MaCtdh { get; set; }

    public int? MaDh { get; set; }

    public int? MaHh { get; set; }

    public int? SoLuong { get; set; }

    public decimal? DonGia { get; set; }

    public virtual DonHang? MaDhNavigation { get; set; }

    public virtual HangHoa? MaHhNavigation { get; set; }
}
