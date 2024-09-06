<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="RationesCurare.ErrorPage" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">

	<meta charset="UTF-8">
	<meta name="date" content="2024-09-06" scheme="YYYY-MM-DD">
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

	<link rel="stylesheet" type="text/css" href="css/rc/F79E10.css?version=20240906">
	<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Ubuntu Mono">
	<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">

	<title>RationesCurare - Error</title>

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

	<section class="giustificato">
		<h3>Something went wrong!</h3>
		<p>An error occurred while loading the page. Please return to the <a href="welcome.aspx">main page</a> or contact the site <a rel="noopener" target="_blank" href="mailto:mikymaione@hotmail.it">administrator</a> if the problem persists.</p>
		<asp:Literal ID="ErrorMessageLiteral" runat="server"></asp:Literal>
		<asp:Literal ID="ErrorDetailLiteral" runat="server"></asp:Literal>
	</section>

	<hr />
	<ruby>RationesCurare © 2006-2024, [MAIONE MIKY]</ruby>
</body>
</html>