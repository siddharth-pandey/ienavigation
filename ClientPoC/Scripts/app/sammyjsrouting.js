function InjectContentToShell(content) {
    $('#shell').fadeOut(50, function () {
        $(this).empty().append(content);
    }).fadeIn(30);
}


$(document).ready(function () {

    var app = $.sammy('#shell', function () {
        var content = null;
        
        
        function partial(options, context) {
            var cxt = context;

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
                    cache: false
                }).success(function (response, status, xhr) {
                    var ct = xhr.getResponseHeader("content-type") || "";
                    if (ct.indexOf('html') > -1) {
                        // returned result is of type html, so act accordingly
                        if (options.selector == undefined) {
                            if (response != null) {
                                if (options.redirectRoute != undefined) {
                                    content = response;
                                    cxt.redirect(options.redirectRoute);
                                } else {
                                    InjectContentToShell(response);
                                }
                            }
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

        this.get('#/', function (context) {

            document.title = Math.random().toString();
            var options = {
                url: '/Account/Login',
                type: 'GET'
            };

            partial(options, context);
        });

        this.get('#/home', function (context) {
            if (content == null) {
                var options = {
                    url: '/Home/Index',
                    type: 'GET'
                };
                partial(options, context);
            } else {
                InjectContentToShell(content);
                content = null;
            }
        });



        this.get('#/details/:reference/:width/:height', function (context) {

            if (content == null) {
                var options = {
                    url: '/Home/Details?detailViewReference=' + this.params['reference'] + "&width=" + this.params['width'] + "&height=" + this.params['height'],
                    type: 'GET'
                };

                partial(options, context);
            } else {
                InjectContentToShell(content);
                content = null;
            }
        });


        this.post('#/login', function (context) {

            var options = {
                url: '/Account/Login',
                type: 'POST',
                dataToPost: $('#loginform').serialize(),
                redirectRoute: '#/home'
            };
            partial(options, context);
        });

        this.post('#/summaryview', function (context) {
	console.log(“summaryview”);
            var summaryViewReference = this.params['SummaryViewReference'],
                formId = summaryViewReference.replace('/', ''),
                //detailsViewReference = instance.find('#DetailViewId').val(),
                layoutWidth = this.params['Width'],
                layoutHeight = this.params['Height'],
                detailViewId = this.params['DetailViewId'].replace('/', 'P');

            var dataToPost = $('form#' + formId).serialize();

            var options = {
                url: '/Home/Details',
                type: "POST",
                dataType: "html",
                dataToPost: dataToPost,
                redirectRoute: '#/details/' + detailViewId + "/" + layoutWidth + "/" + layoutHeight
            };

            partial(options, context);
        });

    });


    $(function () {
        app.run('#/');
    });


});

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
