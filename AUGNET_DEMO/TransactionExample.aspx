<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionExample.aspx.cs" Inherits="AUGNET_DEMO.TransactionExample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>ADO.NET Transaction Management</h2>
        
        <!-- Button to trigger the transaction -->
        <asp:Button ID="btnExecuteTransaction" runat="server" Text="Execute Transaction" OnClick="btnExecuteTransaction_Click" />
        
        <!-- Label to display the result of the transaction -->
        <div class="result">
            <asp:Label ID="lblResult" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>
    </div>
</asp:Content>
