use Jumper

create table Agent
(
	Id int primary key identity not null,
	Type nvarchar(max)  null,
	[Name] nvarchar(max) not null,
	Email nvarchar(max)  null,
	Phone nvarchar(max)  null,
	Icon nvarchar(max) null,
	Address nvarchar(max) null,
	Priority nvarchar(max) null,
	Director nvarchar(max) null,
	INN nvarchar(max) null,
	KPP nvarchar(max) null
)

create table Product 
(
	Id int primary key identity not null,
	Name nvarchar(max) not null,
	Type nvarchar(max) null,
	Articul nvarchar(max) null,
	HumansCount int null,
	SubDivision int null,
	MinimalCost nvarchar(max) null
)

create table ProductSale 
(
	Id int primary key identity not null,
	SaleDate DateTime null,
	ProductQuantity int null,
	ProductId int foreign key references Product(Id) on delete cascade null,
	AgentId int foreign key references Agent(Id) on delete cascade null
)