using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ica10_ShawnHough : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _gvProducts.Visible = false;
            _gvEmployee.Visible = false;
        }
    }
    protected void _gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;

        DataRowView drv = e.Row.DataItem as DataRowView;



        if ((Int16)drv["UnitsInStock"] < 20 && (Int16)drv["UnitsOnOrder"] < 5)
        {
          e.Row.BackColor = Color.Cyan;
          e.Row.Cells[4].BackColor = Color.GreenYellow;
        }
        else if ((Int16)drv["UnitsInStock"] < 25)
        {
            e.Row.BackColor = Color.LightSalmon;
        }
       if ((Decimal)drv["UnitPrice"] > 25)
        {
            e.Row.Cells[3].BackColor = Color.Yellow;
        }
        
        
      
    }
    protected void _btnShowProd_Click(object sender, EventArgs e)
    {
        _gvProducts.Visible = true;
        _gvEmployee.Visible = false;
    }
    protected void _btnEmpEdit_Click(object sender, EventArgs e)
    {
        _gvProducts.Visible = false;
        _gvEmployee.Visible = true;
    }

    protected void _gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;
    }
    protected void _gvEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void _gvProducts_DataBound(object sender, EventArgs e)
    {

    }
}