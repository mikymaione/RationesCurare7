<%@ Page Title="" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mMenu.aspx.cs" Inherits="RationesCurare.mMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .trGrid {
            cursor: pointer;
        }

            .trGrid:hover {
                color: black !important;
                background-color: #F79E10 !important;
            }

                .trGrid:hover span {
                    color: black;
                }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Saldo</h2>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False" ShowFooter="True" Width="100%" GridLines="None" RowStyle-CssClass="trGrid"
        OnRowDataBound="GridView1_RowDataBound">

        <AlternatingRowStyle BackColor="#f0f0f0" />
        <FooterStyle BackColor="Black" ForeColor="White" />
        <HeaderStyle BackColor="Black" ForeColor="White" />

        <Columns>
            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate>
                    <input type="hidden" value='<%# Eval("Tipo") %>' />
                    <asp:Literal runat="server" Text='<%# Eval("Tipo") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Saldo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" Text='<%# Eval("Saldo", "{0:c}") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" />
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <script>
        function addRowHandlers() {
            let table = document.getElementById("<%=GridView1.ClientID%>");
            let rows = table.getElementsByTagName("tr");

            for (let i = 0; i < rows.length; i++) {
                let currentRow = table.rows[i];

                let createClickHandler = function (row) {
                    return function () {
                        let cell = row.getElementsByTagName("td")[0];
                        let hidden = cell.getElementsByTagName("input")[0];

                        window.location.href = 'mSaldo.aspx?T=' + hidden.value;
                    };
                };

                currentRow.onclick = createClickHandler(currentRow);
            }
        }

        addRowHandlers();
    </script>
</asp:Content>
