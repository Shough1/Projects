using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string s = MapPath(@"~/ICAs");
        DirectoryInfo DRI = new DirectoryInfo(s);
        FileInfo[] FI = DRI.GetFiles();
        Response.Clear();
        foreach (var f in FI)
        {
            
            HyperLink hyp = new HyperLink();
            hyp.NavigateUrl = "~/ICAs/" + f;
            if (f.Extension == ".aspx")
            {  
               
               hyp.Text = f.ToString()+"<br/>";
             
            }
            
            phTags.Controls.Add(hyp);
        }
        //Response.End();
    }
}