<%@ Page Title="" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mMenu.aspx.cs" Inherits="RationesCurare.mMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RationesCurare - Saldo</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Saldo</h2>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True" Width="100%" GridLines="None"
        OnRowDataBound="GridView1_RowDataBound">

        <AlternatingRowStyle BackColor="#f0f0f0" />
        <FooterStyle BackColor="Black" ForeColor="White" />
        <HeaderStyle BackColor="Black" ForeColor="White" />

        <Columns>
            <asp:TemplateField HeaderText="Tipo">
                <ItemTemplate>
                    <a href='<%# Eval("Tipo", "mSaldo.aspx?T={0}") %>'>
                        <%# Eval("Tipo") %>
                    </a>
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
</asp:Content>
