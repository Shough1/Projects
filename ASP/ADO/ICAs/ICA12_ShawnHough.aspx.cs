using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICAs_ICA12 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDropList("");
        }

    }
    protected void FillDropList(string filter)
    {

        _ddlCompanyName.DataSource = NorthwindAccess.GetSuppliersSDS(filter);
        _ddlCompanyName.DataValueField = "SupplierID";
        _ddlCompanyName.DataTextField = "CompanyName";
        _ddlCompanyName.Items.Clear();
        _ddlCompanyName.DataBind();
        _ddlCompanyName.AppendDataBoundItems = true;
        _ddlCompanyName.Items.Insert(0, new ListItem("Now Pick a Company from [" + _ddlCompanyName.Items.Count + "]", "0"));
        _ddlCompanyName.AutoPostBack = true;


    }
    protected void _btnFilter_Click(object sender, EventArgs e)
    {
        string sFilter = _txbxFilter.Text;
        FillDropList(sFilter);
    }
    protected void _ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<List<string>> itemsList = NorthwindAccess.GetProducts(_ddlCompanyName.SelectedValue);
        int headers = 0;
        Label _lblCompany = new Label();
        _lblCompany.Text = _ddlCompanyName.SelectedItem.ToString();
        _phCompanyName.Controls.Add(_lblCompany);

        TableHeaderRow THR = new TableHeaderRow();
        headers = itemsList[0].Count;
        foreach (var items in itemsList[0])
        {
            TableHeaderCell THC = new TableHeaderCell();
            THC.Text = items;
            THC.BackColor = Color.LightBlue;
            THR.Controls.Add(THC);
        }
        itemsList.Remove(itemsList[0]);
        _tblProduct.Rows.Add(THR);


        foreach (var productInfo in itemsList)
        {
            TableRow TR = new TableRow();
            for (int x = 0; x < productInfo.Count; x++)
            {
                if (x % headers == 0)
                {
                    TR = new TableRow();
                }
                TableCell TC = new TableCell();
                TC.Text = productInfo[x];
                TR.Controls.Add(TC);
                _tblProduct.Rows.Add(TR);
            }
        }
    }
}