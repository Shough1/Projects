<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title>Demo 6</title>
</head>
<body>
    <form method="post" id="form1" runat="server">
    <div>
        Hi how are you? <br />
        <asp:Label ID="Label1" runat="server" Text="My Label"></asp:Label><br />
        Username:<asp:TextBox ID="_tbName" runat="server" placeholder="Username"></asp:TextBox><br />
        <asp:Button ID="_btnPost" runat="server" Text="Submit to Server" OnClick="_btnPost_Click" ForeColor="#000066" />
        <hr />
        <asp:DropDownList ID="_ddl" runat="server" AutoPostBack="True" OnSelectedIndexChanged="_ddl_SelectedIndexChanged">
            <asp:ListItem Value="200">Shawn</asp:ListItem>
            <asp:ListItem Value="100">Nader</asp:ListItem>
            <asp:ListItem Value="Glasses">Josh</asp:ListItem>
        </asp:DropDownList>
        <hr />
        Status:<asp:Label ID="_status" runat="server" Text=" "></asp:Label>
    </div>
    </form>
</body>
</html>
 

