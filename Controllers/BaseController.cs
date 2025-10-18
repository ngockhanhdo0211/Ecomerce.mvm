using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;  
using ECommerceMVC.ViewModels;
using ECommerceMVC.Helpers;

namespace ECommerceMVC.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            // Tính số lượng sản phẩm trong giỏ
            var gioHang = HttpContext.Session.Get<List<GioHangItem>>("GioHang") ?? new List<GioHangItem>();
            ViewBag.SoLuongGioHang = gioHang.Sum(p => p.SoLuong);
        }
    }
}
