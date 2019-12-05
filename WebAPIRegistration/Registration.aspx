<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebAPIRegistration.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Processor</title>
    <link href="CSS/style.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h2>Payment Processor</h2>
        </header>

        <section>
            <nav>
                Please Input Your Name and then input your information to submit
            </nav>

            <article>
                <div>
                    <div>
                        Please Input Your Name
                        <br />
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        Are You A customer or Restaurant? 
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
                </div>

            </article>
            <article>
                <div ID="Restaurant" runat="server">
                    Restaurant Name: 
                    <br /> 
                    <asp:TextBox ID="txtRName" runat="server"></asp:TextBox>
                    <br />
                    Restaurant Address: 
                    <br />
                    <asp:TextBox ID="txtRAddress" runat="server"></asp:TextBox>
                    <br />
                    Bank Account Number (10 digits):
                    <br />
                    <asp:TextBox ID="txtRBAcctNum" runat="server"></asp:TextBox>
                    <br />
                    Bank Account Type (Checking, Saving, etc..)
                    <br />
                    <asp:TextBox ID="txtRBAcctType" runat="server"></asp:TextBox>
                    <br />
                    Bank Account Initial Balance ($xxx.xx)
                    <br />
                    <asp:TextBox ID="txtRBAcctBalance" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnRSubmit" runat="server" Text="Submit" />
                </div>
                <div ID="Customer" runat="server">
                    Customer Name: 
                    <br /> 
                    <asp:TextBox ID="txtCName" runat="server"></asp:TextBox>
                    <br />
                    Customer Address: 
                    <br />
                    <asp:TextBox ID="txtCAddress" runat="server"></asp:TextBox>
                    <br />
                    Bank Account Number (10 digits):
                    <br />
                    <asp:TextBox ID="txtCBAcctNum" runat="server"></asp:TextBox>
                    <br />
                    Bank Account Type (Checking, Saving, etc..)
                    <br />
                    <asp:TextBox ID="txtCBAcctType" runat="server"></asp:TextBox>
                    <br />
                    Bank Account Initial Balance ($xxx.xx)
                    <br />
                    <asp:TextBox ID="txtCBAcctBalance" runat="server"></asp:TextBox>
                    <br />
                   <asp:Button ID="btnCSubmit" runat="server" Text="Submit" />
                </div>
            </article>
        </section>

        <footer>
            <p>Food Delivery Site</p>
        </footer>
       
    </form>
</body>
</html>
