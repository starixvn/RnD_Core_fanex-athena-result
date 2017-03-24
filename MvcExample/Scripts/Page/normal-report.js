/// <reference path="/@JSense.js" />
/* File Created: September 9, 2014 */
/* exported  normalReport */
/* global JsonHuman */
var normalReport = (function () {
    "use strict";
    var me = {};
    /*private variable*/
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
            }
            options += "<option value=\"255\">GoldDeluxe</option>";
            $("#sport").html(options);
            invokeLoadingMsg(false);
        });
    };
    var renderLeague = function (sportId, date) {
        invokeLoadingMsg(true);
        $.getJSON(window.rootUrl + "normalreport/GetLeagues?EventDate=" + date + "&sporttype=" + sportId + "&isShowAll=true", function (data) {
            var options = "<option value=\"0\">All</option>";
            if (data !== undefined && data !== null && data.length > 0) {
                if (data !== undefined && data !== null && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        options += "<option value=\"" + data[i].Id + "\">" + data[i].Name + "</option>";
                    }
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
                languageCode: $("#language").val(),
                showmatchcode: $("#showmatchcode").is(":checked")
            };
            $.ajax({
                url: window.rootUrl + "normalreport/GenHtml",
                data: criteria,
                datatype: "html",
                type: "POST",
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

        $("#gen-excel").click(function () {
            var url = window.rootUrl + "normalreport/GenExcel?date=" + $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate'));
            url += "&sportId=" + $("#sport").val();
            url += "&leagueId=" + $("#league").val();
            url += "&languageCode=" + $("#language").val();
            url += "&showmatchcode=" + $("#showmatchcode").is(":checked");
            window.open(url, "_blank");
        });
        $("#gen-json").click(function () {
            $("#report-content").html("");
            var criteria = {
                date: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                sportId: $("#sport").val(),
                leagueId: $("#league").val(),
                languageCode: $("#language").val(),
                showmatchcode: $("#showmatchcode").is(":checked")
            };
            $.ajax({
                url: window.rootUrl + "normalreport/GenJson",
                data: criteria,
                datatype: "html",
                type: "POST",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                beforeSend: function () {
                    invokeLoadingMsg(true);
                },
                success: function (data) {
                    var nodeJson = JsonHuman.format(JSON.parse(data));
                    $("#report-content").html(nodeJson);
                    invokeLoadingMsg(false);
                },
                error: function () {
                    invokeLoadingMsg(false);
                    alert("Error requesting page");
                }
            });
        });
        $("body").on("click", ".detail-link", function () {
            var detailPopUp = $("#game-detail");
            $.ajax({
                url: window.rootUrl + "normalreport/GenHtmlGameDetail?matchid="
                 + $(this).attr("data-match-id") + "&languageCode=" + $("#language").val()
                 + "&showmatchcode=" + $("#showmatchcode").is(":checked") + "&sportId=" + $("#sport").val(),
                datatype: "html",
                type: "GET",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                cache: false,
                beforeSend: function () {
                    invokeLoadingMsg(true);
                },
                success: function (data) {
                    detailPopUp.html(data);
                    detailPopUp.dialog({ width: "60%" });
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