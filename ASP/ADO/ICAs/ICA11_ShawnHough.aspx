<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ICA11_ShawnHough.aspx.cs" Inherits="ICAs_ICA11" Theme="ADO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>ica 11 - More Data - aware Controls</h1>
    <hr />
    <asp:SqlDataSource ID="_sdsCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:shough1_NorthwindConnectionString %>" SelectCommand="SELECT [CustomerID], [CompanyName], [ContactName], [ContactTitle] FROM [Customers] ORDER BY [CompanyName]">
</asp:SqlDataSource>
    Customer: <asp:DropDownList ID="_ddlCustomers" runat="server" DataSourceID="_sdsCustomer" DataTextField="CompanyName" DataValueField="CustomerID" AutoPostBack="True" OnSelectedIndexChanged="_ddlCustomers_SelectedIndexChanged"></asp:DropDownList>
    <asp:SqlDataSource ID="_sdsOrders" runat="server" ConnectionString="<%$ ConnectionStrings:shough1_NorthwindConnectionString %>" DeleteCommand="DELETE FROM [Orders] WHERE [OrderID] = @OrderID" InsertCommand="INSERT INTO [Orders] ([CustomerID], [OrderDate]) VALUES (@CustomerID, @OrderDate)" SelectCommand="SELECT [OrderID], [CustomerID], [OrderDate] FROM [Orders] WHERE ([CustomerID] = @CustomerID)" UpdateCommand="UPDATE [Orders] SET [CustomerID] = @CustomerID, [OrderDate] = @OrderDate WHERE [OrderID] = @OrderID">
        <DeleteParameters>
            <asp:Parameter Name="original_OrderID" Type="Int32" />
            <asp:Parameter Name="original_CustomerID" Type="String" />
            <asp:Parameter Name="original_OrderDate" Type="DateTime" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CustomerID" Type="String" />
            <asp:Parameter Name="OrderDate" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="_ddlCustomers" Name="CustomerID" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CustomerID" Type="String" />
            <asp:Parameter Name="OrderDate" Type="DateTime" />
            <asp:Parameter Name="original_OrderID" Type="Int32" />
            <asp:Parameter Name="original_CustomerID" Type="String" />
            <asp:Parameter Name="original_OrderDate" Type="DateTime" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="_sdsOrders2" runat="server" ConnectionString="<%$ ConnectionStrings:shough1_NorthwindConnectionString %>" SelectCommand="Select Orders.OrderID, Categories.CategoryID, Categories.CategoryName, SUM(OrderDetails.UnitPrice*OrderDetails.Quantity) as 'Cat Sum'
From Orders
Inner Join OrderDetails
ON Orders.OrderID = OrderDetails.OrderID
Inner Join Products
ON OrderDetails.ProductID = Products.ProductID
Inner Join Categories
ON Products.CategoryID = Categories.CategoryID
Where Orders.OrderID =@PARAMETER
Group By Orders.OrderID, Categories.CategoryID, Categories.CategoryName">
        <SelectParameters>
            <asp:ControlParameter ControlID="_lvOrders" Name="PARAMETER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="_sdsCustomerSelected" runat="server" ConnectionString="<%$ ConnectionStrings:shough1_NorthwindConnectionString %>" SelectCommand="SELECT [CompanyName], [CustomerID] FROM [Customers]"></asp:SqlDataSource>
    <asp:ListView ID="_lvOrders" runat="server" DataKeyNames="OrderID" DataSourceID="_sdsOrders" InsertItemPosition="LastItem" OnSelectedIndexChanged="_lvOrders_SelectedIndexChanged" OnItemInserted="_lvOrders_ItemInserted" OnItemDeleted="_lvOrders_ItemDeleted">
        <AlternatingItemTemplate>
            <tr style="background-color:#FFF8DC;">
                <td>
                    <asp:Button SkinId="ButtSkin" ID="SelectButton" runat="server" CommandName="Select" Text="Pick Me" />
                    <asp:Button SkinId="ButtSkin" ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button SkinId="ButtSkin" ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="OrderIDLabel" runat="server" Text='<%# Eval("OrderID") %>' />
                </td>
                <td>
                    <asp:Label ID="CustomerIDLabel" runat="server" Text='<%# Eval("CustomerID") %>' />
                </td>
                <td>
                    <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Eval("OrderDate","{0:dd-MMM-yyyy}") %>'  />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color:#008A8C;color: #FFFFFF;">
                <td>
                    <asp:Button SkinID="ButtSkin" ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button SkinID="ButtSkin" ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="OrderIDLabel1" runat="server" Text='<%# Eval("OrderID") %>' />
                </td>
                <td>
                    <asp:DropDownList SkinID="DDLSkin" SelectedValue='<%# Bind("CustomerID")%>' ID="_ddlCustIDIns" runat="server" DataSourceID="_sdsCustomer" DataTextField="CompanyName" DataValueField="CustomerID" AutoPostBack="true">
                    
                     </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="OrderDateTextBox" runat="server" Text='<%# Bind("OrderDate") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button SkinId="ButtSkin" ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button SkinId="ButtSkin" ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                     <asp:DropDownList SkinID="DDLSkin" SelectedValue='<%# Bind("CustomerID")%>' ID="_ddlCustIDIns" runat="server" DataSourceID="_sdsCustomer" DataTextField="CompanyName" DataValueField="CustomerID" AutoPostBack="true">
                    
                     </asp:DropDownList>
                </td>
                <td>
                    <asp:Calendar SkinID="CalSkin" ID="_calOrderDate" runat="server" SelectedDate='<%# Bind("OrderDate")%>' ></asp:Calendar>
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Button SkinID="ButtSkin" ID="SelectButton" runat="server" CommandName="Select" Text="Pick Me" />
                    <asp:Button SkinID="ButtSkin" ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button SkinID="ButtSkin" ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    
                </td>
                <td>
                    <asp:Label ID="OrderIDLabel" runat="server" Text='<%# Eval("OrderID") %>' />
                </td>
                <td>
                    <asp:Label ID="CustomerIDLabel" runat="server" Text='<%# Eval("CustomerID") %>' />
                </td>
                <td>
                    <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Eval("OrderDate") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                <th runat="server"></th>
                                <th runat="server">OrderID</th>
                                <th runat="server">CustomerID</th>
                                <th runat="server">OrderDate</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="OrderIDLabel" runat="server" Text='<%# Eval("OrderID") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="_ddlSelectedCustomer" runat="server" DataSourceID="_sdsCustomerSelected" DataTextField="CompanyName" DataValueField="CustomerID" AutoPostBack="True" OnSelectedIndexChanged="_ddlCustomers_SelectedIndexChanged" SelectedValue='<%# Bind("CustomerID") %>' SkinID="DDL"></asp:DropDownList>
                   
                </td>
                <td>
                    <asp:Label ID="OrderDateLabel" runat="server" Text='<%# Eval("OrderDate") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>

    <br />
   
    <asp:DetailsView runat="server" Height="47px" Width="288px" DataSourceID="_sdsOrders2" AllowPaging="True" AutoGenerateRows="False" DataKeyNames="OrderID,CategoryID" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <EmptyDataTemplate>
            Drat! Nothing To Show.
        </EmptyDataTemplate>
        
        <Fields>
            <asp:BoundField DataField="OrderID" HeaderText="Order ID" InsertVisible="False" ReadOnly="True" SortExpression="OrderID">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="CategoryID" HeaderText="Category ID" InsertVisible="False" ReadOnly="True" SortExpression="CategoryID">
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" SortExpression="CategoryName">
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="Cat Sum" DataFormatString="{0:C}" HeaderText="Category Sum" ReadOnly="True" SortExpression="Cat Sum">
            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </asp:BoundField>

        </Fields>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <HeaderTemplate>
            Summary Category Details View
        </HeaderTemplate>
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
    </asp:DetailsView>
    
</asp:Content>

