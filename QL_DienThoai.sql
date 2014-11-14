Create Database QL_DienThoai
go
use QL_DienThoai
go
Create Table NhaSanXuat
(
	MaNSX int Identity(1,1),--Mã NSX tăng tự động
	TenNSX nvarchar(255) NOT NULL,
	CONSTRAINT PK_NhaSanXuat PRIMARY KEY(MaNSX)
)

Create Table SanPham
(
	MaSP int Identity(1,1),--Mã sản phẩm tăng tự động
	TenSP nvarchar(255) NOT NULL,
	UrlHinh nvarchar(255),
	Gia int not null Check(Gia>0) default 0,--ràng buộc giá
	MoTa ntext,--kiểu dl văn bản lớn
	MaNSX int not null,
	SoLuong int Check(SoLuong>0) default 1 not null,	
	CONSTRAINT PK_SanPham PRIMARY KEY(MaSP)
)

Create Table KhachHang
(
	MaKH int Identity(1,1),--Mã khách hàng tăng tự động
	Username nvarchar(50) NOT NULL,
	Password nvarchar(50) not null,
	Email nvarchar(50) not null,
	HoTen nvarchar(50),
	GioiTinh nvarchar(50),
	NgaySinh date,
	DiaChi nvarchar(50),
	DienThoai varchar(11),
	CONSTRAINT PK_KhachHang PRIMARY KEY(MaKH)
)

CREATE TABLE DonHang
(
	MaDH INT IDENTITY(1,1),--Mã đơn hàng tăng tự động
	MaKH INT,
	Ngay DATE,
	Trigia int Check (Trigia>0),
	TinhTrang int,
	MaNV INT,
	CONSTRAINT PK_DonHang PRIMARY KEY(MaDH)
)


CREATE TABLE CT_DonHang
(
	MaDH INT,
	MaSP INT,
	SoLuong Int Check(SoLuong>0),
	Gia int Check(Gia>=0),
	CONSTRAINT PK_CT_DonHang PRIMARY KEY(MaDH,MaSP)
)

CREATE TABLE PhieuNhap
(
	MaPN INT IDENTITY(1,1),--Mã đơn hàng tăng tự động
	MaNPP INT,
	Ngay DATE,
	Trigia int Check (Trigia>0),
	MaNV INT,
	CONSTRAINT PK_NhapHang PRIMARY KEY(MaPN)
)

CREATE TABLE CT_PhieuNhap
(
	MaPN INT,
	MaSP INT,
	SoLuong Int Check(SoLuong>0),
	Gia int Check(Gia>=0),
	CONSTRAINT PK_CT_PhieuNhap PRIMARY KEY(MaPN,MaSP)
)

Create Table NhaPhanPhoi
(
	MaNPP int Identity(1,1),--Mã NSX tăng tự động
	TenNPP nvarchar(255) NOT NULL,
	DiaChi nvarchar(255) NOT NULL,
	Email nvarchar(255) NOT NULL,
	SoDT nvarchar(255) NOT NULL,
	CONSTRAINT PK_NhaPhanPhoi PRIMARY KEY(MaNPP)
)

Create Table NhanVien
(
	MaNV int Identity(1,1),--Mã NhanVien tăng tự động
	TenNV nvarchar(50) NOT NULL,
	Username nvarchar(50) NOT NULL,
	Password nvarchar(50) not null,
	Email nvarchar(50) not null,
	QuyenNV int not null,
	CONSTRAINT PK_NhanVien PRIMARY KEY(MaNV)
)

Create Table QuyenNhanVien
(
	MaQ int,
	TenQ nvarchar(50) NOT NULL,
	CONSTRAINT PK_QuyenNhanVien PRIMARY KEY(MaQ)
)

ALTER TABLE SANPHAM ADD CONSTRAINT FK_SP_NSX FOREIGN KEY (MaNSX) REFERENCES NHASANXUAT(MaNSX)
ALTER TABLE DONHANG ADD CONSTRAINT FK_DonHang_KhachHang FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH)
ALTER TABLE DONHANG ADD CONSTRAINT FK_DonHang_NhanVien FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV)
ALTER TABLE PHIEUNHAP ADD CONSTRAINT FK_PhieuNhap_NhaPhanPhoi FOREIGN KEY (MaNPP) REFERENCES NHAPHANPHOI(MaNPP)
ALTER TABLE PHIEUNHAP ADD CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV)
ALTER TABLE CT_DONHANG ADD CONSTRAINT FK_CT_DonHang_DonHang FOREIGN KEY (MaDH) REFERENCES DONHANG(MaDH)
ALTER TABLE CT_DONHANG ADD CONSTRAINT FK_CT_DonHang_SP FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
ALTER TABLE CT_PHIEUNHAP ADD CONSTRAINT FK_CT_PhieuNhap_SP FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
ALTER TABLE CT_PHIEUNHAP ADD CONSTRAINT FK_CT_PhieuNhap_PhieuNhap FOREIGN KEY (MaPN) REFERENCES PHIEUNHAP(MaPN)
ALTER TABLE NHANVIEN ADD CONSTRAINT FK_NhanVien_QuyenNhanVien FOREIGN KEY (QuyenNV) REFERENCES QUYENNHANVIEN(MaQ)






INSERT NhaSanXuat([TenNSX]) VALUES ('Apple')
INSERT NhaSanXuat([TenNSX]) VALUES ('Nokia')
INSERT NhaSanXuat([TenNSX]) VALUES ('Samsung')
INSERT NhaSanXuat([TenNSX]) VALUES (N'Hãng khác')


--Apple
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 5 32GB','iPhone-5-32GB.jpg',18850000,N'
Màn hình DVGA, 4.0 inches
Hệ điều hành: iOS 6
CPU: Dual-core 1.2 GHz
Camera: 8.0 MP
Dung lượng pin: 1440 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim, 01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 5 16GB','iPhone-5-16GB.jpg',16990000,N'
Màn hình DVGA, 4.0 inches
Hệ điều hành: iOS 6
CPU: Dual-core 1.2 GHz
Camera: 8.0 MP
Dung lượng pin: 1440 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 4s 16GB','iphone-4-4s.jpg',14490000,N'
Màn hình: DVGA, 3.5 inches
HĐH: iOS 5
CPU: Dual-core 1 GHz
Camera: 8.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 4 8GB','iphone-4-4s.jpg',10000000,N'
Màn hình: DVGA, 3.5 inches
HĐH: iOS 5.1
CPU: Solo-core 1 GHz
Camera: 5.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 4 16GB','iphone-4-4s.jpg',13000000,N'
Màn hình: DVGA, 3.5 inches
HĐH: iOS 5.1
CPU: Solo-core 1 GHz
Camera: 5.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 4 32GB','iphone-4-4s.jpg',15000000,N'
Màn hình: DVGA, 3.5 inches
HĐH: iOS 5.1
CPU: Solo-core 1 GHz
Camera: 5.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 4 64GB','iphone-4-4s.jpg',16200000,N'
Màn hình: DVGA, 3.5 inches
HĐH: iOS 5.1
CPU: Solo-core 1 GHz
Camera: 5.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('iPhone 4s 32GB','iphone-4-4s.jpg',15000000,N'
Màn hình: DVGA, 3.5 inches
HĐH: iOS 5.1
CPU: Solo-core 1 GHz
Camera: 5.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, cáp USB, 01 cây lấy sim,01 bộ sách hướng dẫn',1,30)
--Nokia
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Lumia 820','Nokia-Lumia-820.jpg',7990000,N'
Màn hình WVGA, 4.3 inches
HĐH: Windows Phone 8
CPU: Dual-core 1.5GHz
Camera: 8.0 MP
Dung lượng pin: 1650mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: - Máy Nokia Lumia 820, bộ sạc pin nhanh qua cổng USB Nokia AC-50, dây cáp dữ liệu và sạc pin Nokia CA-190CD, dây tai nghe Nokia Stereo WH-208, pin BP-5T (1650mAh), 2 vỏ ốp thân máy (cả 2 vỏ đều không có chức năng sạc pin không dây), sách hướng dẫn',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Lumia 920','Nokia-Lumia-920.jpg',10990000,N'
Màn hình: HD, 4.5 inches
HĐH: Windows Phone 8
CPU: Dual-core 1.5 GHz
Camera: 8.0 MP
Dung lượng pin: 2000 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, Pin BP - 4GW (2000mAh) , Sac AC-16, Cáp dữ liệu CA - 190CD , tai nghe Stereo WH -208, sách hướng dẫn',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Lumia 520','Nokia-Lumia-520.jpg',3840000,N'
Màn hình: WVGA, 4.0 inches
HĐH: Windows Phone 8
CPU: Dual-core 1 GHz
Camera: 5.0 MP
Dung lượng pin: 1430 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, cáp, tai nghe, sách hướng dẫn',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Lumia 620','Nokia-Lumia-620.jpg',5490000,N'
Màn hình WVGA, 3.8 inches
CPU: Dual-core 1GHz
HĐH: Windows Phone 8
Camera: 5.0 MP
Dung lượng pin: 1300 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: - Điện thoại Nokia Lumia 620, bộ sạc pin Nokia AC-50, pin Nokia BL-4J, 1300mAh, dây cáp dữ liệu và sạc pin Nokia CA-190CD, dây tai nghe Nokia WH-108, sách hướng dẫn ',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Asha 501','nokia-asha-501.jpg',1990000,N'
Màn hình: QVGA, 3.0 inches
HĐH: Nokia Asha platform 1.0
Hỗ trợ 2 Sim 2 sóng
Camera: 3.2 MP
Dung lượng pin: 1200 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, thẻ nhớ 4GB, sách hướng dẫn',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Lumia 720','nokia-lumia-720.jpg',7240000,N'
Màn hình: WVGA, 4.3 inches
HĐH: Windows Phone 8
CPU: Dual-core 1 GHz
Camera: 6.7 MP
Dung lượng pin: 2000 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, cáp, tai nghe, kim mở sim, sách hướng dẫn',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Asha 407','nokia-asha-501.jpg',1100000,N'
Màn hình: QVGA, 3.0 inches
HĐH: Nokia Asha platform 1.0
Hỗ trợ 2 Sim 2 sóng
Camera: 3.2 MP
Dung lượng pin: 1200 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, thẻ nhớ 4GB, sách hướng dẫn',2,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Nokia Lumia 420','nokia-lumia-720.jpg',5400000,N'
Màn hình: WVGA, 4.3 inches
HĐH: Windows Phone 8
CPU: Dual-core 1 GHz
Camera: 6.7 MP
Dung lượng pin: 2000 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, cáp, tai nghe, kim mở sim, sách hướng dẫn',2,30)
--Samsung
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy S4','Samsung-Galaxy-S4.jpg',15990000,N'
Màn hình: Full HD, 5.0 inches
HĐH: Android 4.2 (Jelly Bean)
CPU: 8 nhân, 2 lõi 4 nhân
Camera: 13 MP
Dung lượng pin: 2600 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Note2','Samsung-Galaxy-Note-II.jpg',13500000,N'
Màn hình HD, 5.5 inches
HĐH: Android 4.1 (Jelly Bean)
CPU: Quad-core 1.6 GHz
Camera: 8.0 MP
Dung lượng pin: 3100 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, cáp, sạc, tai nghe, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy Mega','samsung-galaxy-mega.jpg',11490000,N'
Màn hình: HD, 6.3 inches
HĐH: Android 4.2 (Jelly Bean)
CPU: Dual-core 1.7 GHz
Camera: 8.0 MP
Dung lượng pin: 3200 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, Pin , sạc, tai nghe, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy S3','Samsung-Galaxy-S3.jpg',10900000,N'
Màn hình HD, 4.8 inches
HĐH: Android 4.0.4 (ICS)
CPU: Quad-core 1.4 GHz
Camera: 8.0 MP
Dung lượng pin: 2100 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, cáp, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy Mega Duos','samsung-galaxy-mega-58.jpg',8990000,N'
Màn hình: qHD, 5.8 inches
HĐH: Android 4.2 (Jelly Bean)
CPU: Dual-core 1.4 GHz
Hỗ trợ 2 Sim 2 sóng
Dung lượng pin: 2600 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, Pin , sạc, tai nghe, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy Young','samsung-galaxy-young-s6310.jpg',2990000,N'
Màn hình: HVGA, 3.27 iches
HĐH: Android 4.1.2(Jelly Bean)
CPU: Solo-core 1 GHz
Camera: 3.15 MP
Dung lượng pin: 1300 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy Mega 2','samsung-galaxy-mega-58.jpg',5990000,N'
Màn hình: qHD, 5.8 inches
HĐH: Android 4.2 (Jelly Bean)
CPU: Dual-core 1.4 GHz
Hỗ trợ 2 Sim 2 sóng
Dung lượng pin: 2600 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, Pin , sạc, tai nghe, sách hướng dẫn',3,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Samsung Galaxy mini','samsung-galaxy-young-s6310.jpg',1990000,N'
Màn hình: HVGA, 3.27 iches
HĐH: Android 4.1.2(Jelly Bean)
CPU: Solo-core 1 GHz
Camera: 3.15 MP
Dung lượng pin: 1300 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, sách hướng dẫn',3,30)
--HangKhac
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('HTC One','HTC-One.jpg',15680000,N'
Màn hình: Full HD, 4.7 inches
HĐH: Android 4.1 (Jelly Bean)
CPU: Quad-core 1.7 GHz
Camera: 4.0 UltraPixel
Dung lượng pin: 2300 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, sạc, tai nghe, sách hướng dẫn',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('LG Optimus L5','LG-E612.jpg',3250000,N'
Màn hình HVGA, 4.0 inches
HĐH: Android 4.0.3 (ICS)
CPU: Solo-core 800MHz
Camera: 5.0 MP
Dung lượng pin: 1500 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, thẻ 4G, sách hướng dẩn',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('LG Optimus L3','lg-optimusL3E400.jpg',1690000,N'
Màn hình QVGA, 3.2 inches
HĐH: Android 2.3 (Gingerbread)
CPU: Solo-core 800 MHz
Camera: 3.15 MP
Dung lượng pin: 1500 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, thẻ 2G, sách hướng dẫn',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('K-Touch SmartOne','K-touch-SmartOne.jpg',1590000,N'
Màn hình: HVGA, 3.5 inches
HĐH: Android 2.3
CPU: Solo-core 800 MHz
Camera: 5.0 MP
Dung lượng pin: 1420 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy , pin , sạc, sách hướng dẫn , tai nghe',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Mobiistar Touch','mobiistar-touch.jpg',2590000,N'
Màn hình WVGA, 4.3 inches
HĐH: Android 4.0.4 (ICS)
CPU: Dual-core 1GHz
Camera: 8.0 MP
Dung lượng pin: 1450 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, Pin, Sạc, Sách Hướng Dẫn',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('HTC Desire','HTC-Desire-C.jpg',2690000,N'
Màn hình: HVGA, 3.5 inches
HĐH: Android 4.0 (ICS)
CPU: Solo-Core 600 MHz
Camera: 5.0 MP
Dung lượng pin: 1230 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, cáp, sách hướng dẫn',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('Mobiistar Touch 2','mobiistar-touch.jpg',1590000,N'
Màn hình WVGA, 4.3 inches
HĐH: Android 4.0.4 (ICS)
CPU: Dual-core 1GHz
Camera: 8.0 MP
Dung lượng pin: 1450 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, Pin, Sạc, Sách Hướng Dẫn',4,30)
INSERT SanPham([TenSP],[UrlHinh],[Gia],[MoTa],[MaNSX],[SoLuong]) VALUES ('HTC Desire 2','HTC-Desire-C.jpg',1690000,N'
Màn hình: HVGA, 3.5 inches
HĐH: Android 4.0 (ICS)
CPU: Solo-Core 600 MHz
Camera: 5.0 MP
Dung lượng pin: 1230 mAh
Bảo hành chính hãng 12 tháng
Bộ sản phẩm gồm có: Thân máy, pin, sạc, tai nghe, cáp, sách hướng dẫn',4,30)







INSERT KhachHang([Username],[Password],[Email],[HoTen],[GioiTinh],[NgaySinh],[DiaChi],[DienThoai]) VALUES ('dungit','123123','dungit@yahoo.com',N'Đào Anh Dũng','Nam','6/3/1993',N'123 Xóm lều','01659033525')
INSERT KhachHang([Username],[Password],[Email],[HoTen],[GioiTinh],[NgaySinh],[DiaChi],[DienThoai]) VALUES ('nghiait','123123','nghiait@yahoo.com',N'Trần Trọng Nghĩa','Nam','1/4/1993',N'123 Xóm lá','01688344525')
INSERT KhachHang([Username],[Password],[Email],[HoTen],[GioiTinh],[NgaySinh],[DiaChi],[DienThoai]) VALUES ('phetit','123123','phetit@yahoo.com',N'Dương Thành Phết',N'Nữ','6/2/1994',N'123 Xóm đất','0905649148')
INSERT KhachHang([Username],[Password],[Email],[HoTen],[GioiTinh],[NgaySinh],[DiaChi],[DienThoai]) VALUES ('Muait','123123','muait@yahoo.com',N'Trần Công Mua',N'Nữ','2/4/1994',N'123 Xóm chùa','0905649547')
INSERT KhachHang([Username],[Password],[Email],[HoTen],[GioiTinh],[NgaySinh],[DiaChi],[DienThoai]) VALUES ('Duongit','123123','duongit@yahoo.com',N'Đào Anh Dương',N'Nữ','4/3/1994',N'123 Xóm chợ','0905123524')
INSERT KhachHang([Username],[Password],[Email],[HoTen],[GioiTinh],[NgaySinh],[DiaChi],[DienThoai]) VALUES ('Minhtit','123123','minhit@yahoo.com',N'Đào Anh Minh',N'Nữ','5/2/1994',N'123 Xóm mít','0905654588')

INSERT QuyenNhanVien([MaQ],[TenQ]) VALUES (1,'Admin')
INSERT QuyenNhanVien([MaQ],[TenQ]) VALUES (2,N'Nhân viên')

INSERT NhanVien([TenNV],[Username],[Password],[Email],[QuyenNV]) VALUES (N'Nguyễn Văn A','admin','1231231','admin@gmail.com',1)
INSERT NhanVien([TenNV],[Username],[Password],[Email],[QuyenNV]) VALUES (N'Trần Hoàng Dũng','user','1231231','user@gmail.com',2)

INSERT DonHang([MaKH],[Ngay],[Trigia],[TinhTrang],[MaNV]) VALUES (1,'3/7/2013',75400000,0,1)
INSERT DonHang([MaKH],[Ngay],[Trigia],[TinhTrang],[MaNV]) VALUES (2,'3/7/2013',101940000,1,1)
INSERT DonHang([MaKH],[Ngay],[Trigia],[TinhTrang],[MaNV]) VALUES (3,'3/7/2013',14490000,0,1)




INSERT CT_DonHang([MaDH],[MaSP],[SoLuong],[Gia]) VALUES (1,1,4,18850000)
INSERT CT_DonHang([MaDH],[MaSP],[SoLuong],[Gia]) VALUES (2,2,6,16990000)
INSERT CT_DonHang([MaDH],[MaSP],[SoLuong],[Gia]) VALUES (3,3,1,14490000)

INSERT NhaPhanPhoi([TenNPP],[DiaChi],[Email],[SoDT]) VALUES (N'Nokia Việt Nam',N'Hà Nội','nokiavietnam@gmail.com','0979333707')
INSERT NhaPhanPhoi([TenNPP],[DiaChi],[Email],[SoDT]) VALUES (N'Digiworld',N'TP.HCM','digiworld@gmail.com','01679346643')





--Chạy riêng hàm tìm kiếm
Create Proc TimKiemSP
@Hang int ,@GiaTu int, @GiaDen Int
as
Select MaSP, TenSP, UrlHinh, Gia, MoTa, SoLuong, MaNSX
From SanPham
Where  
		SoLuong > 0
		And MaNSX like @Hang
		And Gia Between @Giatu and @Giaden
		ORDER BY Gia DESC
