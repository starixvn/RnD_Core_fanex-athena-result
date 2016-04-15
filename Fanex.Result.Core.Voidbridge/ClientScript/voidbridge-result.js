"use strict";
var voidbridgeResult = function (resultApi) {
    var me = this;
    var _ajaxRequest = null;
    var _resultApi = resultApi;

    me.Open = function () {
        var option = {
            type: "GET",
            cache: false,
            url: _resultApi,
            success: function (response) {
                if (response.IsSuccess) {
                    var voidbridgeWindow = window.open(response.Url, '_blank', 'left=20,top=20,width=950,height=650,toolbar=1,resizable=1,scrollbars,status=1');
                } else {
                    alert("Could not open Voidbridge Result Page!");
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