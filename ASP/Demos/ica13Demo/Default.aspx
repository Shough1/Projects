<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="StyleSheet.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <h1>ica13Demo Block : </h1>
        Titles : <asp:ListBox ID="_lb" runat="server" onselectedindexchanged="_lb_SelectedIndexChanged"></asp:ListBox>
        <asp:TextBox ID="_tbFilter" runat="server"></asp:TextBox>
        <asp:Button ID="_btnFilter" runat="server" Text="Filter" 
            onclick="_btnFilter_Click" />
        <hr />
        <asp:GridView ID="_gv" runat="server">
        </asp:GridView>
        <hr />
        ica14 - Update Block Example : <br />
        <asp:TextBox ID="_tbFixed" runat="server"></asp:TextBox>
        <asp:Button ID="_btnUpdate" runat="server" Text="Update" 
            onclick="_btnUpdate_Click" /><br />
        <asp:Label ID="_lbStatus" runat="server" Text="??"></asp:Label>
    </div>
    </form>
</body>
</html>
