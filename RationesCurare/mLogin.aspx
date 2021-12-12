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

    <style>
        div {
            margin-bottom: 1rem;
        }

        select {
            box-sizing: border-box;
            width: 100%;
        }

        input:not([type='submit']) {
            box-sizing: border-box;
            width: 100%;
        }

        input[type='submit'] {
            box-sizing: border-box;
            cursor: pointer;
        }

        .descrizione {
            width: 100%;
        }
    </style>
</head>
<body>
    <h1>RationesCurare</h1>
    <hr />
    <h2>Login</h2>

    <form id="form1" runat="server">
        <div>
            <label for="eUtente">Utente</label>
            <asp:TextBox ID="eUtente" runat="server" placeholder="email" TextMode="Email" />
        </div>
        <div>
            <label for="ePsw">Password</label>
            <asp:TextBox ID="ePsw" runat="server" placeholder="Password" TextMode="Password" />
        </div>

        <div>
            <label for="cbMemorizza">Memorizza</label>
            <select id="cbMemorizza" runat="server">
                <option value="0" selected>No</option>
                <option value="1">Sì</option>                
            </select>
        </div>

        <div>            
            <asp:Button ID="bEntra" runat="server" Text="Entra" OnClick="bEntra_Click" />
            <asp:Button ID="bRegistrati" runat="server" Text="Registrati" OnClick="bRegistrati_Click" />
        </div>

        <div>
            <em>
                <asp:Label ID="lErrore" runat="server" />
            </em>
        </div>
    </form>

    <hr />
    <ruby>© 2006-2021, [MAIONE MIKY]</ruby>
</body>
</html>
