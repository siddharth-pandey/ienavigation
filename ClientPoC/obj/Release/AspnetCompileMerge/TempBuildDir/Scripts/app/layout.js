
alert("layout");
(function (window, undefined) {

    History.Adapter.bind(window, 'statechange', function () {
        var state = History.getState();
        if (state.data.options == undefined) {
            var options = {
                url: state.hash,
                type: "GET",
                dataType: "html"
            };
            GetPartial(options);
        } else {
            GetPartial(state.data.options);
        }

        History.log(state);
    });

})(window);

$(document).on('click', 'a', function (e) {
    e.preventDefault();

    var options = {
        url: $(this).prop('href'),
        type: "GET",
        dataType: "html"
    };

    History.pushState({ options: options }, $(this).text(), $(this).prop('href'));
});


function GetPartial(options) {
    console.log(options);
    $.ajax({
        url: options.url,
        type: options.type,
        dataType: options.datatype,
        async: true,
        data: options.dataToPost,
        beforeSend: function() {
            $("#loaderMain").show();
        },
        complete: function() {
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

function InjectContentToShell(content) {
    $('#shell').fadeOut(100, function () {
        $(this).empty().append(content);
    }).fadeIn(100);
}

