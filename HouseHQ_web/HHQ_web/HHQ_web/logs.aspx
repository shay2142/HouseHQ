﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logs.aspx.cs" Inherits="HHQ_web.logs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        body {
            font-family: "Lato", sans-serif;
            background: rgb(46, 51, 73);
        }

        .sidenav {
            height: 100%;
            width: 0;
            position: fixed;
            z-index: 1;
            top: 0;
            left: 0;
            background-color: rgb(24, 30, 54);
            overflow-x: hidden;
            transition: 0.5s;
            padding-top: 60px;
        }

            .sidenav a {
                padding: 8px 8px 8px 32px;
                text-decoration: none;
                font-size: 25px;
                color: #818181;
                display: block;
                transition: 0.3s;
            }

                .sidenav a:hover {
                    color: #f1f1f1;
                    font-size: 150%;
                }

            .sidenav button, .mybtn {
                padding: 8px 8px 8px 32px;
                text-decoration: none;
                font-size: 25px;
                color: #818181;
                display: block;
                transition: 0.3s;
                background: none;
                border: none;
                block-size: auto;
            }

                .sidenav button:hover, .mybtn:hover {
                    color: #f1f1f1;
                    font-size: 150%;
                }

                .sidenav button:active, .mybtn:active {
                    color: #f1f1f1;
                    font-size: 150%;
                }

             .mybtn1 {
                text-decoration: none;
                font-size: 20px;
                color: #818181;
                transition: 0.3s;
                background: none;
                border: none;
                block-size: auto;
                font-weight: bold;
            }

                .mybtn1:hover {
                    color: #f1f1f1;
                    font-size: 150%;
                }

                .mybtn1:active {
                    color: #f1f1f1;
                    font-size: 150%;
                }

            .sidenav .closebtn {
                position: absolute;
                top: 0;
                right: 25px;
                font-size: 36px;
                margin-left: 50px;
            }

        #main {
            transition: margin-left .5s;
            padding: 16px;
            color: rgb(158, 161, 176);
        }

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
            }

                .sidenav a {
                    font-size: 18px;
                }
        }

        #test {
            text-align: center;
        }

        #userName {
            font-family: "Lato", sans-serif;
            color: rgb(0, 126, 249);
        }

        .logout {
            text-decoration: none;
            color: #818181;
            font-family: "Lato", sans-serif;
            background: none;
            border: none;
            block-size: auto;
        }

            .logout:active, .logout:hover {
                color: #f1f1f1;
                font-size: 150%;
            }

        #test1 {
            font-family: "Lato", sans-serif;
        }

        span:active, span:hover {
            color: #f1f1f1;
            font-size: 150%;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="mySidenav" class="sidenav" runat="server">
            <a href="javascript:void(0)" class="closebtn" onclick="openAndClsoeNav()">&times;</a>
            <div id="test" runat="server">
                <asp:Image ID="Image1" runat="server" ImageUrl="~\img\Untitled-11.png" Width="63px" />
                <br>
                <label runat="server" id="userName">User name</label>
                <br>
                <asp:Button runat="server" CssClass="logout" ID="Button1" Text="logout" OnClick="Button1_Click" />

            </div>
            <br>
            <%--<button href="#" aria-atomic="True" onclick="">About</button>--%>
            <asp:Button runat="server" class="mybtn" ID="Button2" Text="APPS" OnClick="btnApps" />

            <asp:Button runat="server" class="mybtn" ID="Button3" Text="Manager" OnClick="btnMangar" ForeColor="#f1f1f1" />
            <asp:Button runat="server" class="mybtn" ID="ContactUs" Text="Contact Us" OnClick="btnContact" />
            <%--<a href="#">Services</a>
            <a href="#">Clients</a>
            <a href="#">Contact</a>--%>
        </div>

        <div id="main">
            <span style="font-size: 30px; cursor: pointer" onclick="openAndClsoeNav()">&#9776;MANAGER</span>
            <br>
            <br>
            <asp:Button ID="back" runat="server" Text="Back" class="mybtn1" OnClick="backPage"/>
            <br>
            <br>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br>
            <br>
        </div>

        <script>
            function openAndClsoeNav() {
                if (document.getElementById("mySidenav").style.width == "250px") {
                    document.getElementById("mySidenav").style.width = "0";
                    document.getElementById("main").style.marginLeft = "0";
                }
                else {
                    document.getElementById("mySidenav").style.width = "250px";
                    document.getElementById("main").style.marginLeft = "250px";
                }
            }
        </script>
    </form>
</body>
</html>