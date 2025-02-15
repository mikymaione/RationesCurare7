<%@ Page Title="RationesCurare - Pie chart" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mGraficoTorta.aspx.cs" Inherits="RationesCurare.mGraficoTorta" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Pie chart</h2>

    <style>
        body {
            margin-top: -16px;
        }

        div {
            margin-bottom: 1rem;       
        }       

        .buttons {
            display: flex;
            gap: 0.5em;
        }

        .footer {
            color: white;
            background-color: black;
            text-align: right;
        }
    </style>

    <div>
        <label class="required" for="idDataDa">From</label>                
        <input id="idDataDa" runat="server" type="date" required>
    </div>
    <div>
        <label class="required" for="idDataA">Until</label>                
        <input id="idDataA" runat="server" type="date" required>
    </div>

    <div class="buttons">
        <asp:Button ID="bPrev" runat="server" CssClass="googleIcon" Text="skip_previous" OnClick="bPrev_Click" ToolTip="Previous month" />
        <asp:Button ID="bNext" runat="server" CssClass="googleIcon" Text="skip_next" OnClick="bNext_Click" ToolTip="Next month" />
        <asp:Button ID="bCerca" runat="server" CssClass="googleIcon" Text="query_stats" OnClick="bCerca_Click" ToolTip="Search" />
    </div>

    <asp:Chart ID="Chart1" runat="server" 
        Palette="None"
        ImageStorageMode="UseHttpHandler"
        SuppressExceptions="True" BackColor="Transparent"
        AntiAliasing="Graphics" TextAntiAliasingQuality="High"
        Width="745px" Height="500px" CssClass="img-max-size">

        <Series>
            <asp:Series Name="Series1" YValueMembers="Soldini_TOT" XValueMember="MacroArea" ChartType="Pie" Legend="Legend1" />                            
        </Series>

        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BackColor="Transparent" />            
        </ChartAreas>

        <Legends>
            <asp:Legend Name="Legend1" BackColor="Transparent" ForeColor="#F79E10" TitleForeColor="#F79E10" />
        </Legends>
    </asp:Chart>

    <div class="footer">
        <asp:Label ID="lTotale" runat="server"></asp:Label>
    </div>

    <script>
        let c = document.getElementById("<%=Chart1.ClientID %>");

        if (c != null) {
            c.style.height = 'auto';
        }
    </script>
</asp:Content>
