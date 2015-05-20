using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName;
        if (Page.PreviousPage != null && Page.PreviousPage.IsCrossPagePostBack)
        {
            object objUname = Page.PreviousPage.FindControl("_username");
            if (!(objUname is TextBox)) return;
            TextBox TB = objUname as TextBox;
            userName = TB.Text;

            if (TB.Text == "")
            {
                object obj = Page.PreviousPage.FindControl("_storage");
                if (!(obj is HiddenField)) return;
                HiddenField Hid = obj as HiddenField;
                userName = Hid.Value;
            }
            object obji = Page.PreviousPage.FindControl("_fileInfo");
            if (!(obji is HiddenField)) return;
            HiddenField pic = obji as HiddenField;
            _pictureInfo.Value = pic.Value;

            _hf.Value = userName;
            Response.Write("<label>" + userName + "'s Album</label><br/>");
            try
            {
                _phPictures.Controls.Clear();
                string path = MapPath(@"~/Images/" + userName + "dir/");
                if (Directory.Exists(path))
                {
                    DirectoryInfo DRI = new DirectoryInfo(path);
                    FileInfo[] files = DRI.GetFiles();
                    foreach (FileInfo FI in files)
                    {
                        if (FI.Extension == ".jpg")
                        {
                            System.Web.UI.WebControls.Image newImage = new System.Web.UI.WebControls.Image();
                            if (FI.ToString() == _pictureInfo.Value)
                            {
                                newImage.BorderColor = Color.Red;
                                newImage.BorderWidth = 20;
                                newImage.ImageUrl = "~/Images/" + userName + "dir/" + FI;
                                newImage.Height = 200;
                            }
                            else
                            {
                                newImage.ImageUrl = "~/Images/" + userName + "Dir/" + FI;
                                newImage.Height = 200;
                            }
                            _phPictures.Controls.Add(newImage);
                        }
                    }
                }
                else
                    Response.Write("<label>Directory not found</label>");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return;
            }
        }
        else
        {
            _backToLoad.Visible = false;
            Response.Write("Please go back to login.");
            _back.Visible = true;
        }
    }
}