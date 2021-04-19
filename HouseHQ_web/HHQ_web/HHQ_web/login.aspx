<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HHQ_web.login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        body {
            background-color: #1e1e1e;
            font-family: 'Noto Sans', sans-serif;
        }

        form {
            background-color: rgb(24, 30, 54);
            width: 325px;
            height: 325px;
            margin: 30px auto 0;
            overflow: auto;
            box-shadow: 0px 0px 8px 1px rgba(0,0,0,.75);
        }

        label {
            position: relitive;
            display: inline-block;
            color: white;
            top: 100px;
            font-size: 1.25em;
            transition: all .2s ease;
            z-index: 10;
            position: relative;
            top: -30px;
        }

        .input-container {
            width: 75%;
            position: relative;
            margin: 45px auto -25px;
            top: 0px;
            left: 0px;
        }

        input[type="text"], input[type="password"] {
            width: 100%;
            position: relative;
            display: inline-block;
            font-size: 1.25em;
            padding: 0 0 5px 0;
            color: white;
            background-color: transparent;
            border-bottom: solid 2px rgb(0, 126, 249);
            outline: none;
            z-index: 20;
            top: 0px;
            left: 0px;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            margin-bottom: 0px;
        }

        input[type="text"]:focus ~ label, input[type="text"]:valid ~ label {
            top: -60px;
        }
        input[type="password"]:focus ~ label, input[type="password"]:valid ~ label {
            top: -60px;
        }

        #Button1 {
            cursor: pointer;
            margin: auto;
            width: 100%;
            background-color: rgb(0, 126, 249);
            color: white;
            font-family: 'Noto Sans', sans-serif;
            font-size: 1.25em;
            padding: 10px 0;
            border: solid #0356a9;
            border-width: 0 0 4px 0;
            border-radius: 3px;
            outline: none;
        }

        #Button1:hover {
            background: rgb(56, 125, 194);
            border: solid #0356a9;
            border-width: 0 0 4px 0;
        }

        #Button1:active {
            background: #0356a9;
            border: solid transparent;
            border-width: 0 0 0px 0;
        }
        .auto-style1 {
            direction: ltr;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="input-container">
            <input required type="text" id="ipServer" runat="server"/>
            <label>IP</label>
        </div>
        <div class="input-container">
            <input required type="text" id="userName" runat="server"/>
            <label>Name</label>
        </div>

        <div class="input-container">
            <input required type="password" id="pass" runat="server"/>
            <label>Password</label>
        </div>
        <div class="input-container">
            <%--<input type="submit" value="Login" />--%>
            <asp:Button id="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
        </div>
    </form>
    <p class="auto-style1">
        &nbsp;</p>
</body>
</html>

