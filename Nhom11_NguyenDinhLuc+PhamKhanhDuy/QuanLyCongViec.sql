CREATE DATABASE QuanLyCongViec;
GO

USE QuanLyCongViec;
GO

CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100)
);

CREATE TABLE VanBanDen (
    MaVanBanDen INT IDENTITY(1,1) PRIMARY KEY,
    SoVanBan NVARCHAR(50) NOT NULL,
    NgayNhan DATE NOT NULL,
    TrichYeu NVARCHAR(500) NOT NULL,
    DonViGui NVARCHAR(200) NOT NULL,
    NoiNhan NVARCHAR(200) NOT NULL
);

CREATE TABLE VanBanDi (
    MaVanBanDi INT IDENTITY(1,1) PRIMARY KEY,
    SoVanBan NVARCHAR(50) NOT NULL,
    NgayGui DATE NOT NULL,
    TrichYeu NVARCHAR(500) NOT NULL,
    DonViNhan NVARCHAR(200) NOT NULL
);
-- Thêm văn bản đến
INSERT INTO VanBanDen (SoVanBan, NgayNhan, TrichYeu, DonViGui, NoiNhan)
VALUES 
    (N'0012024', '2024-11-01', N'Báo cáo tình hình kinh doanh', N'Phòng Kinh Doanh', N'Phòng Kế Toán'),
    (N'0022024', '2024-11-01', N'Yêu cầu báo giá vật tư', N'Phòng Mua Sắm', N'Phòng Kinh Doanh'),
    (N'0032024', '2024-11-01', N'Thông báo điều chỉnh chính sách', N'Phòng Nhân Sự', N'Tất cả các phòng ban');

-- Thêm văn bản đi
INSERT INTO VanBanDi (SoVanBan, NgayGui, TrichYeu, DonViNhan)
VALUES 
    (N'0012024', '2024-11-01', N'Thông báo nghỉ lễ', N'Tất cả các phòng ban'),
    (N'0022024', '2024-11-01', N'Phê duyệt dự toán ngân sách', N'Phòng Tài Chính'),
    (N'0032024', '2024-11-01', N'Gửi hợp đồng mua bán', N'Phòng Kinh Doanh');
