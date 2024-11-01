using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Models;

namespace QuanLyCongViec.Data
{
    public class QuanLyCongViecContext : DbContext
    {
        public QuanLyCongViecContext(DbContextOptions<QuanLyCongViecContext> options)
            : base(options)
        {
        }

        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<VanBanDen> VanBanDen { get; set; }
        public DbSet<VanBanDi> VanBanDi { get; set; }
    }
}