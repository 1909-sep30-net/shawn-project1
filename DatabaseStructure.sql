--Drop Table Customers
Create Table Customers(
	CustomerId uniqueidentifier NOT NULL Default newid() Primary Key,
	FirstName nvarchar(40) NOT NULL,
	LastName nvarchar(40) NOT NULL)

--Drop Table Locations
Create Table Locations(
	LocationId int NOT NULL Identity(10, 10) Primary Key,
	LocationName varchar(80) NOT NULL)

--Drop Table Products
Create Table Products(
	ProductId uniqueidentifier NOT NULL Default newid() Primary Key,
	ProductName nvarchar(80) NOT NULL,
	ProductDesc nvarchar(200) NOT NULL
	)

--Drop Table LocationStock
Create Table LocationStock(
	LocationId int Not Null,
	ProductId uniqueidentifier Not Null,
	Quantity int Not Null,
	CONSTRAINT FK_LocationStockProducts Foreign Key (ProductId) References Products(ProductId),
	CONSTRAINT FK_LocationStockLocations Foreign Key (LocationId) References Locations,
	Primary Key(LocationId, ProductId)
	)

--Drop Table Orders
Create Table Orders(
	OrderId uniqueidentifier NOT NULL Default newid() Primary Key,
	OrderDate smalldatetime not null DEFAULT Getdate(),
	CustomerID uniqueidentifier Not null,
	LocationId int Not null,
	CONSTRAINT FK_OrdersLocationId Foreign Key (LocationId) References Locations(LocationId),
	CONSTRAINT FK_OrdersCustomerId Foreign Key (CustomerId) References Customers(CustomerId)
	)

--Drop Table OrderItems
Create Table OrderItems(
	OrderId uniqueidentifier Not Null,
	ProductId uniqueidentifier Not Null,
	Quantity int,
	CONSTRAINT FK_OrderItemsProductId Foreign Key (ProductId) References Products(ProductId),
	Primary Key(OrderId, ProductId)
	)