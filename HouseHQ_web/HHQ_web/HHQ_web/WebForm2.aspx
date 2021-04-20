<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="HHQ_web.WebForm2" %>

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
        <asp:TreeView ID="TreeView1" runat="server">
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
        </select></form>
</body>
</html>
