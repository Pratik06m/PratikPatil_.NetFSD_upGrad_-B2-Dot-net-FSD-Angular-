document.getElementById("loginForm")
.addEventListener("submit", function(e){

e.preventDefault()

let email = document.getElementById("email").value
let password = document.getElementById("password").value

// Hardcoded credentials

if(email === "admin@upgrad.com" && password === "12345"){

alert("Login Successful")

// Save login session

sessionStorage.setItem("admin","true")

window.location = "events.html"

}

else{

alert("Invalid Login Credentials")

}

})