<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mRegister.aspx.cs" Inherits="RationesCurare.mRegister" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <meta charset="UTF-8">
    <meta name="date" content="2024-08-23" scheme="YYYY-MM-DD">
    <meta name="author" content="Maione Michele">
    <meta name="description" content="An open-source software for the management of the personal economy">    
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <meta name="msapplication-config" content="/favicon/browserconfig.xml">
    <meta name="msapplication-TileColor" content="#f79e10">
    <meta name="theme-color" content="#f79e10">
    <meta name="apple-mobile-web-app-capable" content="yes">

    <link rel="apple-touch-icon" href="/favicon/apple-touch-icon.png">
    <link rel="mask-icon" href="/favicon/safari-pinned-tab.svg" color="#f79e10">
    <link rel="shortcut icon" href="/favicon/favicon.ico">   

    <link rel="icon" type="image/png" sizes="32x32" href="/favicon/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="/favicon/favicon-16x16.png">
    <link rel="icon" type="image/png" sizes="192x192" href="/favicon/android-chrome-192x192.png">
    <link rel="icon" type="image/png" sizes="512x512" href="/favicon/android-chrome-512x512.png">

    <link rel="manifest" href="/favicon/site.json">  

    <link rel="stylesheet" type="text/css" href="css/rc/F79E10.css?version=20240823">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Ubuntu Mono">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

    <title>RationesCurare - Sign Up</title>

    <style>
        div {
            margin-bottom: 1rem;
        }

        small {
            display: block;
        }
    </style>
</head>
<body>
    <h1><img class="mycolor" src="favicon/favicon.svg"> RationesCurare</h1>
    <p class="giustificato">an open-source software for the management of the personal economy.</p>
    <hr />
    <h2>Sign Up</h2>

    <form id="form1" runat="server">    
        <div>
            <label class="required" for="eNickName">Your Name</label>          
            <input id="eNickName" name="eNickName" runat="server" placeholder="Default change author" required>
        </div>
        <div>
            <label class="required" for="eUtente">Email</label>          
            <input id="eUtente" name="eUtente" runat="server" placeholder="You will not receive any advertising" type="email" required>
        </div>
        <div>
            <label class="required" for="ePsw">Password</label>            
            <input id="ePsw" name="ePsw" runat="server" placeholder="Non-changeable and non-recoverable" type="password" required>
            <small class="mytextcolor giustificato">Since the password will be used to encrypt your database and there is no possibility of recovery, we recommend that you save the password in your browser's password manager.</small>
        </div>
        <div>
            <label class="required" for="eLanguage">Language</label>
            <asp:DropDownList ID="eLanguage" runat="server" DataTextField="description" DataValueField="code" />
        </div>
        <div>                    
            <asp:Button ID="bRegistrati" runat="server" Text="Sign Up" OnClick="bRegistrati_Click" />            
        </div>

        <div>
            <em>
                <asp:Label ID="lErrore" runat="server" />
            </em>
        </div>
    </form>

    <hr />
    <ruby>RationesCurare © 2006-2024, [MAIONE MIKY]</ruby>
</body>
</html>
