using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    //1st to run on all pages load
    protected void Page_Load(object sender, EventArgs e)
    {
        //1st load, no previous asp OR postback??
        if (!IsPostBack)
        {
            //1st view of page by browser, not a post/button/event
            //initalize data
            _status.Text = "1st Load Init";
            _ddl.Items.Add(new ListItem("Jordan", "WOOOOOO"));

        }
       
    }
    protected void _btnPost_Click(object sender, EventArgs e)
    {
        string sTemp = _tbName.Text;
        int iRet = Util.ConvertString(sTemp);
        if (iRet < 0) //Bad input
        {
            _status.Text = " Not a number!!";
            _btnPost.BackColor = Color.Red;
        }
        else if (iRet % 2 == 0)
        {

            _status.Text = _tbName.Text + "  is Even";
            _btnPost.BackColor = Color.Green;
        }
        else
        {
            _status.Text = _tbName.Text + "  is Odd";
            _btnPost.ForeColor = Color.BurlyWood;
        }
    }
    protected void _ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util.UpdateStatusFromDDL(_ddl, _status);
    }
}
