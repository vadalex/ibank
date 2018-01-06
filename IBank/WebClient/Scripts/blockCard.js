$(document).ready(function () {

    function getCardId() {
        var checkedRadio = $('#cards').find('input[type="radio"]:checked').first();
        return checkedRadio.val();
    }

    function getCardName() {
        var checkedRadio = $('#cards').find('input[type="radio"]:checked').first();
        var checkedRow = checkedRadio.parents('tr');
        return $.trim(checkedRow.find('.card-name').first().text());
    }

    function setCardName(id, name) {
        var checkedRadio = $('#cards').find('input[type="radio"][value="' + id + '"]');
        var checkedRow = checkedRadio.parents('tr');
        return checkedRow.find('.card-name').text(name);
    }

    function refreshCardStatus(id, status) {
        var checkedRadio = $('#cards').find('input[type="radio"][value="' + id + '"]');
        var checkedRow = checkedRadio.parents('tr');
        checkedRow.find('.status').text(status);
    }

    function refreshCardIsLock(id, isLock) {
        var checkedRadio = $('#cards').find('input[type="radio"][value="' + id + '"]');
        var checkedRow = checkedRadio.parents('tr');
        checkedRow.find('input[name="item.CardAccount.IsLocked"]').first().val(isLock ? 'True' : 'False').trigger('change');
    }

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

    $('.operations').find('#block').click(function () {

        if (confirm('Заблокировать карту?')) {
            $.ajax({
                url: "/CardAccount/BlockCard",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ cardId: getCardId() }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        refreshCardStatus(data.Info, data.Message);
                        refreshCardIsLock(data.Info, true);
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

    $('.operations').find('#unblock').click(function () {

        if (confirm('Разблокировать карту?')) {
            $.ajax({
                url: "/CardAccount/UnBlockCard",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ cardId: getCardId() }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        refreshCardStatus(data.Info, data.Message);
                        refreshCardIsLock(data.Info, false);
                        refreshTimer();
                    } else {
                        refreshTimer();
                        alert('Ошибка разблокировки карты');
                    }
                },
                error: function (xhr) {
                    alert('Ошибка разблокировки карты');
                }
            });
        } else {
            alert('Ну, как хотите');
        }

    });

    $('.operations').find('#nameChange').click(function () {

        var name = prompt('Введите наименование', getCardName());
        name = $.trim(name);
        if (name && (name.length > 0)) {
            $.ajax({
                url: "/CardAccount/ChangeName",
                dataType: "json",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ cardId: getCardId(), cardName: name }),
                async: true,
                processData: false,
                cache: false,
                success: function (data) {
                    if (data.Success === true) {
                        setCardName(data.Info, data.Message)
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

});