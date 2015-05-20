using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Binding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if( !Page.IsPostBack )
      {//init block
        _ddlPublishers.AppendDataBoundItems = true; // Allow insertions/additions after data fill
        _ddlPublishers.AutoPostBack = true;
        _ddlPublishers.Items.Insert(0, new ListItem("Please Pick One...", "0"));
      }
    }
    protected void _ddlPublishers_SelectedIndexChanged(object sender, EventArgs e)
    {
      // If the TOP MOST DDL has selected a different Publisher,
      //  the cascading effect should wash through and ensure we are
      //  not looking at "old", unrelated data.
      // BY setting the DataSource to null and then setting it back, it will
      //  refresh, or there exist some "Refresh()" method combinations that work too...
    }
}