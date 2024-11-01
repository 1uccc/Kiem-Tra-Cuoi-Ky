using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Data;
using QuanLyCongViec.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace QuanLyCongViec.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly QuanLyCongViecContext _context;

        public TaiKhoanController(QuanLyCongViecContext context)
        {
            _context = context;
        }

        // GET: TaiKhoan
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "TaiKhoan"); // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
            }

            var taiKhoan = await _context.TaiKhoan.FindAsync(int.Parse(userId));
            if (taiKhoan == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy tài khoản
            }

            return View(taiKhoan); // Trả về thông tin tài khoản
        }

        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTaiKhoan,TenDangNhap,MatKhau,HoTen,Email")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.MaTaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Tìm tài khoản hiện tại
                    var existingTaiKhoan = await _context.TaiKhoan.AsNoTracking().FirstOrDefaultAsync(t => t.MaTaiKhoan == id);
                    if (existingTaiKhoan == null)
                    {
                        return NotFound();
                    }

                    // Giữ tên đăng nhập cũ
                    taiKhoan.TenDangNhap = existingTaiKhoan.TenDangNhap;

                    // Nếu mật khẩu rỗng, giữ mật khẩu cũ
                    if (string.IsNullOrEmpty(taiKhoan.MatKhau))
                    {
                        taiKhoan.MatKhau = existingTaiKhoan.MatKhau; // Giữ mật khẩu cũ
                    }

                    // Cập nhật thông tin tài khoản
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.MaTaiKhoan))
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
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(m => m.MaTaiKhoan == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoan.Remove(taiKhoan);
                await _context.SaveChangesAsync();
            }

            // Xóa thông tin phiên làm việc
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "TaiKhoan"); // Quay lại trang đăng nhập
        }


        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoan.Any(e => e.MaTaiKhoan == id);
        }

        // GET: TaiKhoan/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // POST: TaiKhoan/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string tenDangNhap, string matKhau)
        {
            var taiKhoan = await _context.TaiKhoan
                .FirstOrDefaultAsync(m => m.TenDangNhap == tenDangNhap && m.MatKhau == matKhau);

            if (taiKhoan != null)
            {
                // Đăng nhập thành công, lưu thông tin vào session
                HttpContext.Session.SetString("UserId", taiKhoan.MaTaiKhoan.ToString());
                return RedirectToAction("Index", "Home");
            }

            // Thêm lỗi khi đăng nhập thất bại
            ModelState.AddModelError("LoginFailed", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View();
        }

        // POST: TaiKhoan/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa thông tin phiên làm việc
            return RedirectToAction("Index", "Home"); // Quay lại trang chính
        }

        // GET: TaiKhoan/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: TaiKhoan/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register([Bind("TenDangNhap,MatKhau,HoTen,Email")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tên đăng nhập đã tồn tại
                if (await _context.TaiKhoan.AnyAsync(t => t.TenDangNhap == taiKhoan.TenDangNhap))
                {
                    ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại.");
                    return View(taiKhoan);
                }

                // Thêm tài khoản mới
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("RegistrationFailed", "Vui lòng nhập đầy đủ và chính xác thông tin.");
            return View(taiKhoan);
        }
    }
}
