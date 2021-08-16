<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="HHQ_web.WebForm2" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
                <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
                <asp:MenuItem Text="New Item" Value="New Item"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:TreeView ID="TreeView1" runat="server" style="margin-bottom: 51px">
            <Nodes>
                <asp:TreeNode Text="New Node" Value="New Node">
                    <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="New Node" Value="New Node">
                    <asp:TreeNode Text="New Node" Value="New Node">
                        <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="New Node" Value="New Node"></asp:TreeNode>
            </Nodes>
        </asp:TreeView>
        <select id="Select1" name="D1">
            <option></option>
        </select><input id="Checkbox1" type="checkbox" /><input id="Checkbox2" type="checkbox" /><input id="Checkbox3" type="checkbox" /><input id="Checkbox4" type="checkbox" /><input id="Checkbox5" type="checkbox" /><input id="Submit1" type="submit" value="submit" /><select id="Select2" name="D2">
            <option></option>
        </select></form>
</body>
</html>
