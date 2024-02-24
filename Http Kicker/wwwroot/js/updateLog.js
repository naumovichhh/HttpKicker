$(function () {
    const hubConnection = new signalR.HubConnectionBuilder().withUrl("/Log").build();
    hubConnection.on("Update", function (message) {
        $("#log").val(function (i, s) {
            return s + "\n" + message;
        });

        document.getElementById("log").scrollTop = document.getElementById("log").scrollHeight;
    });

    hubConnection.start();
});