function show(id, url) {
    $(id).fadeOut('fast', function () {
        $(id).load(url, function () {
            $(id).fadeIn('slow');
        });
    });
}