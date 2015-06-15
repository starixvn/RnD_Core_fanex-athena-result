/// <reference path="/@JSense.js" />
/* File Created: September 9, 2014 */
/* exported  outRightReport */
/* global JsonHuman */
var outRightReport = (function () {
    "use strict";
    var me = {};
    /*private variable*/

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
            dateFormat: "mm/dd/yy"
        });

        $("#gen-html").click(function () {
            var criteria = {
                date: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),

                languageCode: $("#language").val()
            };
            $.ajax({
                url: window.rootUrl + "OutRightReport/GenHtml",
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
            var url = window.rootUrl + "OutRightReport/GenExcel?date=" + $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate'));

            url += "&languageCode=" + $("#language").val();
            window.open(url, "_blank");
        });
        $("#gen-json").click(function () {
            $("#report-content").html("");
            var criteria = {
                date: $.datepicker.formatDate('mm/dd/yy', $("#date").datepicker('getDate')),

                languageCode: $("#language").val()
            };
            $.ajax({
                url: window.rootUrl + "OutRightReport/GenJson",
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
    };
    me.setup = function () {
        initEvent();
    };

    return me;
})();