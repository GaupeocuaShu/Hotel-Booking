Create database Project_63132986
use Project_63132986

create table UserInfo (
	ID int identity  Primary Key, 
	UserName varchar(255) not null, 
	UserPassword varchar(255) not null, 
	Email varchar(255) not null, 
	PhoneNumber varchar(255) not null, 
	ResetPasswordCode varchar(255) null,
	UserRole varchar(255) default 'user', 
	Avatar varchar(255) null,  
)

create table Position(
	ID int identity Primary Key, 
	PositionName nvarchar(255) not null, 
	Salary money not null, 
)

create table Employee (
	ID int identity Primary Key, 
	EmployeeName nvarchar(255) not null, 
	DateOfBirth datetime not null, 
	Email varchar(255),
	PhoneNumber varchar(255) not null, 
	Sex bit not null, 
	EmployeeAddress varchar(255) not null, 
	PositionID int Foreign Key References Position(ID), 
	Avatar varchar(255) null, 
)

create table RoomType(
	ID int identity Primary Key, 
	RoomTypeName nvarchar(255) not null, 
	Price money not null, 
)

create table Room(
	ID int identity Primary Key, 
	RoomName nvarchar(255) not null, 
	RoomTypeID int Foreign Key References RoomType(ID),
	RoomStatus bit not null default 1,  
	RoomDescription text not null,  
	RoomFeature text not null, 
)

create table RoomImageGallery(
	ID int identity Primary Key,
	RoomID int Foreign Key References Room(ID) not null, 
	RoomImage varchar(255) not null,
)

create table HotelService(
	ID int identity Primary Key, 
	ServiceName nvarchar(255) not null, 
	Price money not null, 
	ServiceDescription text not null, 
	ServiceIcon varchar(255) not null, 
)

create table ServiceImage(
	ID int identity Primary Key, 
	ServiceID int Foreign Key References HotelService(ID), 
	ServiceImage varchar(255) not null, 
)

create table Customer(
	ID varchar(255) Primary Key, 
	CustomerName nvarchar(255) not null, 
	PhoneNumber varchar(255) not null, 
	DateOfBirth datetime not null, 
	CustomerAddress nvarchar(255) not null, 
	Sex bit not null,
)


create table BookedRoom(
	ID int identity Primary Key,
	RoomID int foreign key references Room(ID) not null, 
	CheckinType bit not null, 
	CheckinDay datetime not null,
	CheckoutDay datetime not null,
	UserID int foreign key references UserInfo(ID) null, 
	CustomerID varchar(255) foreign key references Customer(ID) null, 
	BookDate datetime not null, 
	PaymentStatus bit not null, 
	CheckinStatus bit not null, 
	BookingStatus nvarchar(255) not null, 
	CancelDay datetime null,
)

create table ServiceUsage(
	ID int identity Primary Key, 
	BookedRoomID int foreign key references BookedRoom(ID) not null, 
	ServiceID int foreign key references HotelService(ID) not null, 
)

create table Invoice(
	ID int identity Primary Key, 
	BookedRoomID int foreign Key References BookedRoom(ID) not null, 
	TotalPrice money not null, 
	TotalBookedDay int not null, 
	PaymentMethod int null, 
)
create table SliderBannerImage(
	ID int identity Primary Key, 
	BannerImage varchar(255) null, 
	ImageOrder int not null, 
	Link varchar(255) null,
	Title varchar(255) null, 
	Content varchar(255) null, 
)

