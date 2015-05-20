using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;



public partial class _Default : System.Web.UI.Page
{
    byte bRed, bGreen, bBlue;
    Color _clr;
    protected void Page_Load(object sender, EventArgs e)
    {
      if(!IsPostBack)
      {
             
          _radLstGreen.Items.Add(new ListItem("0%", "0"));
          _radLstGreen.Items.Add(new ListItem("50%","128"));
          _radLstGreen.Items.Add(new ListItem("100%","255"));
          _radLstGreen.Items[0].Selected = true;
          
          
      }
    }

    protected bool Parsed()
    {
        string sGreen = _radLstGreen.SelectedItem.Value;
        string sRed = _txtbxRed.Text;
        string sBlue = _ddlBlue.SelectedItem.Value;

        if (ConvertToByte(sRed, ref bRed) && ConvertToByte(sGreen,ref bGreen) && ConvertToByte(sBlue, ref bBlue))
            return true;
        else
            return false;
    }

    

    
    public static bool ConvertToByte(string str, ref byte bNum)
    {
       
       if (byte.TryParse(str, out bNum))
           return true;
       else
           return false;
    }

    
    protected void _btnPreview_Click(object sender, EventArgs e)
    {
        ColorCheck();
    }

    private void ColorCheck()
    {
        if (Parsed())
        {

            _clr = Util.RGBValues(bRed, bGreen, bBlue, _chkbxGrey.Checked);
            _lblColor.BackColor = _clr;
            _status.Text = "";
            _lblColor.Text = "";
        }
        else
        {
            _status.ForeColor = Color.Red;
            _status.Text = "Red Not A Byte!";
        }
    }
            

    protected void _btnSave_Click(object sender, EventArgs e)
    {
        ColorCheck();
        string sName = _txtbxName.Text;
        Color iColour = _lblColor.BackColor;
        
        if (sName == "" || sName == null)
        {
            _status.Text = "Please enter a color name";
        }
        else
        {
            if (Util.NameSaved(sName, iColour, _lstbxSaved))
            {
                _lstbxSaved.Items.Add(new ListItem(sName,iColour.ToArgb().ToString()));
                _status.ForeColor = Color.Green;
                _status.Text = sName + " : Successfully Added!";
            }
            else
            {
                _status.Text = "Color - already exists!";
                _status.ForeColor = Color.Red;
            }
        }
        

    }
    protected void _lstbxSaved_SelectedIndexChanged(object sender, EventArgs e)
    {
        Util.LabelUpdate(_lblColor, _status, _lstbxSaved);
    }
}