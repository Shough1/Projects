<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title>ICA 9 Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:MultiView ID="_mv" runat="server">
            <asp:View runat="server">
                View 1 : <br />
                <asp:Button ID="_btnNextView" runat="server" Text="Next View" OnCommand="ViewControl" />
            </asp:View>
             <asp:View runat="server">
                View 2 : <br />
                 <asp:FileUpload ID="_fu" runat="server" />
                 <asp:Button runat="server" Text="Process File" OnCommand="FileUploadProcess" />
                 <br />
                 <asp:Label ID="_fuLabelUpdate" runat="server" Text="Label">

                 </asp:Label>
                 <asp:Button ID="_btnFirstView" runat="server" Text="First View" />
                 <asp:Button ID="_btnNextView2" runat="server" Text="Next View" OnCommand="ViewControl" />
            </asp:View>
            <asp:View runat="server">
                View 2:<br />
                <asp:PlaceHolder ID="_ph" runat="server">

                </asp:PlaceHolder>
            </asp:View>        

        </asp:MultiView>
    </div>
    </form>
</body>
</html>

