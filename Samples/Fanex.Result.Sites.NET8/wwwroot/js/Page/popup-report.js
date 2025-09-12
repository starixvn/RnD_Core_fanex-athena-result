/// <reference path="/@JSense.js" />
/* File Created: September 9, 2014 */
/* exported  popupReport */
var popupReport = (function () {
    "use strict";
    var me = {};
    /*private variable*/
    var renderSport = function (licenseeid, date, getDataFromExcel) {
        invokeLoadingMsg(true);

        $.getJSON(
            window.rootUrl + "normalreport/GetSports?EventDate=" + date + "&licenseeid=" + licenseeid + "&getDataFromExcel=" + getDataFromExcel,
            function (data) {
                var options = "";
                if (data !== undefined && data !== null && data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        options += "<option value=\"" + data[i].Id + "\">" + data[i].Name + "</option>";
                    }
                }
                options += "<option value=\"255\">GoldDeluxe</option>";
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
                    renderSport(1, dateText, $("#get-data-from-excel").is(":checked"));
                }
            }).datepicker("setDate", new Date());;

        $("#gen-html").click(function () {
            var criteria = {
                WinLossDate: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),
                SportType: $("#sport").val(),
                IsOutRight: $("#IsOutRight").is(":checked"),
                RefNo: $("#refno").val(),
                RaceNo: $("#raceno").val(),
                Bettype: $("#bettype").val(),
                MatchId: $("#matchid").val(),
                languageCode: $("#language").val(),
                Betid: $("#betid").val(),
                Handicap2: $("#handicap2").val(),
                TransDesc: $("#transdesc").val(),
                IncludeTestData: $("#IncludeTestData").is(":checked"),
                GetDataFromExcel: $("#get-data-from-excel").is(":checked")
            };
            genData(criteria);
        });

        $('#get-data-from-excel').change(function () {
            var getDataFromExcel = $("#get-data-from-excel").is(":checked");

            if (getDataFromExcel) {
                renderSport(1, "07/23/2017", getDataFromExcel);
            }
        });
        $("#export-excel-file").click(function () {
            var sportId = $("#sport").val()
            window.open(window.rootUrl + "normalreport/ExportExcelDataFile?sportId=" + sportId, "_blank");
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
