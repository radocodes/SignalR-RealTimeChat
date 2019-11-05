"use strict";

var groupName = document.getElementById("groupName").value;
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says: " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    connection.invoke("Join", groupName);
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var groupName = document.getElementById("groupName").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessageToGroup", groupName, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});