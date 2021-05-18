<%@ Page Title="" Language="C#" MasterPageFile="~/mas.Master" AutoEventWireup="true" CodeBehind="Apps.aspx.cs" Inherits="HHQ_web.WebForm6" %>
<%@ MasterType VirtualPath="~/mas.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Button runat="server" class="mybtn" ID="Button2" Text="APPS" OnClick="btnApps" ForeColor ="#f1f1f1"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
</asp:Content>
