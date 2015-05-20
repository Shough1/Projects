using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PrintableVersion : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.PreviousPage != null && Page.PreviousPage.IsCrossPagePostBack)
        {
            DateTime dt;
            object obj = Page.PreviousPage.FindControl("_lstbxAppoint");
            if (!(obj is ListBox)) return;
            ListBox listBox = (ListBox)obj;
            dt = new DateTime();
            Response.Clear();
            Response.Write("Your Appointments:");
            listBox.Items.RemoveAt(0);
            if (listBox.Items.Count > 0)
            {
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    dt = DateTime.Parse(listBox.Items[i].ToString());
                    Response.Write("<br/>[" + (i+1) + "] - " + dt.ToString("dddd MMMM dd, yyyy") + " : " + listBox.Items[i].Value);

                }
            }
            else
            {
                  return;
            }
            Response.End();
        }
    }
}