<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ica10_ShawnHough.aspx.cs" Inherits="ica10_ShawnHough" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ica10</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>ica10- Shawn Hough - Gridz</h1>
        <hr />
        <asp:Button ID="_btnShowProd" runat="server" Text="Show Products" OnClick="_btnShowProd_Click" /><asp:Button ID="_btnEmpEdit" runat="server" Text="Edit Employees" OnClick="_btnEmpEdit_Click" />
        <asp:SqlDataSource ID="_sqlData" runat="server" ConnectionString="<%$ ConnectionStrings:sHoughNorthwind %>" SelectCommand="SELECT [ProductID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [CategoryName], [ProductName] FROM [Alphabetical list of products] ORDER BY [ProductName]"></asp:SqlDataSource>
        <asp:GridView ID="_gvProducts" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ProductID" DataSourceID="_sqlData" ForeColor="Black" GridLines="Vertical" Height="337px" Width="986px" OnRowDataBound="_gvProducts_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="ProductID" ReadOnly="True" SortExpression="ProductID" Visible="False" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="QuantityPerUnit" HeaderText="Qty Per Unit" SortExpression="QuantityPerUnit">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price"  DataFormatString="{0:c}" SortExpression="UnitPrice">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitsInStock" HeaderText="Units In Stock" SortExpression="UnitsInStock">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitsOnOrder" HeaderText="Units On Order" SortExpression="UnitsOnOrder">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" SortExpression="ReorderLevel">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CheckBoxField>
                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" SortExpression="CategoryName">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="True" />
                </asp:BoundField>
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
        </asp:GridView>
        <asp:SqlDataSource ID="_sqlData2" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:sHoughNorthwind %>" DeleteCommand="DELETE FROM [Employees] WHERE [EmployeeID] = @original_EmployeeID AND [LastName] = @original_LastName AND [FirstName] = @original_FirstName AND (([Title] = @original_Title) OR ([Title] IS NULL AND @original_Title IS NULL)) AND (([HireDate] = @original_HireDate) OR ([HireDate] IS NULL AND @original_HireDate IS NULL)) AND (([Address] = @original_Address) OR ([Address] IS NULL AND @original_Address IS NULL)) AND (([City] = @original_City) OR ([City] IS NULL AND @original_City IS NULL)) AND (([Country] = @original_Country) OR ([Country] IS NULL AND @original_Country IS NULL))" InsertCommand="INSERT INTO [Employees] ([LastName], [FirstName], [Title], [HireDate], [Address], [City], [Country]) VALUES (@LastName, @FirstName, @Title, @HireDate, @Address, @City, @Country)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [EmployeeID], [LastName], [FirstName], [Title], [HireDate], [Address], [City], [Country] FROM [Employees] ORDER BY [EmployeeID]" UpdateCommand="UPDATE [Employees] SET [LastName] = @LastName, [FirstName] = @FirstName, [Title] = @Title, [HireDate] = @HireDate, [Address] = @Address, [City] = @City, [Country] = @Country WHERE [EmployeeID] = @original_EmployeeID AND [LastName] = @original_LastName AND [FirstName] = @original_FirstName AND (([Title] = @original_Title) OR ([Title] IS NULL AND @original_Title IS NULL)) AND (([HireDate] = @original_HireDate) OR ([HireDate] IS NULL AND @original_HireDate IS NULL)) AND (([Address] = @original_Address) OR ([Address] IS NULL AND @original_Address IS NULL)) AND (([City] = @original_City) OR ([City] IS NULL AND @original_City IS NULL)) AND (([Country] = @original_Country) OR ([Country] IS NULL AND @original_Country IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_EmployeeID" Type="Int32" />
                <asp:Parameter Name="original_LastName" Type="String" />
                <asp:Parameter Name="original_FirstName" Type="String" />
                <asp:Parameter Name="original_Title" Type="String" />
                <asp:Parameter Name="original_HireDate" Type="DateTime" />
                <asp:Parameter Name="original_Address" Type="String" />
                <asp:Parameter Name="original_City" Type="String" />
                <asp:Parameter Name="original_Country" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="HireDate" Type="DateTime" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="Country" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="HireDate" Type="DateTime" />
                <asp:Parameter Name="Address" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="Country" Type="String" />
                <asp:Parameter Name="original_EmployeeID" Type="Int32" />
                <asp:Parameter Name="original_LastName" Type="String" />
                <asp:Parameter Name="original_FirstName" Type="String" />
                <asp:Parameter Name="original_Title" Type="String" />
                <asp:Parameter Name="original_HireDate" Type="DateTime" />
                <asp:Parameter Name="original_Address" Type="String" />
                <asp:Parameter Name="original_City" Type="String" />
                <asp:Parameter Name="original_Country" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:GridView ID="_gvEmployee" runat="server" DataSourceID="_sqlData2" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="EmployeeID" ForeColor="#333333" GridLines="None" Height="152px" Width="1116px" OnRowDataBound="_gvEmployee_RowDataBound" OnSelectedIndexChanged="_gvEmployee_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" InsertVisible="False" ReadOnly="True" SortExpression="EmployeeID" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="HireDate" HeaderText="Hire Date" SortExpression="HireDate" DataFormatString="{0:ddMMMyyyy}" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="Red" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />

        </asp:GridView>
    </div>
    </form>
</body>
</html>
