using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        _cal.VisibleDate = DateTime.Now.AddMonths(3);
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        _status.Text = _cal.SelectedDate.ToShortDateString();
    }
}