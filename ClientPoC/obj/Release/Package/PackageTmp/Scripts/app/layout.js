(function (window, undefined) {
    //History.Adapter.bind(window, 'statechange', function () {
    //    var state = History.getState();

    //    if (state.data.options == undefined) {
    //        alert("options is undefined");
    //        var options = {
    //            url: state.url,
    //            type: "GET",
    //            dataType: "html"
    //        };
    //        GetPartial(options);
    //    } else {
    //        GetPartial(state.data.options);
    //    }

    //});

})(window);


$(document).on('click', 'a', function (e) {
    e.preventDefault();

    var options = {
        url: $(this).prop('href'),
        type: "GET",
        dataType: "html"
    };

    if ($(this).text() == "Log Out") {
        window.location.href = options.url;
    }

    History.pushState({ options: options }, $(this).text(), $(this).prop('href'));
});

function Partial(options) {
    if (options) {

        $.ajax({
            url: options.url,
            type: options.type || 'GET',
            dataType: options.datatype || 'html',
            async: false,
            data: options.dataToPost,
            beforeSend: function () {
                $("#loaderMain").show();
            },
            complete: function () {
                $("#loaderMain").hide();
            },
            cache: false,
        }).success(function (response, status, xhr) {
            var ct = xhr.getResponseHeader("content-type") || "";
            if (ct.indexOf('html') > -1) {
                // returned result is of type html, so act accordingly
                if (options.selector == undefined) {
                    InjectContentToShell(response);
                } else {
                    $(options.selector).empty().append(response);
                }
            } else if (ct.indexOf('json') > -1) {
                // returned result is of type json, so act accordingly
            }
        }).fail(function (e) {
            console.log(e);
        });
    }
}

function GetPartial(options) {

    if (options) {

        $.ajax({
            url: options.url,
            type: options.type || 'GET',
            dataType: options.datatype || 'html',
            async: true,
            data: options.dataToPost,
            beforeSend: function () {
                $("#loaderMain").show();
            },
            complete: function () {
                $("#loaderMain").hide();
            },
            cache: false,
        }).success(function (response, status, xhr) {
            var ct = xhr.getResponseHeader("content-type") || "";
            if (ct.indexOf('html') > -1) {
                // returned result is of type html, so act accordingly
                if (options.selector == undefined) {
                    InjectContentToShell(response);
                } else {
                    $(options.selector).empty().append(response);
                }
            } else if (ct.indexOf('json') > -1) {
                // returned result is of type json, so act accordingly
            }
        }).fail(function (e) {
            console.log(e);
        });
    }
}


function InjectContentToShell(content) {
    $('#shell').fadeOut(50, function () {
        $(this).empty().append(content);
    }).fadeIn(30);
}

// Avoid `console` errors in browsers that lack a console.
(function () {
    var method;
    var noop = function () { };
    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeStamp', 'trace', 'warn'
    ];
    var length = methods.length;
    var console = (window.console = window.console || {});

    while (length--) {
        method = methods[length];

        // Only stub undefined methods.
        if (!console[method]) {
            console[method] = noop;
        }
    }
}());