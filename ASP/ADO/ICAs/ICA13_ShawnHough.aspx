<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ICA13_ShawnHough.aspx.cs" Inherits="ICAs_ICA13_ShawnHough"  MasterPageFile="~/MasterPage.master" Theme="ADO"%>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>ica13 - SP's for Customers Summary</h1><br />
    <hr /><br />
    Pick a Customer: <asp:DropDownList ID="_ddlCustomers" SkinID="DDLSkin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="_ddlCustomers_SelectedIndexChanged"></asp:DropDownList>
    <asp:TextBox ID="_cFilter" runat="server"></asp:TextBox>
    <asp:Button ID="_btnFilter" SkinID="ButtSkin" runat="server" Text="Filter" OnClick="_btnFilter_Click" /><br />
    <asp:GridView ID="_gvCustomerSum" SkinID="gvSkin" runat="server" OnRowDataBound="_gvCustomerSum_RowDataBound" Height="193px" Width="377px"></asp:GridView>
</asp:Content>