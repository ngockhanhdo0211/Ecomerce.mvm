using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerceMVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Hshop2023Context db;

        public HomeController(Hshop2023Context context)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            // Load ra memory trước khi dùng ?? để tránh lỗi LINQ to Entities
            var hangHoas = db.HangHoas
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .ToList(); // load dữ liệu ra memory

            var data = hangHoas.Select(h => new HangHoaVM
            {
                MaHH = h.MaHh,
                TenHH = h.TenHh,
                Hinh = h.Hinh,
                MoTaNgan = h.MoTa,
                DonGia = h.DonGia  , // OK, đã ở memory
                TenLoai = h.MaLoaiNavigation?.TenLoai ?? "Chưa có loại",
                TenNcc = h.MaNccNavigation?.TenCongTy ?? "Chưa có NCC"
            }).ToList();

            return View(data);
        }
    }
}
