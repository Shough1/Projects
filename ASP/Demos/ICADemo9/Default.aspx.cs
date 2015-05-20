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
        if (!IsPostBack)
        {
            _mv.ActiveViewIndex = 1;
        }

    }
    protected void ViewControl(object sender, CommandEventArgs e)
    {
        if(sender == _btnFirstView)
        {
            _mv.ActiveViewIndex = 0;
        }

        if (sender == _btnNextView || sender == _btnNextView2)
            _mv.ActiveViewIndex = ((_mv.ActiveViewIndex + 1) % _mv.Views.Count);
        if(_mv.ActiveViewIndex == 2) //view 3
        {
            _ph.Controls.Clear();
            while (_ph.Controls.Count < 20)
            {
                Button btn = new Button();
                btn.Text = "Im a button : " + (_ph.Controls.Count+1);
                _ph.Controls.Add(btn);
            }
        }
    }
    protected void FileUploadProcess(object sender, CommandEventArgs e)
    {
        if (!_fu.HasFile) return;
        if (_fu.PostedFile.ContentType != "image/jpeg" && _fu.PostedFile.ContentType != "image/png")
        {
            //status update -> BAD user data
            return;
        }
        //Save file... but where?
        // >> use MapPath(web_path) returnes File System Path
        string DestFolder = MapPath(@"~/Images");
        string sFileName = _fu.FileName;
        try
        {
            _fu.SaveAs(DestFolder + @"\" + sFileName);
        }
        catch (Exception)
        {

            return;
        }
        System.IO.FileInfo fi = new System.IO.FileInfo(DestFolder + @"\" + sFileName);
        _fuLabelUpdate.Text = "Saved file : " + fi.FullName + " of size: " + fi.Length + " mb";
    }
}