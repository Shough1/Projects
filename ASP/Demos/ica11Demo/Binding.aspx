<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Binding.aspx.cs" Inherits="Binding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="_cph" Runat="Server">
  
  <hr />
  <asp:SqlDataSource ID="_sdsPublishers" runat="server" ConnectionString="<%$ ConnectionStrings:csPubs %>" SelectCommand="SELECT [pub_id], [pub_name] FROM [publishers] ORDER BY [pub_name]"></asp:SqlDataSource>
  Publishers : <asp:DropDownList ID="_ddlPublishers" runat="server" AutoPostBack="True" DataSourceID="_sdsPublishers" DataTextField="pub_name" DataValueField="pub_id" OnSelectedIndexChanged="_ddlPublishers_SelectedIndexChanged"></asp:DropDownList>
  <hr />
  <asp:SqlDataSource ID="_sdsTitles" runat="server" ConnectionString="<%$ ConnectionStrings:csPubs %>" DeleteCommand="DELETE FROM [titles] WHERE [title_id] = @title_id" InsertCommand="INSERT INTO [titles] ([title_id], [title], [type], [pubdate], [pub_id]) VALUES (@title_id, @title, @type, @pubdate, @pub_id)" SelectCommand="SELECT [title_id], [title], [type], [pubdate], [pub_id] FROM [titles] WHERE ([pub_id] = @pub_id) ORDER BY [title]" UpdateCommand="UPDATE [titles] SET [title] = @title, [type] = @type, [pubdate] = @pubdate, [pub_id] = @pub_id WHERE [title_id] = @title_id">
    <DeleteParameters>
      <asp:Parameter Name="title_id" Type="String" />
    </DeleteParameters>
    <InsertParameters>
      <asp:Parameter Name="title_id" Type="String" />
      <asp:Parameter Name="title" Type="String" />
      <asp:Parameter Name="type" Type="String" />
      <asp:Parameter Name="pubdate" Type="DateTime" />
      <asp:Parameter Name="pub_id" Type="String" />
    </InsertParameters>
    <SelectParameters>
      <asp:ControlParameter ControlID="_ddlPublishers" Name="pub_id" PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
    <UpdateParameters>
      <asp:Parameter Name="title" Type="String" />
      <asp:Parameter Name="type" Type="String" />
      <asp:Parameter Name="pubdate" Type="DateTime" />
      <asp:Parameter Name="pub_id" Type="String" />
      <asp:Parameter Name="title_id" Type="String" />
    </UpdateParameters>
  </asp:SqlDataSource>
  <asp:ListView ID="_lvTitles" runat="server" DataKeyNames="title_id" DataSourceID="_sdsTitles" InsertItemPosition="LastItem">
    <AlternatingItemTemplate>
      <tr style="background-color:#FFF8DC;">
        <td>
          <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
          <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
        </td>
        <td>
          <asp:Label ID="title_idLabel" runat="server" Text='<%# Eval("title_id") %>' />
        </td>
        <td>
          <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' />
        </td>
        <td>
          <asp:Label ID="typeLabel" runat="server" Text='<%# Eval("type") %>' />
        </td>
        <td>
          <asp:Label ID="pubdateLabel" runat="server" Text='<%# Eval("pubdate") %>' />
        </td>
        <td>
          <asp:Label ID="pub_idLabel" runat="server" Text='<%# Eval("pub_id") %>' />
        </td>
      </tr>
    </AlternatingItemTemplate>
    <EditItemTemplate>
      <tr style="background-color:#008A8C;color: #FFFFFF;">
        <td>
          <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
          <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
        </td>
        <td>
          <asp:Label ID="title_idLabel1" runat="server" Text='<%# Eval("title_id") %>' />
        </td>
        <td>
          <asp:TextBox ID="titleTextBox" runat="server" Text='<%# Bind("title") %>' />
        </td>
        <td>
          <asp:TextBox ID="typeTextBox" runat="server" Text='<%# Bind("type") %>' />
        </td>
        <td>
          <asp:TextBox ID="pubdateTextBox" runat="server" Text='<%# Bind("pubdate") %>' />
        </td>
        <td>
          <%--Comment out old text box bindings--%>
          <%--<asp:TextBox ID="pub_idTextBox" runat="server" Text='<%# Bind("pub_id") %>' />--%>
          <asp:DropDownList ID="_ddPubSelect" runat="server" DataSourceID="_sdsPublishers" 
            SelectedValue='<%# Bind("pub_id") %>'
            DataTextField="pub_name" DataValueField="pub_id"></asp:DropDownList>
        </td>
      </tr>
    </EditItemTemplate>
    <EmptyDataTemplate>
      <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
        <tr>
          <td>No data was returned - Man !.</td>
        </tr>
      </table>
    </EmptyDataTemplate>
    <InsertItemTemplate>
      <tr style="">
        <td>
          <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
          <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
        </td>
        <td>
          <asp:TextBox ID="title_idTextBox" runat="server" Text='<%# Bind("title_id") %>' />
        </td>
        <td>
          <asp:TextBox ID="titleTextBox" runat="server" Text='<%# Bind("title") %>' />
        </td>
        <td>
          <asp:TextBox ID="typeTextBox" runat="server" Text='<%# Bind("type") %>' />
        </td>
        <td>
          <asp:TextBox ID="pubdateTextBox" runat="server" Text='<%# Bind("pubdate") %>' />
        </td>
        <td>
          <asp:TextBox ID="pub_idTextBox" runat="server" Text='<%# Bind("pub_id") %>' />
        </td>
      </tr>
    </InsertItemTemplate>
    <ItemTemplate>
      <tr style="background-color:#DCDCDC;color: #000000;">
        <td>
          <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
          <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
          <asp:Button ID="SelectButton" runat="server" CommandName="Select" Text="Select Me" />
        </td>
        <td>
          <asp:Label ID="title_idLabel" runat="server" Text='<%# Eval("title_id") %>' />
        </td>
        <td>
          <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' />
        </td>
        <td>
          <asp:Label ID="typeLabel" runat="server" Text='<%# Eval("type") %>' />
        </td>
        <td>
          <asp:Label ID="pubdateLabel" runat="server" Text='<%# Eval("pubdate") %>' />
        </td>
        <td>
          <asp:Label ID="pub_idLabel" runat="server" Text='<%# Eval("pub_id") %>' />
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
                <th runat="server">title_id</th>
                <th runat="server">title</th>
                <th runat="server">type</th>
                <th runat="server">pubdate</th>
                <th runat="server">pub_id</th>
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
          <asp:Label ID="title_idLabel" runat="server" Text='<%# Eval("title_id") %>' />
        </td>
        <td>
          <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' />
        </td>
        <td>
          <asp:Label ID="typeLabel" runat="server" Text='<%# Eval("type") %>' />
        </td>
        <td>
          <asp:Label ID="pubdateLabel" runat="server" Text='<%# Eval("pubdate") %>' />
        </td>
        <td>
          <asp:Label ID="pub_idLabel" runat="server" Text='<%# Eval("pub_id") %>' />
        </td>
      </tr>
    </SelectedItemTemplate>
  </asp:ListView>
  <hr />
  <asp:SqlDataSource ID="_sdsJoinSample" runat="server" ConnectionString="<%$ ConnectionStrings:csPubs %>" SelectCommand="SELECT titleauthor.title_id, authors.au_lname, authors.au_fname FROM titleauthor INNER JOIN authors ON titleauthor.au_id = authors.au_id WHERE (titleauthor.title_id = @title_id)">
    <SelectParameters>
      <asp:ControlParameter ControlID="_lvTitles" Name="title_id" PropertyName="SelectedValue" />
    </SelectParameters>
  </asp:SqlDataSource>
  <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AllowPaging="True" DataSourceID="_sdsJoinSample"></asp:DetailsView>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="_news" Runat="Server">
</asp:Content>

