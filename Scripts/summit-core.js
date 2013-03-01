$(document).ready(function () {
    $('.terms').on('click', 'li a', function (e) {
        var url = $(this).attr('href');

        if (url.indexOf("?") == -1) {
            url += "?ajax=true";
        } else {
            url += "&ajax=true";
        }

        $('.widget-homepage-destinations, .widget-taxonomy-menu').fadeOut(3000, function() {
            $.ajax({
                type: 'GET',
                url: url,
                success: function (data) {
                    $('.zone-content').replaceWith(data);
                }
            });
        });

        return false;
    });
});