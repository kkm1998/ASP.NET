var connection = new signalR.HubConnectionBuilder().withUrl("/counter").build();

connection.on("UserCount", function (message) {
    document.getElementById("licz").innerText = message;
});

connection.start();