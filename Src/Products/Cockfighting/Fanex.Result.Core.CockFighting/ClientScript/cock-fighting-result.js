/*global $,clearInterval,setInterval,window,alert*/
/* File Created: December 16, 2015 */

var cockFightingResult = function (resultApiUrl) {
    "use strict";
    var me = this, ajaxRequest = null, intervalId = 0, resultApi = resultApiUrl;

    function extendSession() {
        intervalId = setInterval(function () {
            var option = {
                cache: false,
                url: resultApi + "?IsKeepAlive=" + true
            };
            if (ajaxRequest !== null) {
                ajaxRequest.abort();
            }
            ajaxRequest = $.ajax(option);
        }, 6000);
    }

    me.Open = function () {
        var option = {
            type: "GET",
            cache: false,
            url: resultApi,
            success: function (response) {
                clearInterval(intervalId);
                if (response.IsSuccess) {
                    window.open(response.Url,
                        '_blank',
                        'left=20,top=20,width=950,height=650' +
                        'toolbar=1,resizable=1,scrollbars,status=1');
                    extendSession();
                } else {
                    alert("Could not open Cock Fighting Result Page!");
                }
            },
            beforeSend: function () {
                //place your block UI
            },
            complete: function () {
                //place your block UI
            }
        };
        if (ajaxRequest !== null) {
            ajaxRequest.abort();
        }
        ajaxRequest = $.ajax(option);
    };
};