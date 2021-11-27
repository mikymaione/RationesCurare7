<%@ Page Title="" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mSaldo.aspx.cs" Inherits="RationesCurare.mSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style>
        input {
            box-sizing: border-box;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Movimenti</h2>

    <table width="100%">
        <thead>
            <tr style="background-color: black; color: white">
                <th colspan="2">Filtri</th>
            </tr>
        </thead>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="bData" runat="server" Text="Filtra per queste date" />
            </td>
        </tr>
        <tr>
            <td>Da</td>
            <td>
                <asp:TextBox ID="eDataDa" runat="server" TextMode="Date" MaxLength="10" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>A</td>
            <td>
                <asp:TextBox ID="eDataA" runat="server" TextMode="Date" MaxLength="10" Width="100%" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="bSoldi" runat="server" Text="Filtra per questi importi" />
            </td>
        </tr>

        <tr>
            <td>Da</td>
            <td>
                <asp:TextBox ID="eSoldiDa" runat="server" TextMode="Number" MaxLength="20" Width="100%" />
            </td>
        </tr>
        <tr>
            <td>A</td>
            <td>
                <asp:TextBox ID="eSoldiA" runat="server" TextMode="Number" MaxLength="20" Width="100%" />
            </td>
        </tr>

        <tr>
            <td colspan="2">Filtra per macroarea</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="eMacroarea" runat="server" MaxLength="250" Width="100%" />
            </td>
        </tr>

        <tr>
            <td colspan="2">Filtra per descrizione</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="eDescrizione" runat="server" MaxLength="500" Width="100%" />
            </td>
        </tr>

        <tr>
            <td colspan="2">Massimo numero righe</td>
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
                <asp:Button ID="bCerca" runat="server" Text="Filtra" />
                <asp:Button ID="bResetta" runat="server" Text="Pulisci" OnClick="bResetta_Click" />
            </td>
        </tr>
    </table>

    <hr />

    <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowSorting="False" AutoGenerateColumns="False" DataKeyNames="ID" Width="100%" AllowPaging="False" PageSize="50" ShowFooter="True"
        OnRowDataBound="GridView1_RowDataBound">

        <AlternatingRowStyle BackColor="#f0f0f0" />
        <FooterStyle BackColor="Black" ForeColor="White" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />

        <Columns>

            <asp:TemplateField HeaderText="Descrizione" ItemStyle-Wrap="true">
                <ItemTemplate>
                    <div>
                        <asp:Label runat="server" ForeColor="#F79E10" Text='<%# Eval("MacroArea") %>' />
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

            <asp:TemplateField HeaderText="Importo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" Text='<%# Eval("soldi", "{0:c}") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
</asp:Content>
