<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Calendar ID="_cal" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
        <asp:RadioButton ID="_rb" runat="server" GroupName="myGroup" />
        <asp:RadioButton ID="_rb2" runat="server" GroupName="myGroup" />
        <asp:RadioButton ID="_rb3" runat="server" GroupName="myGroup" />
        <br />
        <asp:Button ID="_btnXPagePost" runat="server" Text="Button" PostBackUrl="~/DuckSeason.aspx" /><br />

        <asp:label ID="_status" runat="server" text="Label"></asp:label>
    </div>
    </form>
</body>
</html>

