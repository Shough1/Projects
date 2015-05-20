<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ICA12_ShawnHough.aspx.cs" Inherits="ICAs_ICA12" Theme="ADO" %>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>ICA12 - ADOPartI - Basic Queries</h1>
    Pick a Supplier: <asp:DropDownList ID="_ddlCompanyName" runat="server" SkinID="DDLSkin" OnSelectedIndexChanged="_ddlCompanyName_SelectedIndexChanged" Width="250"></asp:DropDownList>
    <asp:TextBox ID="_txbxFilter" runat="server"></asp:TextBox>
    <asp:Button ID="_btnFilter" runat="server" Text="Filter" SkinID="ButtSkin" OnClick="_btnFilter_Click" />
    <br />
    <br />
    <asp:PlaceHolder ID="_phCompanyName" runat="server"></asp:PlaceHolder>
    <br />
    <asp:Table ID="_tblProduct" runat="server" SkinId="tblSkin" Enabled="True"></asp:Table>
    

</asp:Content>