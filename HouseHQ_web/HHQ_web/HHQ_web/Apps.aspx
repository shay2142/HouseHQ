<%@ Page Title="" Language="C#" MasterPageFile="~/mas.Master" AutoEventWireup="true" CodeBehind="Apps.aspx.cs" Inherits="HHQ_web.WebForm6" %>
<%@ MasterType VirtualPath="~/mas.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Button runat="server" class="mybtn" ID="Button2" Text="APPS" OnClick="btnApps" ForeColor ="#f1f1f1"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br>
    <br>
    <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    <br>
    <br>
    <div CssClass="mybtn2">
        <asp:ImageButton ID="editApps" runat="server" ImageUrl="~\img\edit.png" AlternateText="edit apps" CssClass="mybtn2" OnClick="edit_Click"/>
        <label CssClass="mybtn2" style="font-family: 'nirmala UI'" >Edit</label>
    </div>
    <br>
    <br>
    <h2><%=title%></h2>
    <asp:Panel ID="Panel2" runat="server">
    </asp:Panel>
    <div id="div" runat="server">
        <br>
    </div>
</asp:Content>
