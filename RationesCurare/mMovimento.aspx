<%@ Page Title="" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mMovimento.aspx.cs" Inherits="RationesCurare.mMovimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    <script>
        function resoconto() {
            let idSoldi = document.getElementById('<%=idSoldi.ClientID%>');
            let movi_soldi = idSoldi.value.length == 0 ? 0 : parseFloat(idSoldi.value);
            let giro_soldi = (-1 * movi_soldi);

            let lMovimento1 = document.getElementById('lMovimento1');
            let lMovimento2 = document.getElementById('lMovimento2');

            let idCassa = document.getElementById('<%=idCassa.ClientID%>');
            let idGiroconto = document.getElementById('<%=idGiroconto.ClientID%>');

            lMovimento1.innerText = (movi_soldi < 0 ? '' : '+') + movi_soldi + ': ' + idCassa.value;

            if (idGiroconto.selectedIndex > 0) {
                lMovimento2.innerText = (giro_soldi < 0 ? '' : '+') + giro_soldi + ': ' + idGiroconto.value;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2><%=SottoTitolo %></h2>

    <asp:ScriptManager ID="smMain" runat="server" EnablePageMethods="true" />

    <div>
        <label for="idNome">Nome</label>
        <input id="idNome" runat="server" maxlength="255" autocomplete="on" autofocus required>
    </div>
    <div>
        <label for="idSoldi">Importo</label>
        <input id="idSoldi" runat="server" type="number" step="0.01" maxlength="11" required onchange="resoconto();">
    </div>
    <div>
        <label for="idData">Data</label>
        <input id="idData" runat="server" type="datetime-local" required>
    </div>
    <div>
        <script>
            function selectMacroArea() {
                let des = document.getElementById('<%=idDescrizione.ClientID%>').value;
                let userName = '<%=userName%>';

                PageMethods.getMacroAreaByDescrizione(userName, des, selectMacroArea_OnSuccess);
            }

            function selectMacroArea_OnSuccess(response, userContext, methodName) {
                document.getElementById('<%=idMacroarea.ClientID%>').value = response;
            }
        </script>

        <label for="idDescrizione">Descrizione</label>
        <input id="idDescrizione" name="idDescrizione" runat="server" list="dlDescrizioni" onblur="selectMacroArea()" required>
        <datalist id="dlDescrizioni">
            <%
                foreach (var de in getDescrizioni())
                {
            %>
            <option value="<%=de%>">
                <% 
                    }
                %>
        </datalist>
    </div>
    <div>
        <label for="idMacroarea">Macroarea</label>
        <input id="idMacroarea" runat="server" maxlength="250" list="dlMacroaree" required>
        <datalist id="dlMacroaree">
            <%
                foreach (var ma in getMacroAree())
                {
            %>
            <option value="<%=ma%>">
                <% 
                    }
                %>
        </datalist>
    </div>
    <div>
        <label for="idCassa">Cassa</label>
        <asp:DropDownList ID="idCassa" runat="server" DataTextField="Nome" DataValueField="Nome" AppendDataBoundItems="true" onchange="resoconto();"></asp:DropDownList>
    </div>
    <div>
        <label for="idGiroconto">Giroconto</label>
        <asp:DropDownList ID="idGiroconto" runat="server" DataTextField="Nome" DataValueField="Nome" AppendDataBoundItems="true" onchange="resoconto();">
            <asp:ListItem Value="" Text="--nessuno--" Selected="true"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <h3>Resoconto</h3>
    <div>
        <label id="lMovimento1"></label>
    </div>
    <div>
        <label id="lMovimento2"></label>
    </div>

    <div>
        <asp:Button ID="bSalva" runat="server" Text="Salva" OnClick="bSalva_Click" />

        <script>
            function confirmDelete() {
                let result = confirm("Vuoi eliminare questo elemento?");

                if (result) {
                    let movID = <%=IDMovimento%>;
                    location.href = 'mMovimento.aspx?DEL=' + movID;
                }
            }
        </script>

        <asp:Button ID="bElimina" runat="server" Text="Elimina" OnClientClick="return confirmDelete();" OnClick="bElimina_Click" />
    </div>
    <div>
        <em>
            <asp:Label ID="lErrore" runat="server" />
        </em>
    </div>

    <script>
        resoconto();
    </script>

    <br />
</asp:Content>
