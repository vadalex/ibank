function refreshTimer() {
    var $sec = $('.expired-time').clone();
    $('.expired-time').remove();
    $sec.appendTo('.logout');
    var time = moment().add(10, 'm');
    window.tiny_timer = $('.expired-time').tinyTimer({
        to: time,
        format: ' %m:%s',
        onEnd: function () {
            window.location.replace("/Login/Logout");
        }
    });
}

$(document).ready(function () {
    $('.operations').find('#nameChangeBtn').click(function () {
        if (!isCheckedRadio()) {
            alert('Вы не выбрали платеж');
            return;
        }
        var name = prompt('Введите новое имя платежа', getPaymentName());
        name = $.trim(name);
        if (name && (name.length > 0)) {
            $.ajax({
                url: "/OwnPayments/ChangePaymentName",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ paymentId: getPaymentId(), name: name }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        setPaymentName(data.Info, data.Message)
                        refreshTimer();
                    } else {
                        refreshTimer();
                        alert('Ошибка переименования карты');
                    }
                },
                error: function (xhr) {
                    alert('Ошибка переименования карты');
                }
            });
        }
        else {
            alert('Введите хотя бы один символ');
        }
    });

    $('.operations').find('#deleteBtn').click(function () {

        if (!isCheckedRadio())
        {
            alert('Вы не выбрали платеж');
            return;
        }
        if (confirm('Удалить платеж?')) {
            $.ajax({
                url: "/OwnPayments/DeleteOwnPayment",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ paymentId: getPaymentId() }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        window.location.reload();
                        refreshTimer();
                    } else {
                        refreshTimer();
                        alert('Ошибка блокировки карты');
                    }
                },
                error: function (xhr) {
                    alert('Ошибка блокировки карты');
                }
            });
        } else {
            alert('Ну, как хотите');
        }
    });

    $('.operations').find('#nextBtn').click(function () {
        if (!isCheckedRadio()) {
            alert('Вы не выбрали платеж');
            return;
        }
        var paymentId = getPaymentId();
        var type = paymentId.split('/')[0];
        var id = paymentId.split('/')[1];
        window.location.href = "/OwnPayments/OwnPayment?type="+ type +"&paymentId="+id;
    });

});

function getPaymentId() {
    var checkedRadio = $('#ownPaymentsDiv').find('input[type="radio"]:checked').first();
    return checkedRadio.val();
}

function getPaymentName() {
    var checkedRadio = $('#ownPaymentsDiv').find('input[type="radio"]:checked').first();
    var checkedRow = checkedRadio.parents('tr');
    return $.trim(checkedRow.find('.paymentName').first().text());
}

function setPaymentName(id, name) {
    var checkedRadio = $('#ownPaymentsDiv').find('input[type="radio"][value="' + id + '"]');
    var checkedRow = checkedRadio.parents('tr');
    return checkedRow.find('.paymentName').text(name);
}

function isCheckedRadio() {
    var checkedRadio = $('#ownPaymentsDiv').find('input[type="radio"]:checked');
    if(checkedRadio.length === 0)
        return false;
    else 
        return true;
}


