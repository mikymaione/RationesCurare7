﻿<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mCassa.aspx.cs" Inherits="RationesCurare.mCassa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2><%=SottoTitolo %></h2>

    <div>
        <label class="required" for="idNome">Nome</label>
        <input id="idNome" runat="server" maxlength="255" autocomplete="on" autofocus required >
    </div>
    <div>
        <label for="idNascondi">Nascondi</label>
        <select id="idNascondi" runat="server">
            <option value="0">No</option>
            <option value="1">Sì</option>
        </select>
    </div>

    <div>
        <asp:Button ID="bSalva" runat="server" Text="Salva" OnClick="bSalva_Click" />
    </div>
    <div>
        <em>
            <asp:Label ID="lErrore" runat="server" />
        </em>
    </div>

</asp:Content>
