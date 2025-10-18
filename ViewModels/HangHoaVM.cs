namespace ECommerceMVC.ViewModels
{
    public class HangHoaVM
    {
        public int MaHH { get; set; }

        public string TenHH { get; set; } = string.Empty;

        public string? Hinh { get; set; }

        public string? MoTaNgan { get; set; }

        // Dùng double vì DB là float
        public double DonGia { get; set; }

        public string TenLoai { get; set; } = string.Empty;

        public string TenNcc { get; set; } = string.Empty;
    }
}
