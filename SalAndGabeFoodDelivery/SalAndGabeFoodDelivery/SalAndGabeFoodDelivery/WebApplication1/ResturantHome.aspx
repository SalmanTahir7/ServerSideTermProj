﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResturantHome.aspx.cs" Inherits="WebApplication1.ResturantHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sal & Gabe's</title>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
         <link rel="stylesheet" href="CustomerStyles.css" />
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
         <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

           <div class="row" id="headRow">
                <div class="col-sm-2"></div>
                <div class="col-sm-8">
                    <asp:Label runat="server">Sal & Gabe's Food Service</asp:Label>
                </div>
                <div class="col-sm-2"></div>
            </div>
            <br /><br />

            <div class="row" id="menuRow">
                <div class="col-sm-3">
                    <asp:Label runat="server" ID="restName"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <asp:Button ID="btnOrders" runat="server" Text="Orders"/>
                </div>
                <div class="col-sm-3">
                    <asp:Button ID="btnAcctInfo" runat="server" Text="Update Account" Width="122px" OnClick="btnAcctInfo_Click"/>
                </div>
                <div class="col-sm-3">
                    <asp:Button runat="server" ID="btnPays" Text="Payment Methods" Width="122px" />
                </div>
            </div>

            <div id="divOrderGv" runat="server">
                <asp:GridView ID="gvOrderss" runat="server" AutoGenerateColumns="false"></asp:GridView>
                <br />
                <asp:Button ID="btnUpdateOrder" runat="server" Text="Update Orders" />
            </div>
            



            <div id="divUpdateAcctR" runat="server" visible="false">
                <asp:Label ID="lblAcct" runat="server">Update your Account Information Here! Click the "Update Account" Button at the end when finished.</asp:Label>
                <br />

                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                        <asp:Label runat="server">Resturant Image: </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <input type="file" />
                    </div>
                    <div class="col-sm-3"></div>
                     </div>

                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                        <asp:Label runat="server">Resturant Name: </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRestName" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-3"></div>
                     </div>
                
                <br />
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                        <asp:Label runat="server">Resturant Email: </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRestEmail" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-3"></div>
                </div>

                <br />
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                        <asp:Label runat="server">Resturant Address: </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRestAddy" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-3"></div>
                </div>

                <br />
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                        <asp:Label runat="server">Resturant Phone Number: </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRestPhoneNo" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-3"></div>
                </div>

                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-3">
                        <asp:Label runat="server">Password: </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txtRestPassword" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-3"></div>
                </div>

                <div class="row" id="btnSave">
                    <div class="col-sm-5"></div>
                    <div class="col-sm-2">
                        <asp:Button ID="btnSaveAcct" runat="server" Text="Update Account" />
                    </div>
                    <div class="col-sm-5"></div>

                </div>
            </div>

        </div>
    </form>
</body>
</html>
