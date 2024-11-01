using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Data;
using QuanLyCongViec.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuanLyCongViec.Controllers
{
    public class VanBanDiController : Controller
    {
        private readonly QuanLyCongViecContext _context;

        public VanBanDiController(QuanLyCongViecContext context)
        {
            _context = context;
        }

        // Kiểm tra đăng nhập trước mỗi action
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

        // GET: VanBanDi
        public async Task<IActionResult> Index()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            return View(await _context.VanBanDi.ToListAsync());
        }

        // GET: VanBanDi/Create
        public IActionResult Create()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            return View();
        }

        // POST: VanBanDi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoVanBan,NgayGui,TrichYeu,DonViNhan")] VanBanDi vanBanDi)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            // Kiểm tra xem số văn bản đã tồn tại hay chưa
            bool exists = await _context.VanBanDi.AnyAsync(v => v.SoVanBan == vanBanDi.SoVanBan);
            if (exists)
            {
                ModelState.AddModelError("SoVanBan", "Số văn bản đã tồn tại. Vui lòng nhập số văn bản khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(vanBanDi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
                }
            }
            return View(vanBanDi);
        }
        // GET: VanBanDi/Edit/5
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

            var vanBanDi = await _context.VanBanDi.FindAsync(id);
            if (vanBanDi == null)
            {
                return NotFound();
            }
            return View(vanBanDi);
        }

        // POST: VanBanDi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaVanBanDi,SoVanBan,NgayGui,TrichYeu,DonViNhan")] VanBanDi vanBanDi)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            if (id != vanBanDi.MaVanBanDi)
            {
                return NotFound();
            }

            // Kiểm tra xem số văn bản đã tồn tại hay chưa (trừ văn bản hiện tại)
            bool exists = await _context.VanBanDi
                .AnyAsync(v => v.SoVanBan == vanBanDi.SoVanBan && v.MaVanBanDi != id);
            if (exists)
            {
                ModelState.AddModelError("SoVanBan", "Số văn bản đã tồn tại. Vui lòng nhập số văn bản khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vanBanDi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VanBanDiExists(vanBanDi.MaVanBanDi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(vanBanDi);

        }
        private bool VanBanDiExists(int id)
        {
            return _context.VanBanDi.Any(e => e.MaVanBanDi == id);
        }
        // GET: VanBanDi/Delete/5
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

            var vanBanDi = await _context.VanBanDi
                .FirstOrDefaultAsync(m => m.MaVanBanDi == id);
            if (vanBanDi == null)
            {
                return NotFound();
            }

            return View(vanBanDi);
        }

        // POST: VanBanDi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            var vanBanDi = await _context.VanBanDi.FindAsync(id);
            if (vanBanDi != null)
            {
                _context.VanBanDi.Remove(vanBanDi);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
