<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ICA14_ShawnHough.aspx.cs" Inherits="ICAs_ICA14_ShawnHough" Theme="ADO" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <h1>ICA14 - Modify Order Details</h1>
    <hr />
    <h1>Part I- Delete Order Details</h1>
    <hr />
    <asp:SqlDataSource ID="_sdsOrderDetails" runat="server" ConnectionString="<%$ ConnectionStrings:shough1_NorthwindConnectionString %>" SelectCommand="Select OD.OrderID,P.ProductID, P.ProductName, OD.UnitPrice, OD.Quantity, OD.Discount
From [Order Details] OD inner join Products P on OD.ProductID = P.ProductID
WHERE OD.OrderID = @PARAMETER">
        <SelectParameters>
            <asp:ControlParameter ControlID="_txbxOrderID" Name="PARAMETER" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    Enter OrderID:<asp:TextBox ID="_txbxOrderID" runat="server"></asp:TextBox><asp:Button ID="_btnFiler" runat="server" Text="Get Order Details" />
    <hr />
    <asp:GridView DataKeyNames="OrderID,ProductID" SkinID="gvSkin" ID="_gvOrderDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="_sdsOrderDetails" OnSelectedIndexChanged="_gvOrderDetails_SelectedIndexChanged" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="OrderID" HeaderText="OrderID" SortExpression="OrderID" />
            <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False" ReadOnly="True" SortExpression="ProductID" Visible="False" />
            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
            <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
            <asp:BoundField DataField="Discount" HeaderText="Discount" SortExpression="Discount" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView><br />
    <asp:Button ID="_btnDelete" runat="server" Text="Delete Selected" OnClick="_btnDelete_Click" SkinID="ButtSkin" /><br />
    <asp:Label ID="_lblStatus" runat="server" Text="Status: "></asp:Label>
    <br /><br />
    <h1>PartII - Insert Order Details</h1>
    <asp:SqlDataSource ID="_sdsOrderDetails2" runat="server" ConnectionString="<%$ ConnectionStrings:shough1_NorthwindConnectionString %>" SelectCommand="SELECT [ProductID], [ProductName] FROM [Products]">
    </asp:SqlDataSource>
    <hr />
    Enter OrderID:<asp:TextBox ID="_txbxOrderID2" runat="server"></asp:TextBox><br />
    Select Product: <asp:DropDownList ID="_ddlOrderDetails" runat="server" DataSourceID="_sdsOrderDetails2" DataTextField="ProductName" DataValueField="ProductID"></asp:DropDownList><br />
    Enter Quantity: <asp:TextBox ID="_txbxQnty" runat="server"></asp:TextBox><br />
    <asp:Button ID="_btnInsert" runat="server" Text="Insert Record" OnClick="_btnInsert_Click" SkinID="ButtSkin" /><br />
    <asp:Label ID="_lblStatus2" runat="server" Text="Status:"></asp:Label>
</asp:Content>