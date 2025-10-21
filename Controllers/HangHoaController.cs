using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;

        public HangHoaController(Hshop2023Context context)
        {
            db = context;
        }

        // =============================
        // [ACTION] Danh sách sản phẩm
        // =============================
        public IActionResult Index(int? maloai, string? keyword)
        {
            // 1️⃣ Lấy danh sách hàng hóa (kèm loại)
            var hangHoas = db.HangHoas
                             .Include(h => h.MaLoaiNavigation)
                             .AsQueryable();

            // 2️⃣ Lọc theo mã loại
            if (maloai.HasValue)
            {
                hangHoas = hangHoas.Where(h => h.MaLoai == maloai.Value);
            }

            // 3️⃣ Lọc theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                hangHoas = hangHoas.Where(h => h.TenHh.ToLower().Contains(keyword));
            }

            // 4️⃣ Chuyển sang ViewModel
            var data = hangHoas.Select(h => new HangHoaVM
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                Hinh = h.Hinh,
                MoTaNgan = h.MoTa,
                DonGia = h.DonGia,
                TenLoai = h.MaLoaiNavigation != null ? h.MaLoaiNavigation.TenLoai : "Chưa phân loại"
            }).ToList();

            return View(data);
        }

        // =============================
        // [ACTION] Chi tiết sản phẩm
        // =============================
        public IActionResult ChiTiet(int id)
        {
            var hh = db.HangHoas
                       .Include(h => h.MaLoaiNavigation)
                       .Where(h => h.MaHh == id)
                       .Select(h => new HangHoaVM
                       {
                           MaHH = h.MaHh,
                           TenHH = h.TenHh,
                           Hinh = h.Hinh,
                           MoTaNgan = h.MoTa,
                           DonGia = h.DonGia,
                           TenLoai = h.MaLoaiNavigation != null ? h.MaLoaiNavigation.TenLoai : "Chưa phân loại"
                       })
                       .FirstOrDefault();

            if (hh == null)
                return NotFound();

            return View(hh);
        }
    }
}
