$(document).ready(function() {
    var time = moment().add(10, 'm');
    $('.expired-time').tinyTimer({
        to: time,
        format: ' %m:%s',
        onEnd: function() {
            window.location.replace("/Login/Logout");
        }
    });
})