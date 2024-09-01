<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="RationesCurare.index" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">

	<meta charset="UTF-8">
	<meta name="date" content="2024-08-31" scheme="YYYY-MM-DD">
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

	<link rel="stylesheet" type="text/css" href="css/rc/F79E10.css?version=20240831">
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Ubuntu Mono">
	<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

	<title>RationesCurare</title>

	<style>
		body {
			font-size: large;
		}

		p {
			text-align: justify;
		}

		div {
            margin-bottom: 1rem;
        }
	</style>
</head>
<body>
	<h1 style="display: flex"><img src="favicon/favicon_orange.svg">RationesCurare</h1>
	<p>an open-source software for the management of the personal economy.</p>
	<hr />

	<p>RationesCurare is a web application designed to help you manage your home finances with ease, freely licensed under the <a target="_blank" href="https://www.gnu.org/licenses/gpl-3.0.html#license-text">GNU</a> GPL.</p>
	<p>Designed to be easy to use, yet powerful and flexible, RationesCurare allows you to track bank accounts, stocks, income and expenses. As quick and intuitive to use as a checkbook register, it is based on professional accounting principles to ensure balanced books and accurate reports.</p>

	<section class="giustificato">
		<h3>Features</h3>
		<ul>
			<li>
				<b>Track finances</b>: monitor your bank accounts, stocks, income, and expenses with ease;
			</li>
			<li>
				<b>User-friendly</b>: simple and intuitive interface, designed for everyone;
			</li>
			<li>
				<b>Professional accounting</b>: ensures balanced books and accurate reports;
			</li>
			<li>
				<b>Open source</b>: developed, maintained, documented, and translated entirely by volunteers. Check the <a target="_blank" href="https://github.com/mikymaione/RationesCurare7">GitHub</a> repository.
			</li>
		</ul>
	</section>

	<section>
		<h2>Get started</h2>
		<p>Ready to take control of your personal finances?</p>
		
		<form id="form1" runat="server">       
			<button id="butInstall" name="butInstall" type="button" class="bottomspace" style="display: none;">Install the app on your device</button> 
			<asp:Button ID="bEntra" runat="server" CssClass="bottomspace" Text="Access RationesCurare" OnClick="bEntra_Click" />
		</form>
	</section>

	<script>
        window.addEventListener('beforeinstallprompt', (event) => {
            // Prevent the mini-infobar from appearing on mobile.
            event.preventDefault();
            console.log('👍', 'beforeinstallprompt', event);

            // Stash the event so it can be triggered later.
            window.deferredPrompt = event;      

            // Show install button.     
            document.getElementById('butInstall').style.display = 'inline';
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

            // Hide the install button.
            document.getElementById('butInstall').style.display = 'none';
        });
    </script>

	<hr />
	<ruby>RationesCurare © 2006-2024, [MAIONE MIKY]</ruby>
</body>
</html>
