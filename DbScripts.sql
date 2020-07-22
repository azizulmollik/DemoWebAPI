Create Table Customers(
CustomerId Int Identity(1,1) Not null Primary Key,
Name Varchar(50) Not null,
Email Varchar(50) Not null,
Password Varchar(20) Not null,
CreatedDate DateTime Default(GetDate()) Not Null)
GO
Create Table Products(
ProductId Int Identity(1,1) Primary Key,
Name Varchar(100) Not Null,
UnitPrice Decimal Not Null,
AvailableQuantity Int Not Null)
GO
Create Table Orders(
OrderId Int Identity(1,1) Primary Key,
CustomerId Int Not null,
TotalPrice Decimal Not Null,
OrderedDate DateTime Default(GetDate()) Not Null,
Status Int Not Null)
GO
Create Table OrderDetails(
Id Int Identity(1,1) Primary Key,
OrderId Int Not null,
ProductId Int Not null,
Quantity Int Not null,
Price Decimal Not Null,
FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
)
GO

--- Procedures
Create Procedure SP_UpdateOrderStatus
@orderId as int,
@status as int
As
BEGIN
	Update Orders Set Status=@status Where OrderId=@orderId;
END


Create Procedure SP_CustomerListByHasOrder
@hasOrder as bit
As
BEGIN
	IF @hasOrder=1
		BEGIN
			SELECT distinct a.* from Customers a
			INNER JOIN Orders b on b.CustomerId=a.CustomerId
		END
	ELSE
		BEGIN
			SELECT distinct a.* from Customers a
			left JOIN Orders b on b.CustomerId=a.CustomerId
			Where b.CustomerId IS null
		END
END



--Scaffold-DbContext "Server=DESKTOP-T4ES0UE;Database=DemoWebAPI;User Id=sa;Password=sa1234;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


--{"customerId":1,"totalPrice":600,"orderedDate":"2020-07-23T12:00:00","status":0,"orderDetails":[
--{"productId":1,"quantity":1,"price":100},
--{"productId":2,"quantity":1,"price":200},
--{"productId":3,"quantity":1,"price":300}
--]}

