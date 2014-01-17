<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Parlay.aspx.cs" Inherits="Fanex.Result.WebFormTest.Parlay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Fanex.Result/fanex.result.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ol class="round">
        <li class="one">Licensee:12Bet, User=pgmson</li>
        <li class="one">Type:
            <select id="type">
                <option value="0">NumberGame </option>
                <option value="1">Parlay</option>
                <option value="2">Other </option>
            </select>
            RefNo:
            <input type="text" id="RefNo" />
            MatchId
            <input type="text" id="MatchId" />
            RaceNo
            <input type="text" id="RaceNo" />
        </li>
        <li>Sport:<input type="text" value="1000" id="sport" />
        </li>
        <li>
            <input type="button" value="submit" id="submit" />
        </li>
        <li>View By:
            <select id="viewby">
                <option value="Table">Table </option>
                <option value="Json">Json </option>
            </select>
        </li>
    </ol>
    <div id="result">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

            $("input[type=button], button")
      .button()
      .click(function (event) {
          event.preventDefault();
      });
            $("#submit").click(function () {
                var refno = $("#RefNo").val();
                var matchid = $("#MatchId").val();
                var raceno = $("#RaceNo").val();
                var type = $("#type").val();

                getData(refno, matchid, raceno, type);

            });
            $("#viewby").change(function () {
                var refno = $("#RefNo").val();
                var matchid = $("#MatchId").val();
                var raceno = $("#RaceNo").val();
                var type = $("#type").val();

                getData(refno, matchid, raceno, type);
            });
        });
        function getData(refno, matchId, raceno, type) {
            var viewBy = $("#viewby").val();
            var url = "GetPopUpData.ashx?type="
                        + type + "&refno=" + refno + "&matchid="
                        + matchId + "&raceno=" + raceno + "&sport=" + $("#sport").val() + "&by=" + viewBy;

            if (viewBy == "Excel" || viewBy == "Json") {

                window.open(url);
            } else {
                $.ajax({
                    url: url,
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