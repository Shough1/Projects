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
    if (!Page.IsPostBack) // first load
    {
      FillDDL(_ddl, "");
    }
  }
  protected void FillDDL(DropDownList ddl, string sFilter)
  {
    ddl.AppendDataBoundItems = true;  // so we can add/insert later
    ddl.DataSource = PAC.GetTitles(sFilter); // hook up datasource
    ddl.DataTextField = "title";
    ddl.DataValueField = "title_id";
    ddl.Items.Clear(); // Empty the current, or DB retrieve will just add on
    ddl.DataBind(); // Refresh...sorta, DDL has items created
    ddl.Items.Insert(0, new ListItem("Yall, pick one", "0"));
    ddl.AutoPostBack = true;
  }
  protected void _ddl_SelectedIndexChanged(object sender, EventArgs e)
  {
    List<string> lst = PAC.GetSalesForTitle(_ddl.SelectedValue); // TitleID from dropdown
    _ddlItems.Items.Clear();
    foreach (var item in lst)
      _ddlItems.Items.Add(new ListItem(item));
    _output.Text = "Found " + lst.Count + " items";
  }
}