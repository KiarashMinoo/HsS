"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/eventsHub").build();

connection.on("eventRaised", function (message, type) {
    toastr[type](message);
});

connection.start().then(function () {
    return console.log("signalR initiated.");
}).catch(function (err) {
    return console.error(err.toString());
});