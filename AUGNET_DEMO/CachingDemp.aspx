<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CachingDemp.aspx.cs" Inherits="AUGNET_DEMO.CachingDemp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Load Cache" OnClick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Text="Clear Cache" OnClick="Button2_Click" />
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
