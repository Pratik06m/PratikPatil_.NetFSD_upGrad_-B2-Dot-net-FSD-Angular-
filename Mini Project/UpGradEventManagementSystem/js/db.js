let db

let request = indexedDB.open("EventDB",1)

request.onupgradeneeded = function(e){

db = e.target.result

let store = db.createObjectStore("events",{keyPath:"id"})

}

request.onsuccess = function(e){

db = e.target.result

displayEvents()

}