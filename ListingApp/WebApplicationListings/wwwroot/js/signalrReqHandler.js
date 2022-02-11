var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Chat/Index')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.message);
    });

// 3
function sendMessageToHub(message) {
    connection.invoke('sendMessage', message);
}