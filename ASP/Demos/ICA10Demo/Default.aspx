<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ICA10Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>ICA 10 Demo</h1>
        <asp:SqlDataSource ID="_sqlData" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:csNorthwind %>" DeleteCommand="DELETE FROM [Suppliers] WHERE [SupplierID] = @original_SupplierID AND [CompanyName] = @original_CompanyName AND (([ContactName] = @original_ContactName) OR ([ContactName] IS NULL AND @original_ContactName IS NULL)) AND (([ContactTitle] = @original_ContactTitle) OR ([ContactTitle] IS NULL AND @original_ContactTitle IS NULL)) AND (([Address] = @original_Address) OR ([Address] IS NULL AND @original_Address IS NULL)) AND (([City] = @original_City) OR ([City] IS NULL AND @original_City IS NULL)) AND (([Region] = @original_Region) OR ([Region] IS NULL AND @original_Region IS NULL)) AND (([PostalCode] = @original_PostalCode) OR ([PostalCode] IS NULL AND @original_PostalCode IS NULL))" InsertCommand="INSERT INTO [Suppliers] ([CompanyName], [ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode]) VALUES (@CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode)" OldValuesParameterFormatString="original_{0}" OnSelecting="_sqlData_Selecting" SelectCommand="SELECT [SupplierID], [CompanyName], [ContactName], [ContactTitle], [Address], [City], [Region], [PostalCode] FROM [Suppliers]" UpdateCommand="UPDATE [Suppliers] SET [CompanyName] = @CompanyName, [ContactName] = @ContactName, [ContactTitle] = @ContactTitle, [Address] = @Address, [City] = @City, [Region] = @Region, [PostalCode] = @PostalCode WHERE [SupplierID] = @original_SupplierID AND [CompanyName] = @original_CompanyName AND (([ContactName] = @original_ContactName) OR ([ContactName] IS NULL AND @original_ContactName IS NULL)) AND (([ContactTitle] = @original_ContactTitle) OR ([ContactTitle] IS NULL AND @original_ContactTitle IS NULL)) AND (([Address] = @original_Address) OR ([Address] IS NULL AND @original_Address IS NULL)) AND (([City] = @original_City) OR ([City] IS NULL AND @original_City IS NULL)) AND (([Region] = @original_Region) OR ([Region] IS NULL AND @original_Region IS NULL)) AND (([PostalCode] = @original_PostalCode) OR ([PostalCode] IS NULL AND @original_PostalCode IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_SupplierID" Type="Int32" />
                <asp:Parameter Name="original_CompanyName" Type="String" />
                <asp:Parameter Name="original_ContactName" Type="String" />
                <asp:Parameter Name="original_ContactTitle" Type="String" />
                <asp:Parameter Name="original_Address" Type="String" />
                <asp:Parameter Name="original_City" Type="String" />
                <asp:Parameter Name="original_Region" Type="String" />
                <asp:Parameter Name="original_PostalCode" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CompanyName" Type="String" />
                <asp:Parameter Name="ContactName" Type="String" />
                <asp:Parameter Name="ContactTitle" Type="String" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="Region" Type="String" />
                <asp:Parameter Name="PostalCode" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="CompanyName" Type="String" />
                <asp:Parameter Name="ContactName" Type="String" />
                <asp:Parameter Name="ContactTitle" Type="String" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="Region" Type="String" />
                <asp:Parameter Name="PostalCode" Type="String" />
                <asp:Parameter Name="original_SupplierID" Type="Int32" />
                <asp:Parameter Name="original_CompanyName" Type="String" />
                <asp:Parameter Name="original_ContactName" Type="String" />
                <asp:Parameter Name="original_ContactTitle" Type="String" />
                <asp:Parameter Name="original_Address" Type="String" />
                <asp:Parameter Name="original_City" Type="String" />
                <asp:Parameter Name="original_Region" Type="String" />
                <asp:Parameter Name="original_PostalCode" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="_gv" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataKeyNames="SupplierID" DataSourceID="_sqlData" Height="414px" Width="1611px" OnRowDataBound="_gv_RowDataBound">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:CommandField>
                <asp:BoundField DataField="SupplierID" HeaderText="SupplierID" InsertVisible="False" ReadOnly="True" SortExpression="SupplierID" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ContactTitle" HeaderText="ContactTitle" SortExpression="ContactTitle" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Region" HeaderText="Region" SortExpression="Region" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="PostalCode" HeaderText="PostalCode" SortExpression="PostalCode" HeaderStyle-BackColor="WhiteSmoke" >
<HeaderStyle BackColor="WhiteSmoke"></HeaderStyle>
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                No Data Available
            </EmptyDataTemplate>

            <SelectedRowStyle BackColor="Red" />

        </asp:GridView>
    </div>
    </form>
</body>
</html>
