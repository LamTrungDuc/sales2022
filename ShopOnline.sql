use master
go

drop database ShopOnline
go
-- Tạo database ShopOnline_Demo
create database ShopOnline 
go
use ShopOnline 
go
-- 1: Tạo Table [Accounts] chứa tài khoản thành viên được phép sử dụng các trang quản trị ----
create table TaiKhoan
(
	taiKhoan varchar(20) primary key not null,
	matKhau varchar(20) not null,
	hoDem nvarchar(50) null,
	tenTV nvarchar(30) not null,
	ngaysinh datetime ,
	gioiTinh bit default 1,
	soDT nvarchar(20),
	email nvarchar(50),
	diaChi nvarchar(250),
	trangThai bit default 0,
	ghiChu ntext
)
go

-- 2: Tạo Table [Customers] chứa Thông tin khách hàng  ---------------------------------------
create table KhachHang
(
	maKH varchar(10) primary key not null,
	tenKH nvarchar(50) not null,
	soDT varchar(20) ,
	email varchar(50),
	diaChi nvarchar(250),
	ngaySinh datetime ,
	gioiTinh bit default 1,
	ghiChu ntext
)
go

-- 3: Tạo Table [Articles] chứa thông tin về các bài viết phục vụ cho quảng bá sản phẩm, ------
--    xu hướng mua sắm hiện nay của người tiêu dùng , ...             ------------------------- 
create table BaiViet
(
	maBV varchar(10) primary key not null,
	tenBV nvarchar(250) not null,
	hinhDD varchar(max),
	ndTomTat nvarchar(2000),
	ngayDang datetime ,
	loaiTin nvarchar(30),
	noiDung nvarchar(4000),
	taiKhoan varchar(20) not null ,
	daDuyet bit default 0,
	foreign key (taiKhoan) references taiKhoan(taiKhoan) on update cascade 
)
go
-- 4: Tạo Table [LoaiSP] chứa thông tin loại sản phẩm, ngành hàng -----------------------------
create table LoaiSP
(
	maLoai int primary key not null identity,
	tenLoai nvarchar(88) not null,
	ghiChu ntext default ''
)
go
-- 4: Tạo Table [Products] chứa thông tin của sản phẩm mà shop kinh doanh online --------------
create table SanPham
(
	maSP varchar(10) primary key not null,
	tenSP nvarchar(500) not NULL,
	hinhDD varchar(max) DEFAULT '',
	ndTomTat nvarchar(2000) DEFAULT '',
	ngayDang DATETIME DEFAULT CURRENT_TIMESTAMP,
	maLoai int not null references LoaiSP(maLoai),
	noiDung nvarchar(4000) DEFAULT '',
	taiKhoan varchar(20) not null foreign key references taiKhoan(taiKhoan) on update cascade,
	dvt nvarchar(32) default N'Cái',
	daDuyet bit default 0,
	giaBan INTEGER DEFAULT 0,
	giamGia INTEGER DEFAULT 0 CHECK (giamGia>=0 AND giamGia<=100),
	nhaSanXuat nvarchar(168) default ''
)
go

-- 5: Tạo Table [Orders] chứa danh sách đơn hàng mà khách đã đặt mua thông qua web ------------
create table DonHang
(
	soDH varchar(10) primary key not null ,
	maKH varchar(10) not null foreign key references khachHang(maKH),
	taiKhoan varchar(20) not null foreign key references taiKhoan(taiKhoan) on update cascade ,
	ngayDat datetime,
	daKichHoat bit default 1,
	ngayGH datetime,
	diaChiGH nvarchar(250),
	ghiChu ntext
)
go	

-- 6: Tạo Table [OrderDetails] chứa thông tin chi tiết của các đơn hàng ---
--    mà khách đã đặt mua với các mặt hàng cùng số lượng đã chọn ---------- 
create table CtDonHang	
(
	soDH varchar(10) not null foreign key references donHang(soDH),
	maSP varchar(10) not null foreign key references sanPham(maSP),
	soLuong int,
	giaBan bigint,
	giamGia BIGINT,
	PRIMARY KEY (soDH, maSP)
)
go


/*========================== Nhập dữ liệu mẫu ==============================*/

-- YC 1: Nhập thông tin tài khoản, tối thiểu 5 thành viên sẽ dùng để làm việc với các trang: Administrative pages
insert into taiKhoan
values('duc','123',N'Lâm Trung','Đức',07/12/2000,1,0378149630,'minhminh@gmail.com','472 CMT8, P.11,Q12, TP.HCM',1,'')
insert into taiKhoan
values('admin','abc',N'Lâm Trung',N'Đức',07/12/2000,1,0378149630,'ltduc@gmail.com','472 CMT8, P.11,Q12, TP.HCM',1,'')
GO

insert into LoaiSP(tenLoai) values(N'Cây Ăn Trái')
insert into LoaiSP(tenLoai) values(N'Cây Nông Nghiệp')
insert into LoaiSP(tenLoai) values(N'Cây Lâm Nghiệp')
insert into LoaiSP(tenLoai) values(N'Cây Trồng')
insert into LoaiSP(tenLoai) values(N'Loại Cây Hoa')
insert into LoaiSP(tenLoai) values(N'Cây Lâu Năm')
go
-- YC3: Nhập thông tin bài viết, Tối thiểu 10 bài viết thuộc loại: giới thiệu sản phẩm, khuyến mãi, quảng cáo, ... 
--      liên quan đến sản phẩm mà bạn dự định kinh doanh trong đồ án sẽ thực hiện
-- Dụng cụ nhà bếp -------------------------------------------------------------------------------------------------------
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('SH1235', N'Hạt giống cây lúa', '/Asset/Images/sanPham/sp1.jpg',
			          N'Cây mang ý nghĩa cho sự no đủ, thuần khiết', 'admin',39000,1,10,N'VN',
					  N'Bit');
go


insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('NAG1452', N'Hạt giống Bắp cải', '/Asset/Images/sanPham/sp2.jpg',
			          N'Hạt giống bắp cải chịu nhiệt có khả năng chống bệnh thối đen mạnh mẽ,
					  héo Verticillium, chịu nhiệt tốt', 'admin',100000,5,7,N'HanQuac',
					  N'Bộ');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('SHG2703GA', N'Hạt giống ngô ngọt', '/Asset/Images/sanPham/sp3.jpg',
			          N'Ngô ngọt là cây sinh trưởng và phát triển mạnh, thời gian sinh trưởng ngắn (65-70) ngày,
					  thích ứng rộng với thời tiết nên có thể trồng quanh năm.', 'admin',129000,8,8,N'VN',
					  N'Bộ');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('NAG1306', N'Hạt giống chanh dây', '/Asset/Images/sanPham/sp4.jpg',
			          N'Khi chanh cảnh đã bắt đầu lớn thì dần dần giảm số lần nước xuống để cây phát triển vừa phải.', 'admin',75000,10,9,N'Nagakawa',
					  N'Bộ');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('FS-B3010', N'Hạt giống cà chua', '/Asset/Images/sanPham/sp5.jpg',
			          N'Hạt giống cà chua beef là giống F1 quả nặng trung bình 280gam - 350gam, 
					  quả màu đỏ, da sáng không tì vết, quả nhiều thịt. là loại cà chua ngon ngọt ', 'admin',55000,0,11,N'Fivestar Standard',
					  N'Bộ');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('SHG2303TEF', N'hạt giống cây Cherry', '/Asset/Images/sanPham/sp6.jpg',
			          N'Giống Cherry Mỹ nhiệt đới là loại cây thân gỗ cứng lâu năm, tạo bóng mát, 
					  có chiều đặt tới 10m, trung bình 3-7m.', 'admin',79000,0,10,N'Tefal',
					  N'Bộ');
go
-- Trang trí nội thất -------------------------------------------------------------------------------------------------------
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('4062373305', N'hạt giống cây cải', '/Asset/Images/sanPham/sp7.jpg',
			          N'Theo đông y, cải ngọt có tính ôn hòa, lợi trường vị, làm đỡ tức ngực, 
					  tiêu thực hạ khí…Nếu bạn thường xuyên ăn cải ngọt thì có thể chữa được các chứng ho, táo', 'admin',69000,10,8,N'OEM',
					  N'bit');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('8868354221', N'Hạt giống cây rau ngót', '/Asset/Images/sanPham/sp8.jpg',
			          N'Hạt giống rau ngót là giống cây quen thuộc, gần gũi, bình dị với con người Việt Nam.
					  Hiện nay, giống rau ngót ', 'admin',90000,5,9,N'IGEA',
					  N'Bit');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('1506749851', N'hạt giống cây rau má', '/Asset/Images/sanPham/sp9.jpg',
			          N'Hạt giống Rau má lá nhỏ là loại rau ăn lá dễ trồng, sinh trưởng phát triển tốt ở điều kiện thiếu ánh sáng. 
					  Rau má lá nhỏ thường dùng để ăn sống, nấu canh', 'admin',6850000,8,11,N'OEM',
					  N'Bit');
go
insert into sanPham (maSP, tenSP, hinhDD, ndTomTat, taiKhoan, giaBan, giamGia, maLoai, nhaSanXuat, dvt) 
              values('2759614408', N'hạt giống cây bẹ xanh', '/Asset/Images/sanPham/sp10.jpg',
			          N'Hạt giống rau Cải bẹ xanh mỡ là loài cây thuộc họ cải, có lá màu xanh nõn chuối,
					  mép lá có răng cưa, tán lá gọn, ăn vị ngọt, không đắng.', 'admin',49000,10,10,N'OEM',
					  N'Bit');
go

---- Cây ăn trái : Mã loại =7 tài khoản   : admin sản phẩm   .... cần tạo thêm nhớ tạo
insert into SanPham(maSP,tenSP,hinhDD,ndTomTat,ngayDang,maLoai,taiKhoan,dvt,noiDung,giaBan,giamGia,nhaSanXuat)
values('OTCHAUI001',N'HẠT GIỐNG ỚT SỪNG CHÂU PHI','/Asset/Images/sanPham/sp11.jpg',
		N'Ớt là một loại quả gia vị cũng như loại quả làm rau (ớt Đà Lạt) phổ biến trên thế giới,
		đặc biệt là ớt sừng vàng châu Phi. Cây ớt trồng trong chậu có thể làm một loại cây cảnh vì 
		quả ớt có nhiều màu sắc: trắng, đỏ, vàng, cam, xanh, tím… tùy theo giống cây.','2022/05/21',
		10,'admin','chuaco',
		N'',
		25000,0,'HatGiong');
go
insert into SanPham(maSP,tenSP,hinhDD,ndTomTat,ngayDang,maLoai,taiKhoan,dvt,noiDung,giaBan,giamGia,nhaSanXuat)
values('BIDOlaI002',N'HẠT GIỐNG BÍ ĐỎ LAI','/Asset/Images/sanPham/sp12.jpg',
		N'Bí đỏ hay còn gọi là bí ngô là một loại quả chứa rất nhiều giá trị dinh dưỡng tốt cho sức khỏe,
		giàu vitamin, sắt, đạm, chất béo và các loại axít amin hữu cơ tốt cho cơ thể. 
		Quả bí ngô được ưa chuộng trong chế biến các món như canh bí đỏ, đọt bí đỏ xào tỏi,… 
		ngoài ra mọi người cũng thường dùng bí đỏ để chế biến sữa bí đỏ uống rất tốt, giúp tăng cân, đẹp da, 
		tốt cho hệ tiêu hóa và ngăn ngừa các bệnh về tim mạch, xương khớp','2022/05/21',
		10,'admin','chuaco',
		N'',
		35000,0,'HatGiong');
go

---- Bài Viết  ../tạo ít nhất 6 bài viết
Insert into BaiViet(maBV,tenBV,hinhDD,ngayDang,ndTomTat,taiKhoan,loaiTin,daDuyet,noiDung)
values ('BV01',N'Cây ớt – Dặc điểm, cách trồng và chăm sóc cây ớt','/Asset/Images/sanPham/sp11.jpg',
		'2022/05/21',N'Cây Ớt là một trong những thành phần gia vị quen 
		thuộc trong ẩm thực hằng ngày của con người từ xa xưa tới nay','duc','COT',1,N'Cây ớt có tên khoa học là Capsicum,
		thuộc dòng họ Cà (Solanaceae),ngoài tên gọi phổ thông cây ớt còn được gọi với một số tên địa phương khác như: lạt tiêu,
		lạt tử, hải tiêu,…Ớt thuộc cây trồng lâu năm, thân dưới hóa rôc, chiều cao trung bình khoảng 40 – 70cm, phần thân có khoảng 4 cạnh, phân thành nhiều tán. Rễ có hình trụ, nhiều nhánh phụ phát triển nhanh tạo thành rễ chùm
Lá cây ớt là dạng lá đơn, có dạng hình trứng hoặc bầu dục, mọc thành chùm gồm 5 – 6 giống hình hoa thị, phiến lá nhọn dần ở đâu,
có màu xanh đậm, tùy thuộc vào giống mà có loại có lông hoặc không có lông. ')