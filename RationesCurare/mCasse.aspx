﻿<%@ Page Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mCasse.aspx.cs" Inherits="RationesCurare.mCasse" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gestione account</title>

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
    <h2>Gestione account</h2>    

    <asp:GridView ID="GridView1" runat="server" GridLines="None" AutoGenerateColumns="False" DataKeyNames="Nome" Width="100%" RowStyle-CssClass="trGrid"
        OnSelectedIndexChanging="GridView1_SelectedIndexChanging" HeaderStyle-HorizontalAlign="Left"
        OnRowDataBound="GridView1_RowDataBound">
       
        <HeaderStyle BackColor="Black" ForeColor="White" />

        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="NascondiS" HeaderText="Nascondi" />
        </Columns>

        <RowStyle CssClass="trGrid"></RowStyle>

    </asp:GridView>

    <asp:Button runat="server" CssClass="myBtn googleIcon" ToolTip="Nuovo account" Text="account_balance_wallet" ID="bNuovo" OnClick="bNuovo_Click" />
</asp:Content>
