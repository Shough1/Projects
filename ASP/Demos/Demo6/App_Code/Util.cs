using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for Util
/// </summary>
public static class Util
{
    public static int ConvertString(string str)
    {
        int iTry = 0;
        if(int.TryParse(str, out iTry))
        {
            return iTry;
        }
        return -1;
    }
    public static void UpdateStatusFromDDL( DropDownList ddl, Label lb)
    {
        lb.Text ="Got "+ddl.SelectedItem.Text+ " / "+ddl.SelectedItem.Value;
    }
}