$(document).ready(function () {
    $('.terms').on('click', 'li a', function (e) {
        var url = $(this).attr('href');

        if (url.indexOf("?") == -1) {
            url += "?ajax=true";
        } else {
            url += "&ajax=true";
        }

        $.ajax({
            type: 'GET',
            url: url,
            success: function (data) {
                $('.widget-homepage-destinations').fadeOut(300);
                $('.widget-taxonomy-menu').slideUp(200);
                $('.zone-content').replaceWith(data);
                //                $('html,body').animate({ scrollTop: $('.wellbeing-category .recent-articles').offset().top }, 'slow');
            }
        });

        return false;
    });
});