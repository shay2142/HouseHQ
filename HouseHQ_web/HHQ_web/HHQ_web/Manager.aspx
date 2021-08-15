<%@ Page Title="" Language="C#" MasterPageFile="~/mas.Master" AutoEventWireup="true" CodeBehind="Manager.aspx.cs" Inherits="HHQ_web.Manager1" %>
<%@ MasterType VirtualPath="~/mas.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Button runat="server" class="mybtn" ID="Apps" Text="APPS" OnClick="btnApps1" />
    <asp:Button runat="server" class="mybtn" ID="Mangar" Text="Manager" OnClick="btnMangar1" ForeColor="#f1f1f1" />
    <asp:Button runat="server" class="mybtn" ID="ContactUs" Text="Contact Us" OnClick="btnContact" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br>
    <br>
    <asp:Button ID="usersManager" runat="server" Text="users manager" class="mybtn1" OnClick="usersManager_Click" />
    <br>
    <br>
    <asp:Button ID="remoteAppManagement" runat="server" Text="remote app management" class="mybtn1" OnClick="remoteAppManagement_Click" />
    <br>
    <br>
    <asp:Button ID="levelKeyManagement" runat="server" Text="level key management" class="mybtn1" OnClick="levelKeyManagement_Click" />
    <br>
    <br>
    <asp:Button ID="logs" runat="server" Text="view logs" class="mybtn1" OnClick="viewLogs_Click" />
    <br>
    <br>
</asp:Content>
