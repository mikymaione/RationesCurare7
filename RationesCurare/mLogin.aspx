<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mLogin.aspx.cs" Inherits="RationesCurare.mLogin" %>

<!DOCTYPE html>

<html>
<head runat="server">
  
    <link rel="apple-touch-icon" sizes="180x180" href="favicon/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="favicon/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="favicon/favicon-16x16.png">    

    <link rel="manifest" href="favicon/site.webmanifest">
    <link rel="mask-icon" href="favicon/safari-pinned-tab.svg" color="#F79E10">
    
    <meta name="msapplication-TileColor" content="#F79E10">
    <meta name="theme-color" content="#F79E10">

    <link rel="stylesheet" type="text/css" href="css/F79E10.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Ubuntu Mono">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

    <meta charset="UTF-8">
   
    <meta name="date" content="2024-01-12" scheme="YYYY-MM-DD">
    <meta name="author" content="Maione Michele">
    <meta name="description" content="RationesCurare">

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Rationes Curare</title>

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

        input:user-invalid {
            border-color: #F79E10;
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
            <input id="eUtente" name="eUtente" runat="server" placeholder="Email" type="email" required>
        </div>
        <div>
            <label for="ePsw">Password</label>          
            <input id="ePsw" name="ePsw" runat="server" placeholder="Password" type="password" required>
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
    <ruby>© 2006-2024, [MAIONE MIKY]</ruby>
</body>
</html>
