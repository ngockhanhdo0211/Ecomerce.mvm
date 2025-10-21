using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Data;

public partial class Hshop2023Context : DbContext
{
    public Hshop2023Context()
    {
    }

    public Hshop2023Context(DbContextOptions<Hshop2023Context> options)
        : base(options)
    {
    }

    // ================================
    // Các bảng (DbSet)
    // ================================
    public virtual DbSet<HangHoa> HangHoas { get; set; } = null!;
    public virtual DbSet<Loai> Loais { get; set; } = null!;
    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
    public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
    public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; } = null!;
    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
    public virtual DbSet<BanBe> BanBes { get; set; } = null!;
    public virtual DbSet<ChuDe> ChuDes { get; set; } = null!;
    public virtual DbSet<GopY> Gopies { get; set; } = null!;
    public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
    public virtual DbSet<HoiDap> HoiDaps { get; set; } = null!;
    public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
    public virtual DbSet<PhanCong> PhanCongs { get; set; } = null!;
    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; } = null!;
    public virtual DbSet<PhongBan> PhongBans { get; set; } = null!;
    public virtual DbSet<TrangThai> TrangThais { get; set; } = null!;
    public virtual DbSet<TrangWeb> TrangWebs { get; set; } = null!;
    public virtual DbSet<YeuThich> YeuThiches { get; set; } = null!;


    // ================================
    // Mapping giữa class ↔ bảng SQL
    // ================================
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== Loai =====
        modelBuilder.Entity<Loai>(entity =>
        {
            entity.HasKey(e => e.MaLoai);
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        // ===== HangHoa =====
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHh);
            entity.Property(e => e.TenHh).HasMaxLength(100);
            entity.Property(e => e.MoTaDonVi).HasMaxLength(50);
            entity.Property(e => e.DonGia).HasColumnType("float");

            entity.HasOne(d => d.MaLoaiNavigation)
                .WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_HangHoa_Loai");

            entity.HasOne(d => d.MaNccNavigation)
                .WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_HangHoa_NhaCungCap");
        });

        // ===== NhaCungCap =====
        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);
            entity.Property(e => e.TenCongTy).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        // ===== KhachHang =====
        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        // ===== HoaDon =====
        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd);
            entity.Property(e => e.NgayDat).HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_HoaDon_KhachHang");

            entity.HasOne(d => d.MaTrangThaiNavigation)
                .WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaTrangThai)
                .HasConstraintName("FK_HoaDon_TrangThai");
        });

        // ===== ChiTietHd =====
        modelBuilder.Entity<ChiTietHd>(entity =>
        {
            entity.HasKey(e => e.MaCt);
            entity.Property(e => e.DonGia).HasColumnType("float");

            entity.HasOne(d => d.MaHhNavigation)
                .WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_ChiTietHd_HangHoa");

            entity.HasOne<HoaDon>()
                .WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK_ChiTietHd_HoaDon");
        });

        // ===== ChiTietDonHang =====
        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MaCtdh);
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.DonGia).HasColumnType("float");

            entity.HasOne(d => d.MaDhNavigation)
                .WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaDh)
                .HasConstraintName("FK_ChiTietDonHang_DonHang");

            entity.HasOne(d => d.MaHhNavigation)
                .WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.MaHh)
                .HasConstraintName("FK_ChiTietDonHang_HangHoa");
        });

        // ===== BanBe =====
        modelBuilder.Entity<BanBe>(entity =>
        {
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
            entity.HasKey(e => e.MaGy);
            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.NgayGy).HasColumnType("datetime");

            entity.HasOne(d => d.MaCdNavigation)
                .WithMany(p => p.Gopies)
                .HasForeignKey(d => d.MaCd)
                .HasConstraintName("FK_GopY_ChuDe");

            entity.HasOne(d => d.MaKhNavigation)
                .WithMany()
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_GopY_KhachHang");
        });

        // ===== TrangThai =====
        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai);
            entity.Property(e => e.TenTrangThai).HasMaxLength(100);
        });

        // ===== Các bảng còn lại =====
        modelBuilder.Entity<DonHang>(e => e.HasKey(x => x.MaDh));
        modelBuilder.Entity<HoiDap>(e => e.HasKey(x => x.MaHd));
        modelBuilder.Entity<NhanVien>(e => e.HasKey(x => x.MaNv));
        modelBuilder.Entity<PhanCong>(e => e.HasKey(x => x.MaPc));
        modelBuilder.Entity<PhanQuyen>(e => e.HasKey(x => x.MaPq));
        modelBuilder.Entity<PhongBan>(e => e.HasKey(x => x.MaPb));
        modelBuilder.Entity<TrangWeb>(e => e.HasKey(x => x.MaTrang));
        modelBuilder.Entity<YeuThich>(e => e.HasKey(x => x.MaYt));
    }
}
