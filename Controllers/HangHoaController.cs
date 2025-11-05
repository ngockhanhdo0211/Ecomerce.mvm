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
        // [ACTION] Danh sách sản phẩm + Lọc + Phân trang
        // =============================
        public IActionResult Index(int? maloai, string? keyword, int page = 1)
        {
            int pageSize = 8;

            // 1️⃣ Query base
            var query = db.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .AsQueryable();

            // 2️⃣ Lọc theo loại
            if (maloai.HasValue)
                query = query.Where(h => h.MaLoai == maloai.Value);

            // 3️⃣ Lọc theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim().ToLower();
                query = query.Where(h => h.TenHh.ToLower().Contains(keyword));
            }

            // 4️⃣ Phân trang
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var items = query
                .OrderBy(h => h.TenHh)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(h => new HangHoaVM
                {
                    MaHH = h.MaHh,
                    TenHH = h.TenHh,
                    Hinh = h.Hinh,
                    MoTaNgan = h.MoTa,
                    DonGia = h.DonGia,
                    TenLoai = h.MaLoaiNavigation != null ? h.MaLoaiNavigation.TenLoai : "Chưa phân loại"
                })
                .ToList();

            // 5️⃣ Truyền dữ liệu sang View
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Keyword = keyword;
            ViewBag.MaLoai = maloai;

            return View(items);
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

        // =============================
        // [ACTION] Kết quả tìm kiếm (hiển thị riêng)
        // =============================
        [HttpGet]
        public IActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return RedirectToAction("Index");

            var data = db.HangHoas
                .Where(h => h.TenHh.Contains(keyword))
                .Select(h => new HangHoaVM
                {
                    MaHH = h.MaHh,
                    TenHH = h.TenHh,
                    Hinh = h.Hinh,
                    DonGia = h.DonGia,
                    MoTaNgan = h.MoTa
                })
                .ToList();

            ViewBag.Keyword = keyword;
            return View("SearchResult", data);
        }

        // =============================
        // [ACTION] Gợi ý từ khóa (autocomplete)
        // =============================
        [HttpGet]
        public IActionResult Suggest(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Json(new { });

            var data = db.HangHoas
                .Where(h => h.TenHh.Contains(keyword))
                .Select(h => new
                {
                    maHH = h.MaHh,
                    tenHH = h.TenHh
                })
                .Take(5)
                .ToList();

            return Json(data);
        }
    }
}
