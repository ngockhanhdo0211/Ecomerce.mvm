using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Data
{
public partial class Hshop2023Context : DbContext
{
public Hshop2023Context()
{
}

    public Hshop2023Context(DbContextOptions<Hshop2023Context> options)
        : base(options)
    {
    }

    // =============================
    // Các DbSet ánh xạ tới bảng SQL
    // =============================
    public virtual DbSet<HangHoa> HangHoas { get; set; } = null!;
    public virtual DbSet<Loai> Loais { get; set; } = null!;
    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
    public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
    public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; } = null!;
    public virtual DbSet<BanBe> BanBes { get; set; } = null!;
    public virtual DbSet<ChuDe> ChuDes { get; set; } = null!;
    public virtual DbSet<GopY> Gopies { get; set; } = null!;
    public virtual DbSet<TrangWeb> TrangWebs { get; set; } = null!;
    public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
    public virtual DbSet<HoiDap> HoiDaps { get; set; } = null!;
    public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
public virtual DbSet<PhanCong> PhanCongs { get; set; } = null!;
public virtual DbSet<PhanQuyen> PhanQuyens { get; set; } = null!;
public virtual DbSet<PhongBan> PhongBans { get; set; } = null!;
public virtual DbSet<TrangThai> TrangThais { get; set; } = null!;
public virtual DbSet<YeuThich> YeuThiches { get; set; } = null!;



    // =============================
    // Cấu hình Mapping chi tiết
    // =============================
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== Loai =====
        modelBuilder.Entity<Loai>(entity =>
        {
            entity.ToTable("Loai");
            entity.HasKey(e => e.MaLoai);
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        // ===== HangHoa =====
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.ToTable("HangHoa");
            entity.HasKey(e => e.MaHh);
            entity.Property(e => e.TenHh).HasMaxLength(100);
            entity.Property(e => e.MoTaDonVi).HasMaxLength(50);
            entity.Property(e => e.DonGia).HasColumnType("float");
            entity.Property(e => e.MaNcc).HasMaxLength(10);

            entity.HasOne(d => d.MaLoaiNavigation)
                .WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_HangHoa_Loai");

            entity.HasOne(d => d.MaNccNavigation)
                .WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_HangHoa_NhaCungCap");
        });
        // ===== HoiDap =====
modelBuilder.Entity<HoiDap>(entity =>
{
    entity.ToTable("HoiDap");
    entity.HasKey(e => e.MaHd); // ✅ Khóa chính

    entity.Property(e => e.CauHoi).HasMaxLength(500);
    entity.Property(e => e.TraLoi).HasMaxLength(500);
    entity.Property(e => e.MaNv).HasMaxLength(10);
    entity.Property(e => e.NgayDua).HasColumnType("date");

    entity.HasOne(d => d.MaNvNavigation)
        .WithMany()
        .HasForeignKey(d => d.MaNv)
        .HasConstraintName("FK_HoiDap_NhanVien");
});


        // ===== NhaCungCap =====
        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.ToTable("NhaCungCap");
            entity.HasKey(e => e.MaNcc);
            entity.Property(e => e.TenCongTy).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Logo).HasMaxLength(255);
        });

        // ===== KhachHang =====
        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.ToTable("KhachHang");
            entity.HasKey(e => e.MaKh);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
        });

        // ===== HoaDon =====
        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.ToTable("HoaDon");
            entity.HasKey(e => e.MaHd);
            entity.Property(e => e.NgayDat).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_HoaDon_KhachHang");
        });

        // ===== ChiTietHd =====
modelBuilder.Entity<ChiTietHd>(entity =>
{
    entity.ToTable("ChiTietHD");
    entity.HasKey(e => e.MaCt);
    entity.Property(e => e.DonGia).HasColumnType("float");
    entity.Property(e => e.SoLuong).HasDefaultValue(1);

    // ✅ Quan hệ với bảng HangHoa
    entity.HasOne(d => d.HangHoa)
        .WithMany(p => p.ChiTietHds)
        .HasForeignKey(d => d.MaHh)
        .HasConstraintName("FK_ChiTietHd_HangHoa");

    // ✅ Quan hệ với bảng HoaDon
    entity.HasOne(d => d.HoaDon)
        .WithMany(p => p.ChiTietHds)
        .HasForeignKey(d => d.MaHd)
        .HasConstraintName("FK_ChiTietHd_HoaDon");
});


        // ===== BanBe =====
        modelBuilder.Entity<BanBe>(entity =>
        {
            entity.ToTable("BanBe");
            entity.HasKey(e => e.MaBb).HasName("PK_Promotion");
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(250);
            entity.Property(e => e.NgayGui).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation)
                .WithMany(p => p.BanBes)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_BanBe_KhachHang");

            entity.HasOne(d => d.MaHhNavigation)
                .WithMany(p => p.BanBes)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_BanBe_HangHoa");
        });

        // ===== ChuDe =====
        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.ToTable("ChuDe");
            entity.HasKey(e => e.MaCd);
            entity.Property(e => e.TenCd).HasMaxLength(100);

            entity.HasOne(d => d.MaNvNavigation)
                .WithMany()
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_ChuDe_NhanVien");
        });


        // ===== GopY =====
modelBuilder.Entity<GopY>(entity =>
{
    entity.ToTable("GopY");
    entity.HasKey(e => e.MaGy);
    entity.Property(e => e.NoiDung).HasMaxLength(1000);
    entity.Property(e => e.HoTen).HasMaxLength(100);
    entity.Property(e => e.Email).HasMaxLength(100);
    entity.Property(e => e.DienThoai).HasMaxLength(20);
    entity.Property(e => e.TraLoi).HasMaxLength(1000);

    entity.HasOne(d => d.MaCdNavigation)
        .WithMany(p => p.Gopies)
        .HasForeignKey(d => d.MaCd)
        .HasConstraintName("FK_GopY_ChuDe");

    entity.HasOne(d => d.MaKhNavigation)
        .WithMany(p => p.Gopies)
        .HasForeignKey(d => d.MaKh)
        .HasConstraintName("FK_GopY_KhachHang");
});


        // ===== TrangWeb =====
        modelBuilder.Entity<TrangWeb>(entity =>
        {
            entity.ToTable("TrangWeb");
            entity.HasKey(e => e.MaTrang);
            entity.Property(e => e.TenTrang).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        // ===== DonHang =====
        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.ToTable("DonHang");
            entity.HasKey(e => e.MaDh);
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaKhNavigation)
                .WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_DonHang_KhachHang");
        });
        modelBuilder.Entity<NhanVien>(entity =>
{
    entity.ToTable("NhanVien");
    entity.HasKey(e => e.MaNv); // ✅ khóa chính

    entity.Property(e => e.MaNv).HasMaxLength(10);
    entity.Property(e => e.HoTen).HasMaxLength(100);
    entity.Property(e => e.Email).HasMaxLength(100);

    entity.HasOne(d => d.MaPbNavigation)
        .WithMany(p => p.NhanViens)
        .HasForeignKey(d => d.MaPb)
        .HasConstraintName("FK_NhanVien_PhongBan");
});
// ===== PhanCong =====
modelBuilder.Entity<PhanCong>(entity =>
{
    entity.ToTable("PhanCong");
    entity.HasKey(e => e.MaPc); // ✅ Khóa chính

    entity.Property(e => e.MaNv).HasMaxLength(10);
    entity.Property(e => e.MaPb).HasMaxLength(10);
    entity.Property(e => e.NhiemVu).HasMaxLength(200);
    entity.Property(e => e.NgayPhanCong).HasColumnType("datetime");

    entity.HasOne(d => d.MaNvNavigation)
        .WithMany(p => p.PhanCongs)
        .HasForeignKey(d => d.MaNv)
        .HasConstraintName("FK_PhanCong_NhanVien");

    entity.HasOne(d => d.MaPbNavigation)
        .WithMany(p => p.PhanCongs)
        .HasForeignKey(d => d.MaPb)
        .HasConstraintName("FK_PhanCong_PhongBan");
});
// ===== PhanQuyen =====
modelBuilder.Entity<PhanQuyen>(entity =>
{
    entity.ToTable("PhanQuyen");
    entity.HasKey(e => e.MaPq); // ✅ Khóa chính

    entity.Property(e => e.MaPb).HasMaxLength(10);
    entity.Property(e => e.MaTrang).HasColumnName("MaTrang");
    entity.Property(e => e.Them).HasDefaultValue(false);
    entity.Property(e => e.Sua).HasDefaultValue(false);
    entity.Property(e => e.Xoa).HasDefaultValue(false);
    entity.Property(e => e.Xem).HasDefaultValue(false);

    entity.HasOne(d => d.MaPbNavigation)
        .WithMany(p => p.PhanQuyens)
        .HasForeignKey(d => d.MaPb)
        .HasConstraintName("FK_PhanQuyen_PhongBan");

    entity.HasOne(d => d.MaTrangNavigation)
        .WithMany(p => p.PhanQuyens)
        .HasForeignKey(d => d.MaTrang)
        .HasConstraintName("FK_PhanQuyen_TrangWeb");
});
// ===== PhongBan =====
modelBuilder.Entity<PhongBan>(entity =>
{
    entity.ToTable("PhongBan");
    entity.HasKey(e => e.MaPb); // ✅ Khóa chính

    entity.Property(e => e.MaPb).HasMaxLength(10);
    entity.Property(e => e.TenPb).HasMaxLength(100);
    entity.Property(e => e.ThongTin).HasMaxLength(255);
});
// ===== TrangThai =====
modelBuilder.Entity<TrangThai>(entity =>
{
    entity.ToTable("TrangThai");
    entity.HasKey(e => e.MaTrangThai); // ✅ Khóa chính

    entity.Property(e => e.TenTrangThai)
        .HasMaxLength(100)
        .IsRequired();

    entity.Property(e => e.MoTa)
        .HasMaxLength(255);
});
modelBuilder.Entity<YeuThich>(entity =>
{
    entity.ToTable("YeuThich");
    entity.HasKey(e => new { e.MaKh, e.MaHh }); // ✅ Khóa chính kép

    entity.Property(e => e.NgayChon).HasColumnType("datetime");

    entity.HasOne(d => d.MaKhNavigation)
        .WithMany(p => p.YeuThiches)
        .HasForeignKey(d => d.MaKh)
        .HasConstraintName("FK_YeuThich_KhachHang");

    entity.HasOne(d => d.MaHhNavigation)
        .WithMany(p => p.YeuThiches)
        .HasForeignKey(d => d.MaHh)
        .HasConstraintName("FK_YeuThich_HangHoa");
});





    }
}


}