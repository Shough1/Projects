using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DuckSeason : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.PreviousPage != null && Page.PreviousPage.IsCrossPagePostBack) // was there a previous asp page?
        {
            // try to retrieve the controls from the page we believe we came from...
           object obj = Page.PreviousPage.FindControl("_cal");
           if (!(obj is Calendar)) return;
           Calendar cal = obj as Calendar;
           DateTime dt = cal.SelectedDate;
           _status.Text = dt.ToLongDateString();

           Response.End();
        }
    }
}