<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PlaceHolder ID="_phPictures" runat="server"></asp:PlaceHolder><br />
        <asp:Button ID="_backToLoad" runat="server" Text="Add Again" PostBackUrl="~/ica09_ShawnHough.aspx" />
        <asp:Button ID="_back" runat="server" Text="Back" PostBackUrl="~/ica09_ShawnHough.aspx" Visible="false" />
        <asp:HiddenField ID="_hf" runat="server" />
        <asp:HiddenField ID="_pictureInfo" runat="server" />
    </div>
    </form>
</body>
</html>
