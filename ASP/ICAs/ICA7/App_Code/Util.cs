using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

/// <summary>
/// Summary description for util
/// </summary>
public static class Util
{
    public static Color RGBValues(byte bRed, byte bGreen, byte bBlue, bool grey)
    {
        if (grey == false)
        {
            return Color.FromArgb(bRed, bGreen, bBlue);
        }
        else
        {
            return Color.FromArgb(bRed, bRed, bRed);
        }
    }

    public static bool NameSaved(string colorName, Color clr, ListBox lbx)
    {
        if (lbx.Items.FindByText(colorName) == null && lbx.Items.FindByValue(clr.ToArgb().ToString()) == null)
        {
          return true; 
        }
        return false;
    }





    public static void LabelUpdate(Label colorLbl, Label statusLbl, ListBox lstbx)
    {
        string cValue = lstbx.SelectedItem.Value;
        int iColValue = int.Parse(cValue);
        string cText = lstbx.SelectedItem.Text;
        colorLbl.BackColor = Color.FromArgb(iColValue);
        statusLbl.ForeColor = Color.Green;
        statusLbl.Text = "Color - " + cText + " : Successfully Loaded";
    }
}