<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="HHQ_web.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            background-color: #1e1e1e;
            font-family: 'Noto Sans', sans-serif;
        }
        .auto-style1 {

            background-color: #1e1e1e;
            font-family: 'Noto Sans', sans-serif;
        }
        .auto-style2 {
            background-color: rgb(24, 30, 54);
            height: 900px;
            width: 200px;
        }
        .auto-style3 {
            background-color: rgb(46, 51, 73);
            width: auto;
            margin-left: 0px;
        }
        .auto-style5 {

            background-color: rgb(24, 30, 54);
        }
        .auto-style6 {
            margin-left: 57px;
            margin-top: 28px;
        }
        .auto-style7 {
            margin-left: 57px;
        }
        .auto-style8 {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1" id ="div1">
            <div class="auto-style3" id ="div2">
                <div class="auto-style2" id ="div3">
                    
                    <asp:Panel ID="Panel1" runat="server" CssClass="auto-style5" Height="200px">
                        <asp:Image ID="Image1" runat="server"  Height="69px" ImageUrl="~\img\Untitled-11.png" Width="72px" CssClass="auto-style6" />
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Microsoft Sans Serif" Font-Size="9.75pt" ForeColor="#007EF9" Height="16px" Text="User Name" Width="86px" CssClass="auto-style7"></asp:Label>
                    </asp:Panel>
                    <asp:Button ID="Button1" runat="server" Text="Apps"  BackColor="#181E36" BorderStyle="None" Font-Bold="True" Font-Names="Microsoft Sans Serif" Font-Size="9.75pt" ForeColor="#007EF9" Height="42px" Width="200px" CssClass="auto-style8" OnClick="Button1_Click"/>
                    
            </div>

                </div>
            
            
        </div>
    </form>
</body>
</html>
