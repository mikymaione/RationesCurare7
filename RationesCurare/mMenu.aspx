<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mMenu.aspx.cs" Inherits="RationesCurare.mMenu" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Balance</h2>

    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Tipo" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="False" ShowFooter="True" Width="100%" GridLines="None" RowStyle-CssClass="trGrid"
        OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
        OnRowDataBound="GridView1_RowDataBound">
        
        <FooterStyle BackColor="Black" ForeColor="White" />
        <HeaderStyle BackColor="Black" ForeColor="White" />

        <Columns>
            <asp:TemplateField HeaderText="Account" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Literal runat="server" Text='<%# Eval("Tipo") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Balance" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" CssClass='<%# RationesCurare.GB.GetColor(Eval("Saldo")) %>' Text='<%# Eval("Saldo", "{0:c}") %>' />
                </ItemTemplate>

                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" />
                </FooterTemplate>

                <FooterStyle HorizontalAlign="Right" Wrap="False"></FooterStyle>

                <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>

    <asp:Button runat="server" CssClass="myBtn googleIcon" ToolTip="New amount" Text="local_atm" ID="bNuovo" OnClick="bNuovo_Click" />
</asp:Content>
