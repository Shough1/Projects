<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Theme="MyCustom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="_cph" Runat="Server">
  Normal Content...<br />
  Normal Content...<br />
  Normal Content...<br />
  Normal Content...<br />
  <asp:Calendar ID="Calendar1" runat="server" SkinID="km"></asp:Calendar>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="_news" Runat="Server">
  This just in... Edmonton can't build anything 
</asp:Content>

