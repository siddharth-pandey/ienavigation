define("hlcComponent", ["jquery"], function($) {

    $('div[data-view-type="hubdetails"]').each(function () {
        var hubId = $(this).data('hub-id'),
            url = $(this).data('requestaddress'),
            formId = $(this).parent('form').prop('id');
        GetSummaryView(url, hubId, formId);

    });


    function GetSummaryView(url, hubId, formId) {
        var dataToPost = $('form#' + formId).serialize();
        console.log(dataToPost);


        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            data: dataToPost,
            async: true,
        }).success(function(response, status, xhr) {
            var ct = xhr.getResponseHeader("content-type") || "";
            if (ct.indexOf('html') > -1) {
                // returned result is of type html, so act accordingly
                var detailsViewInstance = $('div[data-hub-id="' + hubId + '"]');
                detailsViewInstance.find('div.loaderwrapp').hide();
                detailsViewInstance.html(response);
            } else if (ct.indexOf('json') > -1) {
                // returned result is of type json, so act accordingly
            }
        }).fail(function(e) {
            console.log(e);
        });
    }

});
