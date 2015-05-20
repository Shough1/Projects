CREATE PROCEDURE spUpdateForSale
@howMuch money,
@titleType char(12),
@status varchar(64) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @rows int = 0

	update titles
	set price = price + @howMuch
	where type like @titleType
	set @rows = @@ROWCOUNT
	set @status = 'Updated [' + @titleType + '] : ' + cast( @rows as varchar ) + ' records' 
	return @rows
END
GO
declare @status varchar(64)
declare @ret int
exec @ret = spUpdateForSale 5, 'business', @status output
print @status
print @ret

select * from titles t where t.type like 'business'

declare @rows int = 10
declare @titleType char(12) = 'business'
declare @status varchar(64)
set @status = 'Updated [' + @titleType + '] : ' + cast( @rows as varchar ) + ' records' 
print @status