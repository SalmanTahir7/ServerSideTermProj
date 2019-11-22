<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebAPIRegistration.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblName" runat="server"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="Customer" Text="Customer"></asp:ListItem>
                <asp:ListItem Value="Restaurant" Text="Restaurant"></asp:ListItem>
            </asp:DropDownList>
            <br />
        </div>
        <br />
        <asp:Button ID="btnCreate" runat="server" Text="Create Account" OnClick="btnCreate_Click" />
        <br />
        <br />
        <asp:Label ID="lblCreated" runat="server"></asp:Label>
    </form>
</body>
</html>
