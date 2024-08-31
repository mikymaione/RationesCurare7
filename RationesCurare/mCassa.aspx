<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mCassa.aspx.cs" Inherits="RationesCurare.mCassa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            margin-top: -16px;
        }

        div {
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2><%=SottoTitolo %></h2>

    <div>
        <label class="required" for="idNome">Name</label>
        <input id="idNome" runat="server" maxlength="255" autocomplete="on" autofocus required >
    </div>
    <div>
        <label for="idNascondi">Hide</label>
        <select id="idNascondi" runat="server">
            <option value="0">No</option>
            <option value="1">Yes</option>
        </select>
    </div>

    <div>
        <asp:Button ID="bSalva" runat="server" Text="Save" OnClick="bSalva_Click" />
    </div>
    <div>
        <em>
            <asp:Label ID="lErrore" runat="server" />
        </em>
    </div>

</asp:Content>
