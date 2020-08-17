<%@ Page Title="RC Web" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true"
    CodeBehind="mSaldo.aspx.cs" Inherits="RCWebMobile.mSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="bData" runat="server" Text="Data" />
            </td>            
        </tr>
        <tr>
            <td>
                Da <asp:TextBox ID="eDataDa" runat="server" TextMode="Date" MaxLength="10" />
            </td>
            <td>
                A <asp:TextBox ID="eDataA" runat="server" TextMode="Date" MaxLength="10" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CheckBox ID="bSoldi" runat="server" Text="Soldi" />
            </td>            
        </tr>
        <tr>
            <td>
                Da <asp:TextBox ID="eSoldiDa" runat="server" TextMode="Number" MaxLength="20" />
            </td>
            <td>
                A <asp:TextBox ID="eSoldiA" runat="server" TextMode="Number" MaxLength="20" />
            </td>
        </tr>
        <tr>
            <td>
                Macroarea <asp:TextBox ID="eMacroarea" runat="server" MaxLength="250" />
            </td>
            <td>
                Descrizione <asp:TextBox ID="eDescrizione" runat="server" MaxLength="500" />
            </td>
        </tr>
        <tr>
            <td>
                Max 
                <asp:DropDownList ID="eMax" runat="server">
                    <asp:ListItem Selected="True" Value="50">50</asp:ListItem>
                    <asp:ListItem Value="250">250</asp:ListItem>
                    <asp:ListItem Value="500">500</asp:ListItem>
                    <asp:ListItem Value="1000">1000</asp:ListItem>
                    <asp:ListItem Value="10000">10000</asp:ListItem>
                    <asp:ListItem Value="100000">100000</asp:ListItem>
                    <asp:ListItem Value="1000000">1000000</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="bCerca" runat="server" Text="Cerca" />
            </td>            
        </tr>
    </table>

    <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="Vertical" AllowSorting="False"
        AutoGenerateColumns="False" DataKeyNames="ID"
        Width="100%" AllowPaging="False" PageSize="50" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>            
            <asp:TemplateField HeaderText="soldi" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" Text='<%# Eval("soldi", "{0:c}") %>' />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblTotal" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="data" ItemStyle-Wrap="false">
                <ItemTemplate>                    
                    <asp:Literal ID="lblData" runat="server" Text='<%# RCWebMobile.GB.ObjectToDateTime(Eval("data")) %>'></asp:Literal>
                </ItemTemplate>                
            </asp:TemplateField>
            
            <asp:BoundField DataField="tipo" HeaderText="tipo" ItemStyle-Wrap="false">
                <ItemStyle Wrap="False"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MacroArea" HeaderText="macroarea"  />
            <asp:BoundField DataField="descrizione" HeaderText="descrizione"  />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>    
</asp:Content>
