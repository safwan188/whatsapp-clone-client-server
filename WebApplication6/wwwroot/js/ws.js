// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    var connection = new signalR.HubConnectionBuilder().withUrl("/myHub").build();
    connection.start();
    $('textarea').keyup(() => {
        console.log('sending: ' + $('textarea').val());
        connection.invoke("changed", $('textarea').val());
    });
    connection.on("ChangeReceived", function (value) {
        console.log('received ' + value);
        $('textarea').val(value);
    });
});