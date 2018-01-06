$(document).ready(function() {
    
    $.datepicker.regional['ru'] = {
        closeText: 'Закрыть',
        prevText: '&#x3c;Пред',
        nextText: 'След&#x3e;',
        currentText: 'Сегодня',
        monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь',
        'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
        monthNamesShort: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн',
        'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
        dayNames: ['воскресенье', 'понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота'],
        dayNamesShort: ['вск', 'пнд', 'втр', 'срд', 'чтв', 'птн', 'сбт'],
        dayNamesMin: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
        dateFormat: 'dd.mm.yy',
        firstDay: 1,
        isRTL: false
    };
    $.datepicker.setDefaults($.datepicker.regional['ru']);

    $("#datepicker-from").datepicker({
        showOn: "button",
        buttonImage: "/Content/images/calendar.gif",
        buttonImageOnly: true,
        buttonText: "Select date"
    });

    $("#datepicker-to").datepicker({
        showOn: "button",
        buttonImage: "/Content/images/calendar.gif",
        buttonImageOnly: true,
        buttonText: "Select date",
    });

    $("#datepicker-from").datepicker().datepicker("setDate", new Date());
    $("#datepicker-to").datepicker().datepicker("setDate", new Date());

    $('#select_all').change(function () {
        $('.payment').prop('checked', true);
    });

    $('.payment').change(function() {
        if (!this.checked) {
            $('#select_all').prop('checked', false);
        }
    });
});