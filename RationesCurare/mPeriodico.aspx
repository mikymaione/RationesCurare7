<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mPeriodico.aspx.cs" Inherits="RationesCurare.mPeriodico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            margin-bottom: 1rem;
        }       
    </style>

    <script>
        function resoconto() {
            let idSoldi = document.getElementById('<%=idSoldi.ClientID%>');
            let movi_soldi = idSoldi.value.length == 0 ? 0 : parseFloat(idSoldi.value);            
            let lMovimento1 = document.getElementById('lMovimento1');            
            let idCassa = document.getElementById('<%=idCassa.ClientID%>');            

            lMovimento1.innerText = (movi_soldi < 0 ? '' : '+') + movi_soldi + ': ' + idCassa.value;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2><%=SottoTitolo %></h2>

    <asp:ScriptManager ID="smMain" runat="server" EnablePageMethods="true" />
    
    <div>
        <label class="required" for="idSoldi">Importo</label>
        <input id="idSoldi" runat="server" type="number" inputmode="decimal" step="0.01" min="-10000000" max="10000000" autofocus required onchange="resoconto();">
    </div>    
    <div>
        <script>
            function selectMacroArea() {
                let des = document.getElementById('<%=idDescrizione.ClientID%>').value;
                let userName = '<%=userName%>';

                PageMethods.getMacroAreaByDescrizione(userName, des, selectMacroArea_OnSuccess);
            }

            function selectMacroArea_OnSuccess(response, userContext, methodName) {
                let ma = String(response);

                if (ma) {
                    document.getElementById('<%=idMacroarea.ClientID%>').value = ma;
                }                
            }
        </script>

        <label class="required" for="idDescrizione">Descrizione</label>
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
        <label class="required" for="idMacroarea">Macroarea</label>
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
        <label class="required" for="idCassa">Dal account</label>
        <asp:DropDownList ID="idCassa" runat="server" DataTextField="Nome" DataValueField="Nome" AppendDataBoundItems="true" onchange="resoconto();"></asp:DropDownList>
    </div>
    <div>
        <script>
            function showNumeroGiorni()
            {                
                let bScadenza = document.getElementById('<%=bScadenza.ClientID%>');
                let idScadenza = document.getElementById('<%=idScadenza.ClientID%>');
                let idTipoGiorniMese = document.getElementById('<%=idTipoGiorniMese.ClientID%>');
                let idNumeroGiorni = document.getElementById('<%=idNumeroGiorni.ClientID%>');
                let divNumeroGiorni = document.getElementById('divNumeroGiorni');
                
                if (idTipoGiorniMese.value == 'G') {
                    idNumeroGiorni.required = true;    
                    divNumeroGiorni.style.display = 'inherit';
                } else {
                    idNumeroGiorni.required = false;
                    idNumeroGiorni.value = null;
                    divNumeroGiorni.style.display = 'none';
                }                               

                if (bScadenza.checked) {
                    idScadenza.disabled = false;
                    idScadenza.required = true;
                } else {
                    idScadenza.value = null;
                    idScadenza.disabled = true;                    
                    idScadenza.required = false;
                }
            }
        </script>
        <label class="required" for="idTipoGiorniMese">Periodicità</label>
        <select id="idTipoGiorniMese" runat="server" required onchange="showNumeroGiorni();">
            <option value="G">Giornaliero</option>
            <option value="M">Mensile</option>
            <option value="B">Bimestrale</option>
            <option value="T">Trimestrale</option>
            <option value="Q">Quadrimestrale</option>
            <option value="S">Semestrale</option>
            <option value="A">Annuale</option>
        </select>
    </div>
    <div>
        <label class="required" for="idData">Partendo dal giorno</label>
        <input id="idData" runat="server" type="date" required>
    </div>
    <div id="divNumeroGiorni">
        <label class="required" for="idNumeroGiorni">Ogni quanti giorni</label>
        <input id="idNumeroGiorni" runat="server" type="number" step="1" min="1" max="3650" maxlength="4" autofocus required onchange="resoconto();">
    </div>    
    <div>        
        <input id="bScadenza" runat="server" type="checkbox" onchange="showNumeroGiorni();">
        <label class="required" for="idScadenza">Scadenza</label>        
        <input id="idScadenza" runat="server" type="date" required>
    </div>
    <div>
        <label class="required" for="idNome">Autore</label>
        <input id="idNome" runat="server" maxlength="255" autocomplete="on" required>
    </div>

    <h3>Resoconto</h3>
    <div>
        <label id="lMovimento1"></label>
    </div>
    <div>
        <label id="lMovimento2"></label>
    </div>

    <div class="divSpaceBetween">
        <script>
            function confirmDelete() {
                return confirm("Vuoi eliminare questo elemento?");                
            }
        </script>

        <asp:Button ID="bSalva" runat="server" Text="Salva" OnClick="bSalva_Click" />
        <asp:Button ID="bElimina" runat="server" Text="Elimina" OnClientClick="return confirmDelete();" OnClick="bElimina_Click" />
    </div>
    <div>
        <em>
            <asp:Label ID="lErrore" runat="server" />
        </em>
    </div>

    <script>
        showNumeroGiorni();
        resoconto();        
    </script>

    <br />
</asp:Content>
