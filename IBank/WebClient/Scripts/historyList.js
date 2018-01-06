$(document).ready(function() {
    $('.history-list-wrapper .ibTable').find('tr').click(function() {
        $('.history-list-wrapper .ibTable').find('tr').removeClass("lineChecked");
        $(this).addClass('lineChecked');
    });

    $('#details').click(function (event) {
        if ($('.history-list-wrapper .ibTable').find('.lineChecked').length) {
            var $line = $('.history-list-wrapper .ibTable').find('.lineChecked').first();
            var trId = $line.find('input[name="item.TransactionId"]').val();
            var trType = $line.find('input[name="item.Type"]').val();
            $('#id').val(trId);
            $('#type').val(trType);
        } else {
            alert("Выберите платеж");
            event.preventDefault();
        }
    });
});