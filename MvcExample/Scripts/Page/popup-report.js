/// <reference path="/@JSense.js" />
/* File Created: September 9, 2014 */
/* exported  popupReport */
var popupReport = (function () {
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
            }
            options += "<option value=\"255\">GoldDeluxe</option>";
            options += "<option value=\"203\">MicroGaming</option>";
            options += "<option value=\"250\">AG Casino</option>";
            options += "<option value=\"211\">All Bet Casino</option>";
            $("#sport").html(options);
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
        $("#sb-mode").click(function () {
            var criteria = {
                WinLossDate: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                SportType: 1,
                IsOutRight: false,
                RefNo: null,
                RaceNo: null,
                Bettype: 1,
                MatchId: 10315089,
                languageCode: $("#language").val()
            };
            genData(criteria);
        });
        $("#parlay-mode").click(function () {
            var criteria = {
                WinLossDate: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                SportType: 9,
                IsOutRight: false,
                RefNo: 9399090000,
                RaceNo: null,
                Bettype: 9,
                MatchId: 1,
                languageCode: $("#language").val()
            };
            genData(criteria);
        });
        $("#racing-mode").click(function () {
            var criteria = {
                WinLossDate: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                SportType: 153,
                IsOutRight: false,
                RefNo: null,
                RaceNo: 1,
                Bettype: 32,
                MatchId: 4493799,
                languageCode: $("#language").val()
            };

            genData(criteria);
        });
        $("#date").datepicker(
        {
            dateFormat: "mm/dd/yy",
            onSelect: function (dateText) {
                renderSport(1, dateText);
            }
        });

        $("#gen-html").click(function () {
            var criteria = {
                WinLossDate: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                SportType: $("#sport").val(),
                IsOutRight: $("#IsOutRight").is(":checkbox"),
                RefNo: $("#refno").val(),
                RaceNo: $("#raceno").val(),
                Bettype: $("#bettype").val(),
                MatchId: $("#matchid").val(),
                languageCode: $("#language").val(),
                Betid: $("#betid").val()
            };
            genData(criteria);
        });
    };
    var genData = function (criteria) {
        $.ajax({
            url: window.rootUrl + "PopUpReport/GenHtml",
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
    };
    me.setup = function () {
        initEvent();
    };

    return me;
})();