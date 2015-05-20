using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Therapy : System.Web.UI.Page
{
    string Date;
    bool removed = false;
    ListItem LI;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            _lstbxAppoint.Items.Add("Dates to Remember");
        }
    }

    protected void _cal_SelectionChanged(object sender, EventArgs e)
    {
        Date = _cal.SelectedDate.ToLongDateString();

        if (!(_lstbxAppoint.Items.Contains(_lstbxAppoint.Items.FindByText(Date))))
        {
            if (Physio.Checked == true)
            {
                LI = new ListItem(Date, Physio.Text);

                StatusUpdate(Date);
                _lstbxAppoint.Items.Add(LI);
            }
            else if (Dental.Checked == true)
            {
                LI = new ListItem(Date, Dental.Text);
                StatusUpdate(Date);
                _lstbxAppoint.Items.Add(LI);
            }
            else if (Psycho.Checked == true)
            {
                LI = new ListItem(Date, Psycho.Text);
                StatusUpdate(Date);
                _lstbxAppoint.Items.Add(LI);
            }
        }
        else
        {
            _lstbxAppoint.Items.RemoveAt(_lstbxAppoint.Items.IndexOf(_lstbxAppoint.Items.FindByText(Date)));
            removed = true;
            StatusUpdate(Date);
        }
    }

    private void StatusUpdate(string s)
    {
        if (removed)
            _status.Text = "Removed your " + s + " Appointment";
        else
            _status.Text = "Added: " + Date;
    }
    protected void _cal_DayRender(object sender, DayRenderEventArgs e)
    {
        
        
        if (_lstbxAppoint.Items.Contains(_lstbxAppoint.Items.FindByText(e.Day.Date.ToLongDateString())))
        {   string s = _lstbxAppoint.Items.FindByText(e.Day.Date.ToLongDateString()).Value;
            if (s == Physio.Text)
            {
                e.Cell.BackColor = Physio.BackColor;
            }
            else if (s == Dental.Text)
            {
                e.Cell.BackColor = Dental.BackColor;
            }
            else
            {
                e.Cell.BackColor = Psycho.BackColor;
            }
        }
    }
}