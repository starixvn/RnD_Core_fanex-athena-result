<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OutRight.aspx.cs" Inherits="Fanex.Result.WebFormTest.OutRight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Fanex.Result/fanex.result.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="round">
        <li class="one">Date
            <input type="text" id="date" />
            <input type="text" id="tDate" />
            &nbsp;<input type="button" value="submit" id="submit" />
        </li>
        <li>View By:
            <select id="viewby">
                <option value="Table">Table </option>
                <option value="Json">Json </option>
                <option value="Excel">Excel </option>
            </select>
        </li>
    </ol>
    <div id="result">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#date,#tDate").datepicker();
            $("input[type=button], button")
      .button()
      .click(function (event) {
          event.preventDefault();
      });
            $("#submit").click(function () {
                GetData($("#date").val(), $("#tDate").val(), 0, 0);

            });

            $("#viewby").change(function () {
                var date = $("#date").val();
                var tDate = $("#tDate").val();
                GetData(date, tDate, 0, 0);
            });
        });
        function GetData(datetime, tdatetime, sportid, leagueId) {

            if (date == null || date.length == 0) alert("pls select date");
            var viewBy = $("#viewby").val();
            if (viewBy == "Excel" || viewBy == "Json") {
                window.open("GetData.ashx?dateTime=" + datetime + "&sportId=" + sportid + "&leagueId="
                + leagueId + "&by=" + viewBy + "&isoutright=1&tdatetime=" + tdatetime, "_blank");
            }
            else {
                $.ajax({
                    url: "GetData.ashx?dateTime=" + datetime + "&sportId=" + sportid
                + "&leagueId=" + leagueId + "&by=" + viewBy + "&isoutright=1&tdatetime=" + tdatetime,
                    type: "GET",
                    async: false,
                    success: function (response) {

                        $("#result").html(response);

                    },
                    error: function (error) {
                        console.log(error);
                        //alert(error);
                    }
                });
            }
        }
    </script>
</asp:Content>