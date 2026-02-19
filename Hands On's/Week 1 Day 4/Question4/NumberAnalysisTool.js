let num = 7;   

let sign = (num >= 0) ? "Positive" : "Negative";
console.log("Number is:", sign);

if (num % 2 === 0) {
    console.log("Number is: Even");
} else {
    console.log("Number is: Odd");
}

console.log("Numbers from 1 to", num, ":");

for (let i = 1; i <= num; i++) {
    console.log(i);
}
