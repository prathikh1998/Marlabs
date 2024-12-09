<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SqlAdapterDemo.aspx.cs" Inherits="AUGNET_DEMO.SqlAdapterDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Employee Search</h2>
    
    <asp:TextBox ID="TextBox1" runat="server" Placeholder="Enter Employee ID"></asp:TextBox>
    <asp:Button ID="Button1" Text="Search Employee" OnClick="ButtonClick" runat="server" />
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" EmptyDataText="No employees found.">
    </asp:GridView>
</asp:Content>
