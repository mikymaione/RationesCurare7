<%@ Page Title="RationesCurare - Bar chart" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mGrafico.aspx.cs" Inherits="RationesCurare.mGrafico" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Bar chart</h2>

    <style>
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
        <asp:Button ID="bType" runat="server" CssClass="googleIcon" Text="date_range" ToolTip="Monthly" OnClick="bType_Click" />
        <asp:Button ID="bCerca" runat="server" CssClass="googleIcon" Text="query_stats" ToolTip="Search" OnClick="bCerca_Click" />
    </div>

    <asp:Chart ID="Chart1" runat="server" 
        ImageStorageMode="UseHttpHandler"
        OnPrePaint="Chart1_PrePaint" SuppressExceptions="True" BackColor="Transparent"
        AntiAliasing="Graphics" TextAntiAliasingQuality="High"
        Width="745px" Height="500px" CssClass="img-max-size">

        <Series>
            <asp:Series Name="Series1" YValueMembers="Soldini_TOT" XValueMember="Mese" />
        </Series>

        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                <AxisY LabelAutoFitStyle="IncreaseFont, DecreaseFont">
                    <LabelStyle ForeColor="#F79E10" Format="{0:C0}" />
                </AxisY>
                <AxisX IntervalAutoMode="VariableCount" IsLabelAutoFit="False" LabelAutoFitStyle="IncreaseFont, DecreaseFont">
                    <LabelStyle ForeColor="#F79E10" />
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
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
