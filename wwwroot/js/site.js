// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    var $dates = $(".js-hook-datetimepicker");

    //console.log($dates);

    if ($dates.length) {
        $dates.each(function (i, id) {
            let $elem = $(this);

            //console.log($elem);

            let value = $elem.val() || new Date();

            console.log(value);

            $elem.datetimepicker({
                defaultDate: new Date(value),
                format: 'm/d/Y H:i a',
            });
        });
    }
 
});
