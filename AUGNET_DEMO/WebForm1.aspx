<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AUGNET_DEMO.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>ADO.NET</h1>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" />
            <asp:Label ID="Label1" runat="server" Text="label"></asp:Label>
        </div>
    </form>
</body>
</html>
