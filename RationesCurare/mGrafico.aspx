<%@ Page Title="RationesCurare - Grafico a barre" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mGrafico.aspx.cs" Inherits="RationesCurare.mGrafico" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Grafico a barre</h2>

    <nav>
        <a id="bGraficoM" href="mGrafico.aspx?T=M" runat="server" style="font-family: 'Ubuntu Mono'; font-size: initial">Mensile</a> |		    
        <a id="bGraficoY" href="mGrafico.aspx?T=Y" runat="server" style="font-family: 'Ubuntu Mono'; font-size: initial">Annuale</a>
    </nav>

    <asp:Chart ID="Chart1" runat="server" ImageStorageMode="UseImageLocation" ImageLocation="~/public/ChartImages/chartStatComp_#SEQ(100,10)" OnPrePaint="Chart1_PrePaint" SuppressExceptions="True" Width="745px" Height="500px" CssClass="img-max-size">
        <Series>
            <asp:Series Name="Series1" YValueMembers="Soldini_TOT" XValueMember="Mese" Color="Green" />
        </Series>

        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisY LabelAutoFitStyle="IncreaseFont, DecreaseFont" />
                <AxisX IntervalAutoMode="VariableCount" IsLabelAutoFit="False" LabelAutoFitStyle="IncreaseFont, DecreaseFont" />
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    <script>
        let c = document.getElementById("<%=Chart1.ClientID %>");

        if (c != null)
            c.style.height = 'auto';
    </script>
</asp:Content>
