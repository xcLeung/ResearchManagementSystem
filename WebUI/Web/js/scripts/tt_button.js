$(document).ready(function () {

    $('#back').fadeTo('fast', 0.5);
    $('#go').fadeTo('fast', 0.5);
    $('#projectBack').fadeTo('fast', 0.5);


    var mouseinhandler = function () {
        $(this).fadeTo('fast', 1);
    };

    var mouseouthandler = function () {
        $(this).fadeTo('fast', 0.5);
    };

    $('#back').mouseenter(mouseinhandler);
    $('#back').mouseleave(mouseouthandler);
    $('#go').mouseenter(mouseinhandler);
    $('#go').mouseleave(mouseouthandler);
    $('#projectBack').mouseenter(mouseinhandler);
    $('#projectBack').mouseleave(mouseouthandler);
});