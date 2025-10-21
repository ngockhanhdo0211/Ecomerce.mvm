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
    public int MaDh { get; set; }  // ✅ Khóa ngoại đến HoaDon

    [Required]
    public int MaHh { get; set; }  // ✅ Khóa ngoại đến HangHoa

    public int SoLuong { get; set; }

    [Column(TypeName = "float")]
    public double DonGia { get; set; }

    // 🔹 Navigation properties
    [ForeignKey("MaDh")]
    public virtual HoaDon? MaDhNavigation { get; set; }

    [ForeignKey("MaHh")]
    public virtual HangHoa? MaHhNavigation { get; set; }
}
