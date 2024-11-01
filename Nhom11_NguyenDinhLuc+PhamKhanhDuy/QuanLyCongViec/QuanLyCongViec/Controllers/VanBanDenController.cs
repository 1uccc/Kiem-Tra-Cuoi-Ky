using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Data;
using QuanLyCongViec.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuanLyCongViec.Controllers
{
    public class VanBanDenController : Controller
    {
        private readonly QuanLyCongViecContext _context;

        public VanBanDenController(QuanLyCongViecContext context)
        {
            _context = context;
        }

        // Kiểm tra đăng nhập trước mỗi action
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

        // GET: VanBanDen
        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            return View(await _context.VanBanDen.ToListAsync());
        }

        // GET: VanBanDen/Create
        public IActionResult Create()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            return View();
        }

        // POST: VanBanDen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoVanBan,NgayNhan,TrichYeu,DonViGui,NoiNhan")] VanBanDen vanBanDen)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(vanBanDen);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }
            return View(vanBanDen);
        }

        // GET: VanBanDen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            if (id == null)
            {
                return NotFound();
            }

            var vanBanDen = await _context.VanBanDen.FindAsync(id);
            if (vanBanDen == null)
            {
                return NotFound();
            }
            return View(vanBanDen);
        }

        // POST: VanBanDen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaVanBanDen,SoVanBan,NgayNhan,TrichYeu,DonViGui,NoiNhan")] VanBanDen vanBanDen)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            if (id != vanBanDen.MaVanBanDen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vanBanDen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VanBanDenExists(vanBanDen.MaVanBanDen))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vanBanDen);
        }

        // GET: VanBanDen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            if (id == null)
            {
                return NotFound();
            }

            var vanBanDen = await _context.VanBanDen
                .FirstOrDefaultAsync(m => m.MaVanBanDen == id);
            if (vanBanDen == null)
            {
                return NotFound();
            }

            return View(vanBanDen);
        }

        // POST: VanBanDen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            var vanBanDen = await _context.VanBanDen.FindAsync(id);
            if (vanBanDen != null)
            {
                _context.VanBanDen.Remove(vanBanDen);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VanBanDenExists(int id)
        {
            return _context.VanBanDen.Any(e => e.MaVanBanDen == id);
        }
    }
}
