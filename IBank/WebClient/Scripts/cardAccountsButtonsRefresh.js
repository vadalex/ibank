$(document).ready(function () {
    function supportButtonsStatus() {
        var checkedRadio = $('#cards').find('input[type="radio"]:checked');
        var checkedRow = checkedRadio.parents('tr');
        var isBlocked = checkedRow.find('input[name="item.CardAccount.IsLocked"]').first().val() === 'False' ? false : true;

        if (isBlocked) {
            $('.block').hide();
            $('.unblock').show();
        } else {
            $('.block').show();
            $('.unblock').hide();
        }
    }

    supportButtonsStatus();

    $('input[name="item.CardAccount.CardAccountID"]').change(supportButtonsStatus);
    $('input[name="item.CardAccount.IsLocked"]').change(supportButtonsStatus);
});