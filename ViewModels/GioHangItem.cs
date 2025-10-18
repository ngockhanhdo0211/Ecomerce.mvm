namespace ECommerceMVC.ViewModels
{
    public class GioHangItem
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; } 
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        // Tính thành tiền (DonGia * SoLuong)
        public double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }
    }
}
