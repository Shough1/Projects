SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE spPubsTitles
	@filter varchar(24) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select titles.title_id, titles.title
	from titles
	where title like '%' + @filter + '%'
END
GO
execute spPubsTitles 'comp'

