<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="HHQ_web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            background-color: #1e1e1e;
            font-family: 'Noto Sans', sans-serif;
        }
        #form1{
             background-color:rgb(46, 51, 73)
         }
        .auto-style2 {}
        #Image1 {            margin-left: 60px;
            margin-top: 22px;
        }
        .auto-style3 {
            margin-left: 55px;
        }
        .auto-style4 {}
        .auto-style5 {
            direction: ltr;
        }
    </style>
</head>
<body class="auto-style5">
    <form id="form1" runat="server" class="header">
        <div>
            <asp:Panel ID="Panel3" runat="server">
            <asp:Panel ID="Panel1" runat="server" CssClass="auto-style1" Height="900" Width="186px" BackColor="#181E36">
                <asp:Panel ID="Panel2" runat="server" Height="154px" Width="186px" BackColor="#181E36">
                    <asp:Image ID="Image1" runat="server" CssClass="auto-style2" Height="63px" ImageUrl="~\img\Untitled-11.png" Width="63px" />
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style3" Font-Bold="True" Font-Names="Microsoft Sans Serif" Font-Size="9.75pt" ForeColor="#007EF9" Height="16px" Text="User Name" Width="86px"></asp:Label>
                </asp:Panel>
                <asp:Button ID="Button1" runat="server" Text="Apps"  BackColor="#181E36" BorderStyle="None" CssClass="auto-style4" Font-Bold="True" Font-Names="Microsoft Sans Serif" Font-Size="9.75pt" ForeColor="#007EF9" Height="42px" Width="186px" OnClick="Button1_Click"/> 
                
                
                
            </asp:Panel>
            </asp:Panel>
        </div>
    </form>
    
</body>
</html>
