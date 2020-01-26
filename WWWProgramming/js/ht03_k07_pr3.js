function getNthFibonacciNumberES5(nth) { 
  
  var a = 0, b = 1, temp; //temp == undefined 
  
  for (var i = 0; i <= nth; i++) { 
  temp = a; 
  a = a + b; 
  b = temp; 
  } 
  
  return b; 
  
} 
  
function getNthFibonacciNumberES6(nth) { 
  
  let a = 0, b = 1, temp; //temp == undefined 
  
  for (let i = 0; i <= nth; i++) { 
  temp = a; 
  a = a + b; 
  b = temp; 
  } 
  
  return b; 
  
} 
  
function displayResult() { 
  
  var nth = 7; 
  
  document.getElementById("tulostus").innerHTML = 
  nth + '. Fibonaccin luku on ' + getNthFibonacciNumberES5(nth); 
  
} 
  
window.onload = displayResult; 