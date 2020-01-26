function isKarkausVuosi(vuosi) {
    if ((vuosi % 4 == 0)
		&& (vuosi % 100 !== 0 || vuosi % 400 == 0)) {
            return true;
        }
}

const displayResult = function() { 
  
  let vuosi = 2000; 
  
  document.getElementById("tulostus").innerHTML = isKarkausVuosi(vuosi) ? 'Vuosi ' + vuosi + ' on karkausvuosi.' : 'Vuosi ' + vuosi + ' ei ole karkausvuosi.';
  
}; 
  
window.onload = displayResult; 