$(document).ready(function () {

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
    $("#StartDate").datepicker({
        showOn: "button",
        buttonImage: "/Content/images/calendar.gif",
        buttonImageOnly: true,
        buttonText: "Select date",
        minDate: currDate
    });


   // new Date($("#hiddenYear").val(), $("#hiddenMonth").val(), $("#hiddenDay").val());
    var currDate = new Date();
    
    if (($("#StartMinutes").val() === "0") && ($("#StartHours").val() === "0") && ($('#hiddenTicks').val() === "0")) {
        $("#StartMinutes").val(currDate.getMinutes());
        $("#StartHours").val(currDate.getHours());
        $("#StartDate").datepicker().datepicker("setDate", currDate);
    } else {
        $("#StartDate").datepicker("setDate", new Date(parseInt($("#hiddenYear").val()), parseInt($("#hiddenMonth").val()) - 1, parseInt($("#hiddenDay").val())));
    }
    

});