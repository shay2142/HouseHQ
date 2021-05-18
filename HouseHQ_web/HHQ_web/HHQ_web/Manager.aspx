<%@ Page Title="" Language="C#" MasterPageFile="~/mas.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="HHQ_web.Manager1" %>
<%@ MasterType VirtualPath="~/mas.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Button runat="server" class="mybtn" ID="Apps" Text="APPS" OnClick="btnApps1" />
    <asp:Button runat="server" class="mybtn" ID="Mangar" Text="Manager" OnClick="btnMangar1" ForeColor="#f1f1f1" />
    <asp:Button runat="server" class="mybtn" ID="ContactUs" Text="Contact Us" OnClick="btnContact" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br>
    <br>
    <asp:Button ID="createUsers" runat="server" Text="create users" class="mybtn1" OnClick="createUsers_Click" />
    <asp:Button ID="deleteUsers" runat="server" Text="delete users" class="mybtn1" OnClick="deleteUsers_Click" />
    <asp:Button ID="changeUser" runat="server" Text="change user" class="mybtn1" OnClick="changeUser_Click" />
    <asp:Button ID="logs" runat="server" Text="view logs" class="mybtn1" OnClick="viewLogs_Click" />
</asp:Content>
