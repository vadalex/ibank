var ddlki = $('.ddl-header');
ddlki.click(function () {

    if ($(this).parent().find('.dropped').css('display') == 'none') {
        $(this).parent().find('.dropped').slideDown();
    } else {
        $(this).parent().find('.dropped').slideUp();
    }

});