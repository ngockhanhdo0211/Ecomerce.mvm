using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMVC.Data;

[Table("ChiTietHD")]
public partial class ChiTietHd
{
    [Key]
    [Column("MaCT")]
    public int MaCt { get; set; }

    public int MaHd { get; set; }

    public int MaHh { get; set; }

    [Column(TypeName = "float")]
    public double DonGia { get; set; }

    public int SoLuong { get; set; }

    public double GiamGia { get; set; }

    [ForeignKey("MaHh")]
    public virtual HangHoa MaHhNavigation { get; set; } = null!;

    [ForeignKey("MaHd")]
    public virtual HoaDon MaHdNavigation { get; set; } = null!;
}
