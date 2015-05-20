using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICAs_ICA13_ShawnHough : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillCustomersDDL("", _ddlCustomers);
        }

    }

    private void FillCustomersDDL(string sFilter, DropDownList ddl)
    {
        ddl.DataSource = NorthwindAccess.SQLDataReader(sFilter);
        ddl.DataValueField = "CustomerID";
        ddl.DataTextField = "CompanyName";
        ddl.Items.Clear();
        ddl.DataBind();
        ddl.AppendDataBoundItems = true;
        ddl.Items.Insert(0, new ListItem("Now Pick a Company from [" + ddl.Items.Count + "]", "0"));

    }


    protected void _btnFilter_Click(object sender, EventArgs e)
    {
        string sFilter = _cFilter.Text;
        FillCustomersDDL(sFilter, _ddlCustomers);
        _gvCustomerSum.Visible = false;

    }
    protected void _ddlCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sID = _ddlCustomers.SelectedValue;
        _gvCustomerSum.DataSource = NorthwindAccess.CustomerCategorySummary(sID);
        _gvCustomerSum.DataBind();
        _gvCustomerSum.Visible = true;
    }
    protected void _gvCustomerSum_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e == null || e.Row == null || e.Row.DataItem == null)
            return;
        
        if (e.Row.RowType != DataControlRowType.Header)
        {
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            e.Row.ForeColor = Color.BlueViolet;
            string s = e.Row.Cells[2].Text;
            decimal d = decimal.Parse(s);
            e.Row.Cells[2].Text = "$" + Math.Round(d, 2).ToString();
        }
    }
}