using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMVC.Data;

[Table("ChiTietDonHang")]
public partial class ChiTietDonHang
{
    [Key]
    [Column("MaCTDH")]
    public int MaCtdh { get; set; }

    [Required]
    public int MaDh { get; set; }

    [Required]
    public int MaHh { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "float")]
    public double DonGia { get; set; }

    [ForeignKey("MaDh")]
    public virtual DonHang? MaDhNavigation { get; set; }

    [ForeignKey("MaHh")]
    public virtual HangHoa? MaHhNavigation { get; set; }
}
