﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mLogin.aspx.cs" Inherits="RationesCurare.mLogin" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <meta charset="UTF-8">
    <meta name="date" content="2024-05-09" scheme="YYYY-MM-DD">
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

    <link rel="stylesheet" type="text/css" href="css/rc/F79E10.css?version=20240509">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Ubuntu Mono">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

    <title>Rationes Curare</title>

    <style>
        div {
            margin-bottom: 1rem;
        }      
    </style>
</head>
<body>
    <h1>RationesCurare</h1>
    <hr />
    <h2>Login</h2>

    <form id="form1" runat="server">    
        <div id="divNickName" runat="server" visible="false">
            <label class="required" for="eNickName">Utente</label>          
            <input id="eNickName" name="eNickName" runat="server" placeholder="Utente" required>
        </div>
        <div>
            <label class="required" for="eUtente">Email</label>          
            <input id="eUtente" name="eUtente" runat="server" placeholder="Email" type="email" required>
        </div>
        <div>
            <label class="required" for="ePsw">Password</label>          
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
            <a href="welcome.aspx"><button type="button">Installa la app</button></a> 
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
