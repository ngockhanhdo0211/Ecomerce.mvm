using ECommerceMVC.Helpers;
using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ECommerceMVC.Controllers
{
    public class GioHangController : BaseController
    {
        private readonly Hshop2023Context db;
        private const string CART_KEY = "GioHang";

        public GioHangController(Hshop2023Context context)
        {
            db = context;
        }

        // 🔹 Lấy giỏ hàng hiện tại trong Session
        private List<GioHangItem> LayGioHang()
        {
            var data = HttpContext.Session.Get<List<GioHangItem>>(CART_KEY);
            return data ?? new List<GioHangItem>();
        }

        // 🔹 Hiển thị giỏ hàng
        public IActionResult Index(string? message = null)
        {
            var gioHang = LayGioHang();
            ViewBag.TongTien = gioHang.Sum(p => p.ThanhTien);
            ViewBag.Message = message;
            return View(gioHang);
        }

        // 🔹 Thêm sản phẩm vào giỏ (nhận đủ thông tin từ trang ChiTiet)
        public IActionResult ThemVaoGio(int mahh, string tenHH, double donGia, string hinh)
        {
            var gioHang = LayGioHang();
            var item = gioHang.FirstOrDefault(p => p.MaHH == mahh);

            if (item == null)
            {
                gioHang.Add(new GioHangItem
                {
                    MaHH = mahh,
                    TenHH = tenHH,
                    DonGia = donGia,
                    Hinh = string.IsNullOrEmpty(hinh) ? "no-image.jpg" : hinh,
                    SoLuong = 1
                });
            }
            else
            {
                item.SoLuong++;
            }

            HttpContext.Session.Set(CART_KEY, gioHang);
            return RedirectToAction("Index", new { message = "added" });
        }

        // 🔹 Xóa 1 sản phẩm khỏi giỏ
        public IActionResult Xoa(int mahh)
        {
            var gioHang = LayGioHang();
            var item = gioHang.FirstOrDefault(p => p.MaHH == mahh);
            if (item != null) gioHang.Remove(item);

            HttpContext.Session.Set(CART_KEY, gioHang);
            return RedirectToAction("Index");
        }

        // 🔹 Xóa toàn bộ giỏ hàng
        public IActionResult XoaTatCa()
        {
            HttpContext.Session.Remove(CART_KEY);
            return RedirectToAction("Index");
        }

        // 🔹 Cập nhật số lượng (POST)
        [HttpPost]
        public IActionResult CapNhat(int mahh, int soLuong)
        {
            var gioHang = LayGioHang();
            var item = gioHang.FirstOrDefault(p => p.MaHH == mahh);
            if (item != null)
            {
                item.SoLuong = soLuong;
            }

            HttpContext.Session.Set(CART_KEY, gioHang);
            return RedirectToAction("Index");
        }
    }

    // 🔹 Helpers: Lưu và đọc object vào Session
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null
                ? default(T)
                : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}
