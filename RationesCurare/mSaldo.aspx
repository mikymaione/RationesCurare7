<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mSaldo.aspx.cs" Inherits="RationesCurare.mSaldo" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="css/rc/awesomplete.css" />
    <script src="css/rc/awesomplete.min.js"></script>

    <h2><%=SottoTitolo%></h2>

    <script>
        var isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
    </script>

    <table width="100%">
        <thead>
            <tr style="background-color: black; color: white">
                <th colspan="2" style="text-align: left">Filters</th>
            </tr>
        </thead>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="bData" runat="server" Text="Enable filter by dates" />
            </td>
        </tr>
        <tr>
            <td>From</td>
            <td>
                <asp:TextBox ID="eDataDa" runat="server" TextMode="Date" MaxLength="10" Width="100%" Placeholder="Date the transaction was performed" />
            </td>
        </tr>
        <tr>
            <td>Until</td>
            <td>
                <asp:TextBox ID="eDataA" runat="server" TextMode="Date" MaxLength="10" Width="100%" Placeholder="Date the transaction was performed" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="bSoldi" runat="server" Text="Enable filter by amounts" />
            </td>
        </tr>

        <tr>
            <td>From</td>
            <td>
                <input id="eSoldiDa" runat="server" type="number" step="0.01" min="-10000000" max="10000000" placeholder="Transaction amount in money">
            </td>
        </tr>
        <tr>
            <td>To</td>
            <td>
                <input id="eSoldiA" runat="server" type="number" step="0.01" min="-10000000" max="10000000" placeholder="Transaction amount in money">
            </td>
        </tr>

        <tr>
            <td colspan="2">Filter by category</td>
        </tr>
        <tr>
            <td colspan="2">
                <input id="idMacroarea" data-minchars="1" runat="server" maxlength="250" list="dlMacroaree" autocomplete="off" placeholder="Category to which the transaction belongs">
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

                            new Awesomplete(idMacroarea, {list: dlMacroaree});
                        } catch(err) {
                            print(err);
                        }
                    }
                </script>
            </td>
        </tr>

        <tr>
            <td colspan="2">Filter by description</td>
        </tr>
        <tr>
            <td colspan="2">                
                <input id="idDescrizione" data-minchars="1" name="idDescrizione" runat="server" list="dlDescrizioni" autocomplete="off" placeholder="Operation Description">
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

                <script>
                    if (isMobile) {
                        try {
                            let idDescrizione = document.getElementById("<%=idDescrizione.ClientID%>");
                            let dlDescrizioni = document.getElementById("dlDescrizioni");

                            new Awesomplete(idDescrizione, { list: dlDescrizioni });
                        } catch (err) {
                            print(err);
                        }
                    }
                </script>
            </td>
        </tr>

        <tr>
            <td colspan="2">Filter by account</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DropDownList ID="idCassa" runat="server" DataTextField="Nome" DataValueField="Nome" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text="--nessuno--" Selected="true" />
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="2">Maximum number of amounts to show</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DropDownList ID="eMax" runat="server" Width="100%">
                    <asp:ListItem Selected="True" Value="50">50</asp:ListItem>
                    <asp:ListItem Value="250">250</asp:ListItem>
                    <asp:ListItem Value="500">500</asp:ListItem>
                    <asp:ListItem Value="1000">1000</asp:ListItem>
                    <asp:ListItem Value="10000">10000</asp:ListItem>
                    <asp:ListItem Value="100000">100000</asp:ListItem>
                    <asp:ListItem Value="1000000">1000000</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <br>
                <style>
                    .spazia {
                        display: flex;
                        justify-content: space-between;
                    }
                </style>
                <asp:Panel ID="pSearch" runat="server" DefaultButton="bCerca" CssClass="spazia">
                    <asp:Button ID="bCerca" runat="server" Text="Search" />
                    <asp:Button ID="bResetta" runat="server" Text="Clear all filters" OnClick="bResetta_Click" />
                </asp:Panel>
            </td>
        </tr>
    </table>

    <hr />

    <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowSorting="False" AutoGenerateColumns="False" DataKeyNames="ID" Width="100%" AllowPaging="False" ShowFooter="True" RowStyle-CssClass="trGrid"
        OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
        OnRowDataBound="GridView1_RowDataBound">
        
        <FooterStyle BackColor="Black" ForeColor="White" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />

        <Columns>
            <asp:TemplateField HeaderText="Description" ItemStyle-Wrap="true" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>                    
                    <div>
                        <asp:Label runat="server" Text='<%# Eval("Tipo") %>' CssClass="trLabelC" Visible="<%# TipoVisibile %>" />
                        <asp:Label runat="server" Text='<%# Eval("MacroArea") %>' CssClass="trLabel" />
                    </div>
                    <div>
                        <small>                      
                            <asp:Literal runat="server" Text='<%# Eval("descrizione") %>' />
                        </small>
                    </div>
                    <div>
                        <ruby>
                            <asp:Literal ID="lblData" runat="server" Text='<%# RationesCurare.GB.ObjectToDateTimeString(Eval("data")) %>'></asp:Literal>
                        </ruby>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>            

            <asp:TemplateField HeaderText="Amount" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" CssClass='<%# RationesCurare.GB.GetColor(Eval("soldi")) %>' Text='<%# Eval("soldi", "{0:c}") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <asp:Button runat="server" CssClass="myBtn googleIcon" ToolTip="New amount" Text="local_atm" ID="bNuovo" OnClick="bNuovo_Click" />    
</asp:Content>
