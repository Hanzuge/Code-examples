const hotellihuone = function(huonetyyppi, alennuskoodi) {
    var hinta = 0.00
    if (huonetyyppi == 'standard') {
        hinta = 100.00;
        if (alennuskoodi == 'ale10') {
            hinta = hinta*0.9
            return hinta;
        }
        else if (alennuskoodi == 'ale30') {
            hinta = hinta*0.7
            return hinta;
        }
        else
            return hinta;
    }
    else if (huonetyyppi == 'luxury') {
        hinta = 200.00;
        if (alennuskoodi == 'ale10') {
            hinta = hinta*0.9
            return hinta;
        }
        else if (alennuskoodi == 'ale30') {
            hinta = hinta*0.7
            return hinta;
        }
        else
            return hinta;
    }

    
    return false;
    
}

const displayResult = function() { 
  
  let huonetyyppi = 'standard';
  let alennuskoodi = 'ale30' 
  
  document.getElementById("tulostus").innerHTML = 'Hotellihuoneen hinta on  ' + hotellihuone(huonetyyppi, alennuskoodi) + ' euroa.';
}; 
  
window.onload = displayResult; 
