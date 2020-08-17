<%@ Page Title="RC Web" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mMenu.aspx.cs" Inherits="RCWebMobile.mMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AllowSorting="False" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" ShowFooter="True" Width="100%"         
        OnRowDataBound="GridView1_RowDataBound" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
            <asp:TemplateField HeaderText="Saldo" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" Text='<%# Eval("Saldo", "{0:c}") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>    
</asp:Content>
