
const getIthLinux = (Ith) => {

    //let linuxes = []; //Tyhjä taulukko literaalilla
    let linuxes = ['Ubuntu', 'Centos', 'Redhat'];
    //let linuxes = ['Ubuntu', 3, 'Centos', (), 'Redhat'];

    return linuxes[Math.floor(linuxes.length/2)] //Palauttaa Centos

}

const displayResult = function() { 
  
  let vuosi = 2000; 
  
  document.getElementById("tulostus").innerHTML = isKarkausVuosiES6NF(vuosi) ? 'Vuosi ' + vuosi + ' on karkausvuosi.' : 'Vuosi ' + vuosi + ' ei ole karkausvuosi.';
  
}; 
  
window.onload = displayResult; 