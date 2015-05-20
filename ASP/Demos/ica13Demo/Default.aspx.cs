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
    if (!Page.IsPostBack)
    {
      FillList(""); // no filter to start
    }
  }
  protected void FillList(string str) // Allow filter for Titles
  {
    _lb.AppendDataBoundItems = false;
    _lb.AutoPostBack = true;
    _lb.DataSource = PAC.FillListBox(str);
    _lb.DataTextField = "title";
    _lb.DataValueField = "title_id";
    _lb.DataBind(); // Activate !!!
    _lb.AppendDataBoundItems = true;
    _lb.Items.Insert(0, new ListItem("Please Pick Me", "0"));
  }
  protected void _lb_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (_lb.SelectedValue == "0") // nothing picked
      return;
    string sTitleId = _lb.SelectedValue;
    int iRows = -99;
    _gv.DataSource = PAC.GetSummary(sTitleId, out iRows);
    _gv.DataBind(); // DONT FORGET THIS EITHER!!!!!!
  }
  protected void _btnFilter_Click(object sender, EventArgs e)
  {
    FillList(_tbFilter.Text);
  }
  // ica14 - DUI Code, lets update, we just don't have an easy way to verify
  //         the DB will have to select * from titles to see if price changed
  protected void _btnUpdate_Click(object sender, EventArgs e)
  {
    string sType = "business"; // title.type that we want updated, usually picked from list
    decimal updateValue = decimal.Parse(_tbFixed.Text); // amount we want to adjust the price of each
    string sStatus = ""; // output string, will come back with message from stored proc
    int iRet = -1; // the actual integer of rows affected by proc

    PAC.OnSale(sType, updateValue, out sStatus, out iRet); // call, remember out params
    // Dump these returned out parameters to our web page label
    _lbStatus.Text = sStatus + "<br/>Also got : " + iRet + " return value";
  }
}