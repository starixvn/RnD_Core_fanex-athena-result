<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Normal.aspx.cs" Inherits="Fanex.Result.WebFormTest.Normal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Fanex.Result/fanex.result.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="round">
        <li class="one">Date
            <input type="text" id="date" />
            &nbsp;<input type="button" value="submit" id="submit" />
        </li>
        <li class="two">Sport:
            <select id="sports">
                <option value="0">None </option>
            </select>
            League:
            <select id="league">
                <option value="0">Show All </option>
            </select>
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
            $("#date").datepicker();
            $("input[type=button], button")
      .button()
      .click(function (event) {
          event.preventDefault();
      });
            $("#submit").click(function () {
                var date = $("#date").val();
                if (date == null || date.length == 0) alert("pls select date");
                $.ajax({
                    url: "GetFilter.ashx?dateTime=" + $("#date").val() + "&sportid=0",
                    type: "GET",
                    async: false,
                    success: function (response) {
                        $("#sports").html('<option value="0"> Select</option>');
                        $.each(response.Sport, function (index, value) {
                            $("#sports").append('<option value="' + value.sporttype + '">' + value.sportname + '</option>');
                        });
                        $("#league").html('<option value="0">Select </option>');
                        $.each(response.League, function (index, value) {
                            $("#league").append('<option data-sportype="' + value.sporttype + '" data-eventdate="' + value.eventdate + '" value="' + value.leagueId + '">' + value.leaguename + '</option>');

                        });
                    },
                    error: function (error) {
                        console.log(error);
                        //alert(error);
                    }
                });
            });
            $("#sports").change(function () {
                var date = $("#date").val();
                var sportId = $(this).val();

                if (date == null || date.length == 0) alert("fuck date");
                $.ajax({
                    url: "GetFilter.ashx?dateTime=" + date + "&sportId=" + sportId + "&leagueId=0",
                    type: "GET",
                    async: false,
                    success: function (response) {
                        $("#league").html('<option value="0">FUCK ALL</option>');
                        $.each(response.League, function (index, value) {
                            $("#league").append('<option data-sportype="' + value.sporttype + '" data-eventdate="' + value.eventdate + '" value="' + value.leagueId + '">' + value.leaguename + '</option>');
                        });
                        GetData(date, sportId, 0);
                    },
                    error: function (error) {
                        console.log(error);
                        //alert(error);
                    }
                });

            });
            $("#league").change(function () {
                var date = $("#date").val();
                var sportId = $("#sports").val();
                var leagueId = $(this).val();
                GetData(date, sportId, leagueId);
            });
            $("#viewby").change(function () {
                var date = $("#date").val();
                var sportId = $("#sports").val();
                var leagueId = $("#league").val();
                GetData(date, sportId, leagueId);
            });
        });
        function GetData(datetime, sportid, leagueId) {

            if (date == null || date.length == 0) alert("pls select date");
            var viewBy = $("#viewby").val();
            if (viewBy == "Excel" || viewBy == "Json") {
                window.open("GetData.ashx?dateTime=" + datetime + "&sportId=" + sportid + "&leagueId=" + leagueId + "&by=" + viewBy, "_blank");
            }
            else {
                $.ajax({
                    url: "GetData.ashx?dateTime=" + datetime + "&sportId=" + sportid
                + "&leagueId=" + leagueId + "&by=" + viewBy,
                    type: "GET",
                    async: false,
                    success: function (response) {
                        $("#result").html("");

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