const jarjestys = function(kokonaisluvut) {
    kokonaisluvut.sort(function(a, b){return a-b})
    return kokonaisluvut
}

const displayResult = function() { 
  
  let kokonaisluvut = [2, 84, 26, 100, 52, 4];
  
  document.getElementById("tulostus").innerHTML = 'Taulukko järjesteltynä: ' + jarjestys(kokonaisluvut);
}; 
  
window.onload = displayResult; 
