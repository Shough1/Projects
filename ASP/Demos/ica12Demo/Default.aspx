<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="StyleSheet.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<h1>ica12 Demo - ADO</h1>
      <hr />        <div>
        Pick a Title : <asp:DropDownList ID="_ddl" runat="server" OnSelectedIndexChanged="_ddl_SelectedIndexChanged" >
        </asp:DropDownList><br />
        <asp:Label ID="_output" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:DropDownList ID="_ddlItems" runat="server">
    <asp:ListItem Text="slfjlsdfklsjd"></asp:ListItem>
    </asp:DropDownList>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
    </form>
</body>
</html>
