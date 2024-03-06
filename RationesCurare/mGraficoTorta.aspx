<%@ Page Title="RationesCurare - Grafico a torta" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mGraficoTorta.aspx.cs" Inherits="RationesCurare.mGraficoTorta" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Grafico a torta</h2>

    <div>        
        <style>
            .noWidth {
                width: auto !important;
            }
        </style>

        <label class="required" for="idData">Data</label>
        <input class="noWidth" id="idData" runat="server" type="month" size="7" required>

        <asp:Button ID="bPrev" runat="server" Text="⇦" OnClick="bPrev_Click" ToolTip="Mese precedente" />
        <asp:Button ID="bNext" runat="server" Text="⇨" OnClick="bNext_Click" ToolTip="Mese successivo" />
    </div>

    <asp:Chart ID="Chart1" runat="server" 
        ImageStorageMode="UseImageLocation" ImageLocation="~/public/ChartImages/chartStatComp_#SEQ(100,10)" 
        SuppressExceptions="True" BackColor="Transparent"
        AntiAliasing="Graphics" TextAntiAliasingQuality="High"
        Width="745px" Height="500px" CssClass="img-max-size">

        <Series>
            <asp:Series Name="Series1" YValueMembers="Soldini_TOT" XValueMember="Titolo" ChartType="Pie" Legend="Legend1" />                            
        </Series>

        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BackColor="Transparent" />            
        </ChartAreas>

        <Legends>
            <asp:Legend Name="Legend1" Title="Legenda" />
        </Legends>
    </asp:Chart>

    <script>
        let c = document.getElementById("<%=Chart1.ClientID %>");

        if (c != null) {
            c.style.height = 'auto';
        }
    </script>
</asp:Content>
