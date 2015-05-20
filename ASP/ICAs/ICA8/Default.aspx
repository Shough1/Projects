<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Therapy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title>ICA 8</title>
   
    <style type="text/css">
        #TblAppt {
            height: 401px;
            width: 550px;
        }
        .auto-style1 {
            width: 191px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
        <h1>ICA08: Shawn's Therapy Calendar</h1>
    </div>
        <hr />
        <asp:AdRotator ID="_adRotator" runat="server" Target="_parent" AdvertisementFile="~/App_Data/Ads.xml" />
        <hr />
        <div class="content">
            <table id="TblAppt">
                <tr>
                    <td>
                        <asp:RadioButton ID="Physio" runat="server" GroupName="MyAppointments" Text="Physio" BackColor="Red"  Font-Bold="True" Font-Size="Large" Checked="true" />
                        <asp:RadioButton ID="Dental" runat="server" GroupName="MyAppointments" Text="Dental" BackColor="Blue" Font-Bold="True" Font-Size="Large" />
                        <asp:RadioButton ID="Psycho" runat="server" GroupName="MyAppointments" Text="Psycho" BackColor="Goldenrod" Font-Bold="True" Font-Size="Large" />
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <asp:Calendar ID="_cal" runat="server" Height="190px" Width="350px" OnSelectionChanged="_cal_SelectionChanged" OnDayRender="_cal_DayRender" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" NextPrevFormat="FullMonth" SelectedDate="03/03/2015 10:55:31">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                            <TodayDayStyle BackColor="#CCCCCC" />
                        </asp:Calendar><br />
                        Click to Select and Deselect a Date
                    </td>
                    <td class="auto-style1">
                        <asp:ListBox ID="_lstbxAppoint" runat="server" Height="147px" Width="156px">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="_status" runat="server" Text="[lblStatus]"></asp:Label>
                        <br />
                        <asp:Button ID="btnToPrint" runat="server" Text="CrossPage Printable Version" PostBackUrl="~/PrintableVersion.aspx" />
                    </td>
                </tr>
            </table>

        </div>
        
    </form>
</body>
</html>
