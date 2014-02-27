define(["jquery"], function () {
    return {
        init: function () {
            console.log("INIT");
            $(document).bind("mobileinit", function () {
                $.mobile.pushStateEnabled = false;
            });
        }
    };
});