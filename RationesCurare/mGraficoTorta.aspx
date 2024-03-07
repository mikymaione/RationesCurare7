<%@ Page Title="RationesCurare - Grafico a torta" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mGraficoTorta.aspx.cs" Inherits="RationesCurare.mGraficoTorta" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Grafico a torta</h2>

    <style>
        div {
            margin-bottom: 1rem;       
        }       

        .buttons {
            display: flex;
            gap: 0.5em;
        }
    </style>

    <div>
        <label class="required" for="idDataDa">Da</label>                
        <input id="idDataDa" runat="server" type="date" required>
    </div>
    <div>
        <label class="required" for="idDataA">A</label>                
        <input id="idDataA" runat="server" type="date" required>
    </div>

    <div class="buttons">
        <asp:Button ID="bPrev" runat="server" CssClass="googleIcon" Text="skip_previous" OnClick="bPrev_Click" ToolTip="Mese precedente" />
        <asp:Button ID="bNext" runat="server" CssClass="googleIcon" Text="skip_next" OnClick="bNext_Click" ToolTip="Mese successivo" />
        <asp:Button ID="bCerca" runat="server" Text="Cerca" OnClick="bCerca_Click" />
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
