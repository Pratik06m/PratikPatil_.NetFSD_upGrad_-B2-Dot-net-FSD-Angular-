let signal = "green";  

switch(signal.toLowerCase()) {
    
    case "red":
        console.log("Stop");
        break;
        
    case "yellow":
        console.log("Get Ready");
        break;
        
    case "green":
        console.log("Go");
        break;
        
    default:
        console.log("Invalid signal color");
}
