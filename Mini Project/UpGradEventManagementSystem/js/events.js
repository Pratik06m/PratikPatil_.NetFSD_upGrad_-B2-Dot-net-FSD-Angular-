if(sessionStorage.getItem("admin") !== "true"){

alert("Unauthorized Access")

window.location = "login.html"

}

document.getElementById("eventForm")
.addEventListener("submit", function(e){

e.preventDefault()

let event = {

id: eventId.value,
name: eventName.value,
category: category.value,
date: date.value,
time: time.value,
url: url.value

}

let tx = db.transaction("events","readwrite")

let store = tx.objectStore("events")

store.add(event)

alert("Event Added Successfully")

displayEvents()

})

function displayEvents(){

let tx = db.transaction("events","readonly")

let store = tx.objectStore("events")

let request = store.getAll()

request.onsuccess = function(){

let events = request.result

let html=""

events.forEach(e=>{

html += `
<div class="col-md-4">

<div class="card shadow mb-3">

<div class="card-body">

<h5>${e.name}</h5>

<p>ID : ${e.id}</p>

<p>Category : ${e.category}</p>

<p>Date : ${e.date}</p>

<a href="${e.url}" target="_blank">Join Event</a>

<br><br>

<button class="btn btn-danger"
onclick="deleteEvent('${e.id}')">

Delete

</button>

</div>

</div>

</div>`

})

document.getElementById("eventList").innerHTML = html

}

}


function deleteEvent(id){

let tx = db.transaction("events","readwrite")

let store = tx.objectStore("events")

store.delete(id)

displayEvents()

}


function searchEvent(value){

value = value.toLowerCase()

let cards = document.querySelectorAll(".card")

cards.forEach(card=>{

if(card.innerText.toLowerCase().includes(value)){

card.style.display="block"

}

else{

card.style.display="none"

}

})

}

function logout(){

sessionStorage.removeItem("admin")

window.location="login.html"

}