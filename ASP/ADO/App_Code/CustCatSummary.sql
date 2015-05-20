SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure CustCatSummary
	@customerID nchar(5)
AS
BEGIN
	SELECT CAT.CategoryName, SUM(OD.Quantity) as 'Total', SUM(OD.UnitPrice * OD.Quantity) as 'Cost'
	FROM Customers C inner join Orders O on C.CustomerID = O.CustomerID
					 inner join [Order Details] OD on O.OrderID = OD.OrderID
					 inner join Products P on OD.ProductID = P.ProductID
					 inner join Categories CAT on P.CategoryID = CAT.CategoryID
	WHERE C.CustomerID = @customerID
	GROUP BY CAT.CategoryName
	Order by 'Total' desc
END
GO

execute CustCatSummary 'CHOPS'
go

