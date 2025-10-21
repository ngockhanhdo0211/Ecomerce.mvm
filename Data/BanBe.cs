using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceMVC.Data;

[Table("BanBe")]
public partial class BanBe
{
    [Key]
    [Column("MaBB")]
    public int MaBb { get; set; }

    [StringLength(10)]
    public string? MaKh { get; set; }

    public int MaHh { get; set; }

    [StringLength(100)]
    public string? HoTen { get; set; }

    [Required, StringLength(100)]
    public string Email { get; set; } = null!;

    public DateTime NgayGui { get; set; }

    [StringLength(250)]
    public string? GhiChu { get; set; }

    [ForeignKey("MaKh")]
    public virtual KhachHang? MaKhNavigation { get; set; }

    [ForeignKey("MaHh")]
    public virtual HangHoa MaHhNavigation { get; set; } = null!;
}
