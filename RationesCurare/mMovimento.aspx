<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mMovimento.aspx.cs" Inherits="RationesCurare.mMovimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/rc/awesomplete.css" />
    <script src="css/rc/awesomplete.min.js"></script>

    <title>RationesCurare - New amount</title>

    <style>
        body {
            margin-top: -16px;
        }

        div {
            margin-bottom: 1rem;
        }
    </style>

    <script>
    	var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
        var awsDesc, awsMacro;
        
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
        <label class="required" for="idCassa">From account</label>
        <asp:DropDownList ID="idCassa" runat="server" DataTextField="Nome" DataValueField="Nome" AppendDataBoundItems="true" onchange="resoconto();" />
    </div>
    <div id="divGiroconto" runat="server">
        <label for="idGiroconto">To account</label>
        <asp:DropDownList ID="idGiroconto" runat="server" DataTextField="Nome" DataValueField="Nome" AppendDataBoundItems="true" onchange="resoconto();" OnSelectedIndexChanged="idGiroconto_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Value="" Text="«no account»" Selected="true" />
        </asp:DropDownList>
    </div>
    <div>
        <label class="required" for="idSoldi">Transaction amount</label>
        <input id="idSoldi" runat="server" type="number" step="0.01" min="-10000000" max="10000000" required onchange="resoconto();" placeholder="Transaction amount in money">
    </div>
    <div>
        <label class="required" for="idData">Date</label>
        <br>
        <input id="idData" runat="server" type="datetime-local" required placeholder="Date the transaction was performed">
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

            function updateDescrizioni() {                             
                let input = document.getElementById('<%=idDescrizione.ClientID%>').value;
                let userName = '<%=userName%>';
                
                PageMethods.getDescrizioni(userName, input, onGetDescrizioniSuccess);
            }

            function onGetDescrizioniSuccess(response) {
                if (isMobile) {
                    let ds = [];

                    response.forEach(function (descrizione) {
                        ds.push(descrizione);
                    });

                    let idDescrizione = document.getElementById('<%=idDescrizione.ClientID%>');
                    awsDesc.list = ds;
                } else {
                    let dlDescrizioni = document.getElementById('dlDescrizioni');
                    dlDescrizioni.innerHTML = '';

                    response.forEach(function (descrizione) {
                        let option = document.createElement('option');
                        option.value = descrizione;
                        dlDescrizioni.appendChild(option);
                    });
                }          
            }
        </script>

        <label id="lDescrizione" class="required" for="idDescrizione" runat="server">Description</label>
        <br>
        <input id="idDescrizione" data-minchars="1" name="idDescrizione" runat="server" list="dlDescrizioni" onblur="selectMacroArea()" oninput="updateDescrizioni()" required autocomplete="off" placeholder="Operation Description" >
        <datalist id="dlDescrizioni" />                  
        
        <script>
            if (isMobile) {
                try {
                    let idDescrizione = document.getElementById("<%=idDescrizione.ClientID%>");
                    let dlDescrizioni = document.getElementById("dlDescrizioni");
                    dlDescrizioni.remove();

                    awsDesc = new Awesomplete(idDescrizione);
                } catch (err) {
                    print(err);
                }
            }
        </script>
    </div>
    <div>
        <label id="lMacroarea" class="required" for="idMacroarea" runat="server">Category</label>
        <br>
        <input id="idMacroarea" data-minchars="1" runat="server" maxlength="250" list="dlMacroaree" required autocomplete="off" placeholder="Category to which the transaction belongs">
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
        
        <script>
            if (isMobile) {
                try {
                    let idMacroarea = document.getElementById("<%=idMacroarea.ClientID%>");
                    let dlMacroaree = document.getElementById("dlMacroaree");

                    awsMacro = new Awesomplete(idMacroarea, {list: dlMacroaree});
                } catch(err) {
                    print(err);
                }
            }
        </script>
    </div>    
    <div>
        <label class="required" for="idNome">Author</label>
        <input id="idNome" runat="server" maxlength="255" autocomplete="on" required>
    </div>

    <h3>Summary</h3>
    <div>
        <label id="lMovimento1"></label>
    </div>
    <div>
        <label id="lMovimento2"></label>
    </div>

    <div class="divSpaceBetween">
        <script>
            function confirmDelete() {
                return confirm("Do you want to delete this item?");                
            }
        </script>

        <asp:Button ID="bSalva" runat="server" Text="Save" OnClick="bSalva_Click" />
        <asp:Button ID="bElimina" runat="server" Text="Delete" OnClientClick="return confirmDelete();" OnClick="bElimina_Click" />
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
