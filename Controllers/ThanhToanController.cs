using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ECommerceMVC.Controllers
{
    public class ThanhToanController : BaseController
    {
        private readonly Hshop2023Context _context;

        public ThanhToanController(Hshop2023Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Lấy giỏ hàng từ session
            var cart = HttpContext.Session.Get<List<GioHangItem>>("GioHang") ?? new List<GioHangItem>();
            ViewBag.TongTien = cart.Sum(x => x.ThanhTien);
            return View(cart);
        }

        [HttpPost]
        public IActionResult XacNhan()
        {
            var cart = HttpContext.Session.Get<List<GioHangItem>>("GioHang");
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "GioHang");
            }

            // 1️ Tạo đơn hàng (HoaDon)
            var hoadon = new HoaDon
            {
                MaKh = "ALFKI", //  Dùng mã có thật trong bảng KhachHang
                HoTen = "Maria Anders",
                DiaChi = "Obere Str. 57",
                NgayDat = DateTime.Now,
                CachThanhToan = "COD",
                CachVanChuyen = "Nhanh",
                GhiChu = "Thanh toán khi nhận hàng",
                MaTrangThai = 1
            };


            _context.HoaDons.Add(hoadon);
            _context.SaveChanges();

            // 2️ Tạo chi tiết đơn hàng (ChiTietHD)
            foreach (var item in cart)
            {
                var cthd = new ChiTietHd
                {
                    MaHd = hoadon.MaHd,
                    MaHh = item.MaHH,
                    SoLuong = item.SoLuong,
                    DonGia = (double)item.DonGia
                };
                _context.ChiTietHds.Add(cthd);
            }

            _context.SaveChanges();

            // 3️ Xóa giỏ hàng sau khi thanh toán
            HttpContext.Session.Remove("GioHang");

            return View("XacNhanThanhCong");
        }
    }
}
