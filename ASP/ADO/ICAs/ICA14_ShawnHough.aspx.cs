using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICAs_ICA14_ShawnHough : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void _gvOrderDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void _btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string orderID = _gvOrderDetails.SelectedDataKey.Values["OrderID"].ToString();
            string productID = _gvOrderDetails.SelectedDataKey.Values["ProductID"].ToString();
            int iOrderID = int.Parse(orderID);
            int iProductID = int.Parse(productID);
            string message = NorthwindAccess.DeleteOrderDetails(iOrderID, iProductID);
            _lblStatus.Text = message;
            _gvOrderDetails.DataBind();
            _gvOrderDetails.SelectedIndex = -1;
        }
        catch(Exception err)
        {
            _lblStatus.Text = err.Message;
        }
    }

    protected void _btnInsert_Click(object sender, EventArgs e)
    {
        
            int orderID = int.Parse(_txbxOrderID2.Text);
            int productID = int.Parse(_ddlOrderDetails.SelectedValue);
            short qnty = short.Parse(_txbxQnty.Text);
            _lblStatus2.Text = NorthwindAccess.InsertOrderDetails(orderID, productID, qnty);
            _gvOrderDetails.DataBind();
            _gvOrderDetails.SelectedIndex = -1;
       

    }
}