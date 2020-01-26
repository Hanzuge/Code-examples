
const isKarkausVuosiES6 = function(vuosi) {
	
	if ((vuosi % 4 == 0)
		&& (vuosi % 100 !== 0 || vuosi % 400 == 0)) {
			return true;
	}
	
	return false;
	
}

var isKarkausVuosiES5 = function(vuosi) {
	
	if ((vuosi % 4 == 0)
		&& (vuosi % 100 !== 0 || vuosi % 400 == 0)) {
			return true;
	}
	
	return false;
	
}

const isKarkausVuosiES6NF = (vuosi) => {
	
	if ((vuosi % 4 == 0)
		&& (vuosi % 100 !== 0 || vuosi % 400 == 0)) {
			return true;
	}
	
	return false;
	
}

const displayResult = function() { 
  
  let vuosi = 2000; 
  
  document.getElementById("tulostus").innerHTML = isKarkausVuosiES6NF(vuosi) ? 'Vuosi ' + vuosi + ' on karkausvuosi.' : 'Vuosi ' + vuosi + ' ei ole karkausvuosi.';
  
}; 
  
window.onload = displayResult; 