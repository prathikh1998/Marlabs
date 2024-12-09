<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommandBuilderDemo.aspx.cs" Inherits="AUGNET_DEMO.CommandBuilderDemo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="border: 2px solid blue">
        <tr>
            <td>ID: </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Load" OnClick="LoadFnc" /></td>
        </tr>
        <tr>
            <td>Username: </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email: </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>CreatedAt: </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
           <td><asp:Button ID="Button2" runat="server" Text="Update" OnClick="UpdateFnc" /></td>
            <td> <asp:Button ID="Button3" runat="server" Text="Insert" OnClick="InsertFnc" />
                <asp:Button ID="Button4" runat="server" Text="Delete" OnClick="DeleteFnc" /></td>
          <td> <asp:Label ID="Label1" BorderStyle="Solid" runat="server" Text="Label" ForeColor="Green"></asp:Label></td><br />
          <td> <asp:Label ID="Label2" BorderStyle="Solid" runat="server" Text="Label" ForeColor="Green"></asp:Label></td>
          <td> <asp:Label ID="Label3" BorderStyle="Solid" runat="server" Text="Label" ForeColor="Green"></asp:Label></td>
          <td> <asp:Label ID="Label4" BorderStyle="Solid" runat="server" Text="Label" ForeColor="Green"></asp:Label></td>
</tr>
                
    </table>
</asp:Content>
