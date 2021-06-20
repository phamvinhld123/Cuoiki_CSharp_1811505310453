Create database PhamVinhDB

CREATE TABLE UserAccount(
	ID bigint IDENTITY(1,1) primary key,
	UserName nvarchar(50) NULL,
	Password nvarchar(50) NULL,
	Status nvarchar(50) NULL
	)
	CREATE TABLE Category(
	ID bigint IDENTITY(1,1) primary key,
	Name nvarchar(50) NOT NULL,
	Description nvarchar(100) NULL,
)

CREATE TABLE Product(
	ID bigint IDENTITY(1,1) primary key,
	Name nvarchar(100) NOT NULL,
	UnitCost decimal(18,0) NULL,
	Quantity int NOT NULL,
	Image nvarchar(255) NULL,
	Description nvarchar(250) NULL,
	Status nvarchar(50) NULL,
	CategoryId bigint NOT NULL,
	constraint fk_Product_Category foreign key (CategoryId) references Category(ID)
)
	



	
INSERT INTO UserAccount(UserName, Password, Status) VALUES ( N'vinh', N'123', N'Hoạt động')
INSERT INTO UserAccount(UserName, Password, Status) VALUES ( N'khoa', N'123', N'Hoạt động')


INSERT INTO Product (Name, UnitCost, Quantity, Image, Description, Status) 
VALUES ( N'Phạm Vinh', N'MT', N'1', 1, N'Máy tính Dell Insprision 3559', N'Còn hàng'),
       ( N'Nguyễn Vũ Anh Khoa', N'MT', N'1', 1, N'Máy tính Dell Insprision 3559', N'Còn hàng')


INSERT INTO Category(Name, Description)
VALUES ( N'HD01', N'Dell')

