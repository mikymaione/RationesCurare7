<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mLogin.aspx.cs" Inherits="RCWebMobile.mLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>RC Web - Login</title>
        
        <link rel="shortcut icon" type="image/ico" href="http://www.maionemiky.it/favicon.ico">
        <meta name="viewport" content="user-scalable=no, width=device-width">
        
        <style type="text/css">
            html
            {
                height: 100%;
            }
        
            body
            {
                height: 100%;
                margin: 0;
                padding: 0;
                background-color: #F4F7F9;
                background-image: url('images/spaziatore_login.png');
                background-repeat: repeat-y;
            }               
        </style>    
    </head>

    <body>
        <form id="form1" runat="server" defaultbutton="bEntra">
        <div>
            <table style="width: 350px; background-image: url('images/nuvoletta.png'); background-repeat: no-repeat;
                background-position: right top">
                <tr>
                    <td>
                        <img src="images/logo.png" />
                    </td>
                    <td>
                        <strong style="font-size: xx-large;">RC Web</strong>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="eUtente" runat="server" placeholder="Utente" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="ePsw" runat="server" placeholder ="Password" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="cbMemorizza" runat="server" Text="Memorizza" />
                                    <asp:Button ID="bEntra" runat="server" Text="Entra" OnClick="bEntra_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lErrore" runat="server" ForeColor="Red" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </body>
</html>