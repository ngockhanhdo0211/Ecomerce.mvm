using ECommerceMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context _context;

        public HangHoaController(Hshop2023Context context)
        {
            _context = context;
        }

        // GET: Admin/HangHoa
        public async Task<IActionResult> Index()
        {
            var hangHoas = await _context.HangHoas
                .Include(h => h.Loai)
                .ToListAsync();
            return View(hangHoas);
        }

        // GET: Admin/HangHoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var hangHoa = await _context.HangHoas
                .Include(h => h.Loai)
                .FirstOrDefaultAsync(m => m.MaHh == id);

            if (hangHoa == null)
                return NotFound();

            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Create
        public IActionResult Create()
        {
            ViewData["Loai"] = _context.Loais.ToList();
            return View();
        }

        // POST: Admin/HangHoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HangHoa hangHoa, IFormFile? HinhUpload)
        {
            if (ModelState.IsValid)
            {
                if (HinhUpload != null)
                {
                    string fileName = Path.GetFileName(HinhUpload.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hinh/HangHoa", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HinhUpload.CopyToAsync(stream);
                    }

                    hangHoa.Hinh = fileName;
                }

                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Loai"] = _context.Loais.ToList();
            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
                return NotFound();

            ViewData["Loai"] = _context.Loais.ToList();
            return View(hangHoa);
        }

        // POST: Admin/HangHoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HangHoa hangHoa, IFormFile? HinhUpload)
        {
            if (id != hangHoa.MaHh)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhUpload != null)
                    {
                        string fileName = Path.GetFileName(HinhUpload.FileName);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Hinh/HangHoa", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhUpload.CopyToAsync(stream);
                        }

                        hangHoa.Hinh = fileName;
                    }

                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHh))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Loai"] = _context.Loais.ToList();
            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var hangHoa = await _context.HangHoas
                .Include(h => h.Loai)
                .FirstOrDefaultAsync(m => m.MaHh == id);

            if (hangHoa == null)
                return NotFound();

            return View(hangHoa);
        }

        // POST: Admin/HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoas.Any(e => e.MaHh == id);
        }
    }
}
