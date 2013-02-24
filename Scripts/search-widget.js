

$('.search-widget').autocomplete(
{
    source: function (request, response) {
        $.getJSON('/search/' + request.term, null,
            function (data) {
                response($.map(data, function (item) {
                    return { label: item.Title, value: item.Path };
                }));
            });
    },
    select: function (event, ui) {
        //updateAutoCompleteValue(this, ui);
        return false;
    },
    focus: function (event, ui) {
        //  updateAutoCompleteValue(this, ui);
        return false;
    },
    minLength: 1,
    delay: 100
});