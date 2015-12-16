/* File Created: December 16, 2015 */

"use strict";
var cockFightingResult = function (resultApi) {
    var me = this;
    var _ajaxRequest = null;
    var _intervalId = 0;
    var _resultApi = resultApi;
    var extendSession = function () {
        _intervalId = setInterval(function () {
            var option = {
                cache: false,
                url: _resultApi + "?IsKeepAlive=" + true,
            };
            if (_ajaxRequest != null) {
                _ajaxRequest.abort();
            }
            _ajaxRequest = $.ajax(option);
        }, 6000);
    };
    me.Open = function () {
        var option = {
            type: "GET",
            cache: false,
            url: _resultApi,
            success: function (response) {
                clearInterval(_intervalId);
                if (response.IsSuccess) {
                  
                    var cockFightingWindow = window.open(response.Url, '_blank', 'left=20,top=20,width=950,height=650,toolbar=1,resizable=1,scrollbars,status=1');
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
        if (_ajaxRequest != null) {
            _ajaxRequest.abort();
        }
        _ajaxRequest = $.ajax(option);
    };
};
