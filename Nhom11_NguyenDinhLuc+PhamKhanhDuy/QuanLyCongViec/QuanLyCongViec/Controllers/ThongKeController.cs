using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Data;
using QuanLyCongViec.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuanLyCongViec.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly QuanLyCongViecContext _context;

        public ThongKeController(QuanLyCongViecContext context)
        {
            _context = context;
        }

        // Kiểm tra đăng nhập trước khi truy cập vào action
        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.GetString("UserId") != null;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "TaiKhoan");
            }

            // Truy xuất danh sách văn bản đến và đi
            var vanBanDen = _context.VanBanDen.AsQueryable();
            var vanBanDi = _context.VanBanDi.AsQueryable();

            // Tìm kiếm theo điều kiện
            if (!string.IsNullOrEmpty(searchString))
            {
                vanBanDen = vanBanDen.Where(v => v.NoiNhan.Contains(searchString)
                                                  || v.DonViGui.Contains(searchString)
                                                  || v.TrichYeu.Contains(searchString));

                vanBanDi = vanBanDi.Where(v => v.DonViNhan.Contains(searchString)
                                                || v.TrichYeu.Contains(searchString));
            }

            var viewModel = new ThongKeViewModel
            {
                VanBanDen = await vanBanDen.ToListAsync(),
                VanBanDi = await vanBanDi.ToListAsync()
            };

            return View(viewModel);
        }
    }
}
