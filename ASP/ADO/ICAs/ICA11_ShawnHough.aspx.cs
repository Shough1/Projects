using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICAs_ICA11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            _ddlCustomers.AppendDataBoundItems = true;
            _ddlCustomers.AutoPostBack = true;
            _ddlCustomers.Items.Insert(0, new ListItem("Please Pick One..", "0"));
        }
        else
        {
            
        }

    }

    protected void _ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
            _lvOrders.SelectedIndex = -1;
            _lvOrders.EditIndex = -1;
            
    }
    protected void _lvOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void _lvOrders_ItemInserted(object sender, ListViewInsertedEventArgs e)
    {
    }
    protected void _ddlCustIDIns_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void _lvOrders_ItemDeleted(object sender, ListViewDeletedEventArgs e)
    {
    }
}