using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void _gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;

        DataRowView drv = e.Row.DataItem as DataRowView;
        // drv "looks" like a dictionary resultset for a row
        // ie. drv[column_name] => gets the data element for column for this row.
        string s = (string)drv["ContactName"];
        if (s.Contains("S"))
        {
            e.Row.BackColor = Color.Cornsilk;
            e.Row.Cells[3].BackColor = Color.Crimson;
        }
    }






































    protected void _sqlData_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}