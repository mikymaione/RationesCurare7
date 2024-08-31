<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mPeriodici.aspx.cs" Inherits="RationesCurare.mPeriodici" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RationesCurare - Recurring transaction management</title>

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
    <h2>Recurring transaction management</h2>    

    <asp:GridView
        ID="GridView1" runat="server" GridLines="None" AllowSorting="False" 
        AutoGenerateColumns="False" DataKeyNames="ID" Width="100%" 
        AllowPaging="False" ShowFooter="True" RowStyle-CssClass="trGrid"
        OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
        OnRowDataBound="GridView1_RowDataBound">
        
        <FooterStyle BackColor="Black" ForeColor="White" />
        <HeaderStyle BackColor="Black" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />

        <Columns>
            <asp:TemplateField HeaderText="Description" ItemStyle-Wrap="true" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <div>
                        <asp:Label runat="server" Text='<%# Eval("MacroArea") %>' CssClass="trLabel" />
                    </div>
                    <div>
                        <small>
                            <asp:Literal runat="server" Text='<%# Eval("descrizione") %>' />
                        </small>
                    </div>
                    <div>
                        <ruby>
                            <asp:Literal ID="lblData" runat="server" Text='<%# RationesCurare.GB.ObjectToDateTimeString(Eval("GiornoDelMese")) + " - " + Eval("Periodo_H") %>'></asp:Literal>
                        </ruby>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount" ItemStyle-Wrap="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" FooterStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="lblsoldi" runat="server" CssClass='<%# RationesCurare.GB.GetColor(Eval("Soldi")) %>' Text='<%# Eval("Soldi", "{0:c}") %>' />
                </ItemTemplate>                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button runat="server" CssClass="myBtn googleIcon" ToolTip="New recurring transaction" Text="update" ID="bNuovo" OnClick="bNuovo_Click" />
</asp:Content>
