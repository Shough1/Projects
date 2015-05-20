SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE spSalesSummaryByTitle
	@title_id varchar(6) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select st.stor_name, sum(sa.qty * t.price) as "Sum Sales"
	from stores st inner join sales sa on st.stor_id = sa.stor_id
	inner join titles t on sa.title_id = t.title_id
	where t.title_id = @title_id
	group by st.stor_name
	order by st.stor_name
END
GO
