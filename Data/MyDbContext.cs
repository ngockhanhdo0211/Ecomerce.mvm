using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // Các DbSet tương ứng với các bảng trong CSDL
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }

        // Cấu hình mapping chi tiết nếu cần
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tên bảng
            modelBuilder.Entity<HangHoa>().ToTable("HangHoa");
            modelBuilder.Entity<Loai>().ToTable("Loai");

            // Mối quan hệ 1-nhiều giữa Loai và HangHoa
            modelBuilder.Entity<HangHoa>()
                .HasOne(h => h.Loai)
                .WithMany(l => l.HangHoas)
                .HasForeignKey(h => h.MaLoai)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
