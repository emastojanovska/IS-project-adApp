class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}
// 2
function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let when = new Date();
    let message = new Message(username, text, when);
    sendMessageToHub(message);
}

// 5
function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let rowDiv = document.createElement('div');
    rowDiv.className = "row";

    let offsetDiv = document.createElement('div');
    offsetDiv.className = isCurrentUserMessage ? "col-md-6 offset-md-6" : "";
   
    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker bg-primary" : "container bg-light";

    let sender = document.createElement('p');
    sender.innerHTML = message.userName;
    sender.className = isCurrentUserMessage ? "sender text-right text-white" : "sender text-left"

    let text = document.createElement('p');
    text.innerHTML = message.text;
    text.className = isCurrentUserMessage ? "text-right text-white": "text-left"

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-right text-light" : "time-left";
    var currentdate = new Date();
    when.innerHTML = 
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
    + currentdate.getFullYear() + " "
    + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: true })

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);

    offsetDiv.appendChild(container);
    rowDiv.appendChild(offsetDiv);
    chat.appendChild(rowDiv);
}
