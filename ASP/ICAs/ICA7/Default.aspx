<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title>ICA 7</title>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <div class="header">
    <h1>CMPE2500 - Assignment 07- Color Generator- Shawn Hough</h1>
    </div><br />

        <div class="content">
            <table id="_tblMain">
                <thead>
                <tr>
                    <th>Red</th><th>Green</th><th>Blue</th><th>Saved Colors</th>
                </tr>
                 </thead>
                <tbody>
                <tr>
                    <td><asp:TextBox id="_txtbxRed" runat="server"></asp:TextBox></td>
                    <td id="_greenRad"><asp:RadioButtonList id="_radLstGreen" runat="server" AutoPostBack="False" RepeatDirection="Horizontal"></asp:RadioButtonList></td>
                    <td>
                        <asp:DropDownList id="_ddlBlue" runat="server">
                            <asp:ListItem Value="0" Selected="True">Nada</asp:ListItem>
                            <asp:ListItem Value="64">Just A bit</asp:ListItem>
                            <asp:ListItem Value="128">About Half</asp:ListItem>
                            <asp:ListItem Value="192">Most of It</asp:ListItem>
                            <asp:ListItem Value="255">Every Bit</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td rowspan="3">
                    <asp:ListBox id="_lstbxSaved" runat="server" AutoPostBack="true" OnSelectedIndexChanged="_lstbxSaved_SelectedIndexChanged"></asp:ListBox></td>
                </tr>
                <tr>
                    <td colspan="3">
                        Name:<asp:TextBox id="_txtbxName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:CheckBox id="_chkbxGrey" runat="server" />Greyscale
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" colspan="3">
                        <asp:Label id="_lblColor" runat="server" Text="[lblColorPreview]" BorderStyle="Inset"></asp:Label>
                    </td>
                    <td rowspan="2">
                        <asp:Button id="_btnPreview" runat="server" Text="Preview Chosen Color" OnClick="_btnPreview_Click" /><br />
                        <asp:Button id="Button1" runat="server" Text="Save Chosen Color" OnClick="_btnSave_Click" />
                    </td>
                </tr>
            </tbody>
            </table>
               <asp:Label id="_status" runat="server" Text="[lblStatusMessage]"></asp:Label>
        </div><br />
        <div class="footer">&copy;2015ShawnH</div>
    </form>
</body>
</html>
