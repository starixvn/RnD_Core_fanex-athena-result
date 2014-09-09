/* File Created: September 9, 2014 */
/* exported  normalReport */
var normalReport = (function () {
    "use strict";
    var me = {};

    var renderSport = function (licenseeid, date) {
        invokeLoadingMsg(true);

        $.getJSON(window.rootUrl + "normalreport/GetSports?EventDate=" + date + "&licenseeid=" + licenseeid, function (data) {
            var options = "";
            if (data !== undefined && data !== null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    options += "<option value=\"" + data[i].Id + "\">" + data[i].Name + "</option>";
                }
                var firstSport = data[0].Id;
                if (firstSport !== undefined && firstSport !== null && firstSport > 0) {
                    renderLeague(data[0].Id, date);
                }
                $("#sport").html(options);
                invokeLoadingMsg(false);
            }
        });
    };
    var renderLeague = function (sportId, date) {
        invokeLoadingMsg(true);
        $.getJSON(window.rootUrl + "normalreport/GetLeagues?EventDate=" + date + "&sporttype=" + sportId + "&isShowAll=true", function (data) {
            var options = "";
            if (data !== undefined && data !== null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    options += "<option value=\"" + data[i].Id + "\">" + data[i].Name + "</option>";
                }
            }

            $("#league").html(options);
            invokeLoadingMsg(false);
        });
    };
    var invokeLoadingMsg = function (visible) {
        if (visible) {
            if ($("#loadingMsgBox").is(":hidden")) {
                $("#loadingMsgBox").removeClass("hidden");
            }
        }
        else {
            $("#loadingMsgBox").addClass("hidden");
        }
    };
    var initEvent = function () {
        $("#date").datepicker(
        {
            dateFormat: "mm/dd/yy",
            onSelect: function (dateText) {
                renderSport(1, dateText);
            }
        });
        $("#sport").change(function () {
            var sportId = $(this).val();
            renderLeague(sportId, $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')));
        });
        $("#gen-html").click(function () {
            var criteria = {
                date: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                sportId: $("#sport").val(),
                leagueId: $("#league").val(),
                languageCode: $("#language").val()
            };
            $.ajax({
                url: window.rootUrl + "normalreport/Index",
                type: "POST",
                data: criteria,
                datatype: "html",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                beforeSend: function () {
                    invokeLoadingMsg(true);
                },
                success: function (data) {
                    $("#report-content").html(data);
                    invokeLoadingMsg(false);
                },
                error: function () {
                    invokeLoadingMsg(false);
                    alert("Error requesting page");
                }
            });
        });
    };
    me.setup = function () {
        initEvent();
    };
    return me;
})();