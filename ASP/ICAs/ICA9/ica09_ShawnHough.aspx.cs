using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    string userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _Status.Text = "Lets begin";
            _Status.BackColor = Color.Lime;
            _mv.ActiveViewIndex = 0;
            _btnGTAlb.Enabled = false;
            
        }
        if (Page.PreviousPage != null && Page.PreviousPage.IsCrossPagePostBack)
        {
            object objUname = Page.PreviousPage.FindControl("_hf");
            if (!(objUname is HiddenField)) return;
            HiddenField HF = objUname as HiddenField;
            userName = HF.Value;
            if (userName == "" || userName == null)
            {
                _mv.ActiveViewIndex = 0;
            }
            else
            {
                _mv.ActiveViewIndex = 1;
                _btnGTAlb.Enabled = true;
                _lblIntro.Text = "Thanks " + HF.Value + ", Now add to your album :";
                _storage.Value = userName;
            }
        }
        
        
    }

    protected void ViewControl(object sender, CommandEventArgs e)
    {
        if (_userName.Text == null || _userName.Text == "")
        {
            _userName.Focus();
            _Status.Text = "Username must contain a value";
            _Status.BackColor = Color.Red;


        }
        else if (_pswrd.Text == null || _pswrd.Text == "")
        {
            _pswrd.Focus();
            _Status.Text = "Password must contain a value";
            _Status.BackColor = Color.Red;

        }
        else
        {
            _storage.Value = _userName.Text;
            if (sender == _btnNext)
            {
                _mv.ActiveViewIndex = ((_mv.ActiveViewIndex + 1) % _mv.Views.Count);
                userName = _userName.Text;
            }
        }
        if (_mv.ActiveViewIndex == 1)
        {

            _btnGTAlb.Enabled = true;
            _lblIntro.Text = "Thanks " + _userName.Text + ", Now add to your album :";
        }
        
    }
     protected void FileUpload(object sender, CommandEventArgs e)
    {
        userName = _userName.Text;

        if (userName == "")
        {
            userName = _storage.Value;
        }

        string destFile = MapPath(@"~/Images/" + userName + "dir");
        string sFile = _fu.FileName;

        _fileInfo.Value = sFile;
        try
        {
            if (sender == _btnNext2)
            {
                try
                {
                    if (!Directory.Exists(destFile))
                    {
                        Directory.CreateDirectory(destFile);
                    }
                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                }
                _mv.ActiveViewIndex = ((_mv.ActiveViewIndex + 1) % _mv.Views.Count);
                if (_mv.ActiveViewIndex == 2)
                {
                    if (!_fu.HasFile)
                    {
                        _lblMStat.Text = "Still No file Selected";
                        _lblMStat.BackColor = Color.Red;

                    }
                    else if (_fu.PostedFile.ContentType != "image/jpeg")
                    {
                        _lblMStat.Text = "Bad file type, use only .jpeg";
                        _lblMStat.BackColor = Color.Red;

                    }
                    else
                    {
                        if (File.Exists(destFile + @"\" + sFile))
                        {
                            File.Delete(destFile + @"\" + sFile);
                        }
                        _fu.SaveAs(destFile + @"\" + sFile);
                        _lblMStat.Text = "Thanks " + userName + " - Added: " + _fu.FileName;
                        _lblMStat.BackColor = Color.Lime;
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            _lblMStat.BackColor = Color.Blue;
            _lblMStat.Text = "Error Uploading File";
            return;
        }
         catch(FileLoadException)
        {
            _lblMStat.BackColor = Color.Blue;
            _lblMStat.Text = "Error Uploading File";
            return;
        }
    }
}