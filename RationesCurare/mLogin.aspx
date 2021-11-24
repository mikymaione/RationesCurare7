<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mLogin.aspx.cs" Inherits="RationesCurare.mLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="favicon.ico">
    <link rel="stylesheet" type="text/css" href="css/F79E10.css">
    <link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Ubuntu Mono'>

    <meta charset="UTF-8">

    <meta name="date" content="2021-11-21" scheme="YYYY-MM-DD">
    <meta name="author" content="Maione Michele">
    <meta name="description" content="Maione Michele’s personal website">

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>RationesCurare - Login</title>
</head>
<body>
    <h1>RationesCurare</h1>
    <hr/>
    <h2>Login</h2>

    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:TextBox ID="eUtente" runat="server" placeholder="Utente" TextMode="Email" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="ePsw" runat="server" placeholder="Password" TextMode="Password" />
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
    </form>

    <hr/>
    <ruby>© 2006-2021, [MAIONE MIKY]</ruby>
</body>
</html>
