<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ica09_ShawnHough.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title>ICA 9- Shawn Hough</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:MultiView ID="_mv" runat="server">
            <asp:View runat="server">
                <table>
                    <tr><td>User Name:</td><td><asp:TextBox ID="_userName" runat="server"></asp:TextBox></td><td>Password:</td><td><asp:TextBox ID="_pswrd" runat="server"></asp:TextBox></td><td><asp:Button ID="_btnNext" runat="server" Text="Next" Width="64px" OnCommand="ViewControl" /></td></tr>
             <tr><td colspan="5"><asp:Label ID="_Status" runat="server" BorderStyle="Inset" Width="671px"></asp:Label></td></tr>
            </table>
            </asp:View>
            <asp:View runat="server">
                <table>
                <tr><td><asp:Label ID="_lblIntro" runat="server"></asp:Label></td>
                   <td><asp:FileUpload ID="_fu" runat="server" /></td></tr>
                <tr><td colspan="4"><asp:Button ID="_btnNext2" runat="server" Text="Next" OnCommand="FileUpload" /></td></tr>
                </table>
            </asp:View>
            <asp:View runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="_lblMStat" runat="server" BorderStyle="Inset" Width="697px"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="_backBtn" runat="server" Text="Go Back" OnCommand="ViewControl" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <div>
        <asp:HiddenField ID="_storage" runat="server" />
        <asp:HiddenField ID="_fileInfo" runat="server" />
        <asp:Button ID="_btnGTAlb" runat="server" Text="Go To Album" PostBackUrl="~/Album.aspx" />
     </div>
    </form>
</body>
</html>
