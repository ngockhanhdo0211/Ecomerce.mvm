using ECommerceMVC.Data;                // DbContext + entity classes
using ECommerceMVC.Helpers;             // nếu bạn đặt SessionExtensions ở đây
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceMVC.Controllers
{
    public class KhachHangController : BaseController
    {
        private readonly Hshop2023Context _context;

        public KhachHangController(Hshop2023Context context)
        {
            _context = context;
        }

        // Hàm hash MD5 (demo, thực tế nên dùng hashing mạnh hơn)
        private string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (var b in data)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // GET: /KhachHang/DangKy
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        // POST: /KhachHang/DangKy
        [HttpPost]
        public IActionResult DangKy(KhachHang kh)
        {
            if (!ModelState.IsValid)
            {
                return View(kh);
            }

            // kiểm tra Email tồn tại chưa
            var exists = _context.KhachHangs.Any(x => x.Email == kh.Email);
            if (exists)
            {
                ViewBag.Error = "Email đã được sử dụng!";
                return View(kh);
            }

            kh.MatKhau = GetMd5Hash(kh.MatKhau ?? "");
            kh.HieuLuc = true;
            kh.VaiTro = false;

            _context.KhachHangs.Add(kh);
            _context.SaveChanges();

            return RedirectToAction("DangNhap");
        }

        // GET: /KhachHang/DangNhap
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: /KhachHang/DangNhap
        [HttpPost]
        public IActionResult DangNhap(string email, string matKhau)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            string hashed = GetMd5Hash(matKhau);

            //  Đăng nhập theo Email
            var kh = _context.KhachHangs.FirstOrDefault(k =>
                k.Email == email && k.MatKhau == hashed &&  k.HieuLuc == true);

            if (kh == null)
            {
                ViewBag.Error = "Sai thông tin đăng nhập hoặc tài khoản chưa kích hoạt!";
                return View();
            }

            // Lưu session
            HttpContext.Session.SetString("MaKh", kh.MaKh);
            HttpContext.Session.SetString("HoTen", kh.HoTen ?? kh.MaKh);

            return RedirectToAction("Index", "Home");
        }

        // GET: /KhachHang/DangXuat
        public IActionResult DangXuat()
        {
            HttpContext.Session.Remove("MaKh");
            HttpContext.Session.Remove("HoTen");
            return RedirectToAction("Index", "Home");
        }
    }
}
