

ALTER procedure DeleteOrderDetails
	@orderID int,
	@productID int,
	@message varchar(80) OUTPUT
AS
BEGIN
	DELETE FROM [Order Details] 
	WHERE OrderID = @orderID and ProductID = @productID
	if(@@ROWCOUNT > 0)
	BEGIN
		SET @message = 'Record Deleted'
	END
	ELSE
	BEGIN
		SET @message = 'No Records deleted, possible error'
	END

END
GO


