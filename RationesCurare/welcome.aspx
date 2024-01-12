﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="RationesCurare.welcome" %>

<!DOCTYPE html>

<html>
<head runat="server">
  
    <link rel="apple-touch-icon" sizes="180x180" href="favicon/apple-touch-icon.png">
    <link rel="icon" type="image/png" sizes="32x32" href="favicon/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="16x16" href="favicon/favicon-16x16.png">    

    <link rel="manifest" href="favicon/site.json">
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
    <h2>Install</h2>
    
    <p>Welcome to RationesCurare, an open-source software for the management of the personal economy.</p>

    <button id="butInstall" name="butInstall" type="button">Install</button> 

    <script>
        window.addEventListener('beforeinstallprompt', (event) => {
            // Prevent the mini-infobar from appearing on mobile.
            event.preventDefault();
            console.log('👍', 'beforeinstallprompt', event);

            // Stash the event so it can be triggered later.
            window.deferredPrompt = event;            
        });

        window.addEventListener('appinstalled', (event) => {
            console.log('👍', 'appinstalled', event);

            // Clear the deferredPrompt so it can be garbage collected
            window.deferredPrompt = null;
        });

        butInstall.addEventListener('click', async () => {
            console.log('👍', 'butInstall-clicked');

            const promptEvent = window.deferredPrompt;

            if (!promptEvent) {
                // The deferred prompt isn't available.
                return;
            }

            // Show the install prompt.
            promptEvent.prompt();

            // Log the result
            const result = await promptEvent.userChoice;
            console.log('👍', 'userChoice', result);

            // Reset the deferred prompt variable, since
            // prompt() can only be called once.
            window.deferredPrompt = null;            
        });
    </script>

    <hr />
    <ruby>© 2006-2024, [MAIONE MIKY]</ruby>
</body>
</html>
