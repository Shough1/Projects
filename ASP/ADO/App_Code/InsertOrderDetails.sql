alter PROCEDURE InsertOrderDetails
	@orderID int,
	@productID int,
	@qnty smallint
AS
BEGIN
If not exists( select ProductID from Products where ProductID = @productID)
BEGIN
    return -1
END
If not exists( select OrderID from Orders where OrderID = @orderID)
BEGIN
	return -2
END
If exists( select OrderID, ProductID from [Order Details] where ProductID = @productID and OrderID = @orderID)
Begin
	return -3
END
ELSE
BEGIN
	declare @unitPrice money
	set @unitPrice =  (select UnitPrice  from Products where ProductID = @productID)	
	Insert Into [Order Details](OrderID,ProductID,UnitPrice,Quantity,Discount) Values (@orderID, @productID, @unitPrice, @qnty, 0)
RETURN @@RowCount
END
END

