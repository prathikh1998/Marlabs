<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CacheDBApp.aspx.cs" Inherits="AUGNET_DEMO.CacheDBApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style>
     /* GridView Styling */
     .gridview-style {
         width: 100%;
         border-collapse: collapse;
         margin: 20px 0;
         font-family: Arial, sans-serif;
         color: #333;
     }

     .gridview-style th, .gridview-style td {
         padding: 8px;
         text-align: left;
         border: 1px solid #ddd;
     }

     .gridview-style th {
         background-color: #f4f4f4;
         font-weight: bold;
     }

     .gridview-style tr:nth-child(even) {
         background-color: #f9f9f9;
     }

     .gridview-style tr:hover {
         background-color: #f1f1f1;
     }

     /* Styling for TextBox in Edit Mode */
     .edit-textbox {
         width: 100%;
         padding: 8px;
         border: 1px solid #ccc;
         border-radius: 4px;
     }

     .edit-textbox:focus {
         border-color: #007BFF;
         outline: none;
     }

     /* Styling for Edit, Delete, and Insert buttons */
     .gridview-style .edit-button, .gridview-style .delete-button, .gridview-style .insert-button {
         background-color: #007BFF;
         color: white;
         padding: 5px 10px;
         border: none;
         cursor: pointer;
         border-radius: 4px;
         text-decoration: none;
     }

     .gridview-style .edit-button:hover, .gridview-style .delete-button:hover, .gridview-style .insert-button:hover {
         background-color: #0056b3;
     }

     .gridview-style .insert-button {
         background-color: #28a745;
     }

     .gridview-style .insert-button:hover {
         background-color: #218838;
     }
 </style>

 <asp:GridView ID="GridView1" AllowPaging="true" PageSize="4" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_CancelIndex" OnRowUpdating="GridView1_RowUpdating" CssClass="gridview-style" OnRowDeleting="GridView1_RowDeleting">
     <Columns>
         <asp:BoundField ReadOnly="true" DataField="ID" HeaderText="ID" SortExpression="ID"></asp:BoundField>
         <asp:BoundField DataField="Username" HeaderText="User NAME" SortExpression="Username"></asp:BoundField>
         <asp:TemplateField HeaderText="EMAIL" SortExpression="Email">
             <ItemTemplate><%# Eval("Email") %></ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Email") %>' CssClass="edit-textbox"></asp:TextBox>
             </EditItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="CREATED AT" SortExpression="CreatedAt">
             <ItemTemplate><%# Eval("CreatedAt") %></ItemTemplate>
             <EditItemTemplate>
                 <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CreatedAt") %>' CssClass="edit-textbox"></asp:TextBox>
             </EditItemTemplate>
         </asp:TemplateField>
         <asp:CommandField HeaderText="ACTIONS" ShowDeleteButton="true" ShowEditButton="true" ShowInsertButton="true" ShowSelectButton="true" ButtonType="Link" />
     </Columns>
 </asp:GridView>
 <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <hr />
    <asp:Button ID="Button1" runat="server" Text="Updatw DB" OnClick="Updatedb_Click" />
    <asp:Button ID="Button2" runat="server" Text="Getfrom DB" OnClick="Button2_Click" />
</asp:Content>
