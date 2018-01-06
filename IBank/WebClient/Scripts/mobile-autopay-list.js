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
    $('.operations').find('#deleteBtn').click(function () {

        if (!isCheckedRadio()) {
            alert('Вы не выбрали автооплату');
            return;
        }
        if (confirm('Удалить автооплату?')) {
            $.ajax({
                url: "/MobileAutoPayment/Delete",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ Id: getAutoPayId() }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        window.location.reload();
                        refreshTimer();
                    } else {
                        refreshTimer();
                        alert('Ошибка удаления');
                    }
                },
                error: function (xhr) {
                    alert('Ошибка удаления');
                }
            });
        } else {
            alert('Ну, как хотите');
        }
    });

    $('.operations').find('#nextBtn').click(function () {
        if (!isCheckedRadio()) {
            alert('Вы не выбрали автооплату');
            return;
        }
        var id = getAutoPayId();

        window.location.replace("/MobileAutoPayment/Details?Id=" + id);
    });

    $('.operations').find('#create').click(function () {
        window.location.replace("/MobileAutoPayment/Create");
    });

});

function getAutoPayId() {
    var checkedRadio = $('#autopayments').find('input[type="radio"]:checked').first();
    return checkedRadio.val();
}


function isCheckedRadio() {
    var checkedRadio = $('#autopayments').find('input[type="radio"]:checked');
    if (checkedRadio.length === 0)
        return false;
    else
        return true;
}


