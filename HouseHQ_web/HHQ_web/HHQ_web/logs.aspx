<%@ Page Title="" Language="C#" MasterPageFile="~/mas.Master" AutoEventWireup="true" CodeBehind="logs.aspx.cs" Inherits="HHQ_web.logs1" %>
<%@ MasterType VirtualPath="~/mas.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Button runat="server" class="mybtn" ID="Button2" Text="APPS" OnClick="btnApps" />
    <asp:Button runat="server" class="mybtn" ID="Button3" Text="Manager" OnClick="btnMangar" ForeColor="#f1f1f1" />
    <asp:Button runat="server" class="mybtn" ID="ContactUs" Text="Contact Us" OnClick="btnContact" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br>
    <br>
    <asp:Button ID="back" runat="server" Text="Back" class="mybtn1" OnClick="backPage"/>
    <br>
    <br>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <br>
    <br>
</asp:Content>
