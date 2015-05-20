SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter procedure GetCustomers
	@companyName varchar(25) = ''
as
BEGIN
IF(@companyName is null)
Begin
	Select CustomerID, CompanyName
	From Customers
End
Else
Begin
	--SET NONCOUNT ON;
 	Select CustomerID, CompanyName
	From Customers
	Where CompanyName like '%'+@companyName+'%'
END
End
GO

execute GetCustomers ''
go