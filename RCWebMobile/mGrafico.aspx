<%@ Page Title="RC WEB - Grafico" Language="C#" MasterPageFile="~/RC.Master" AutoEventWireup="true" CodeBehind="mGrafico.aspx.cs" Inherits="RCWebMobile.mGrafico" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        function setCookie(c_name, value, exdays)
        {
            try
            {
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + exdays);

                var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
                document.cookie = c_name + "=" + c_value;
            }
            catch (e)
            {
                //error
            }
        }        
        
        function SetSizee()
        {
            var myWidth = 0;
            var myHeight = 0;

            if (typeof (window.innerWidth) == 'number')
            {
                //Non-IE 
                myWidth = window.innerWidth;
                myHeight = window.innerHeight;
            }
            else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight))
            {
                //IE 6+ in 'standards compliant mode' 
                myWidth = document.documentElement.clientWidth;
                myHeight = document.documentElement.clientHeight;
            }
            else if (document.body && (document.body.clientWidth || document.body.clientHeight))
            {
                //IE 4 compatible 
                myWidth = document.body.clientWidth;
                myHeight = document.body.clientHeight;
            }

            setCookie("HiddenField_ChartSizeW", myWidth, 5);
            setCookie("HiddenField_ChartSizeH", myHeight, 5);

            var c = document.getElementById("<%=Chart1.ClientID %>");

            if (c != null)
            {
                if (myWidth > 0)
                    c.style.width = myWidth - 10 + 'px';

                if (myHeight > 0)
                    c.style.height = myHeight - 80 + 'px';                
            }
        }

        window.onload = SetSizee;
        window.onresize = SetSizee;
    </script>

    <table>
        <tr>
            <td>
                <a href='mGrafico.aspx?T=M'><img src="images/calendarMOnth.png" title="Home" />Mensile</a>
            </td>
            <td>
                <a href='mGrafico.aspx?T=Y'><img src="images/calendarYear.png" title="Home" />Annuale</a>
            </td>
        </tr>
    </table>

    <asp:Chart ID="Chart1" runat="server" ImageStorageMode="UseImageLocation" ImageLocation="~/public/ChartImages/chartStatComp_#SEQ(100,10)" OnPrePaint="Chart1_PrePaint" SuppressExceptions="True">
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

</asp:Content>