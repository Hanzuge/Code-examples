
function createKirjaPN() {

    var kirja = {};
    kirja.nimi = 'Sinuhe egyptiläinen';
    kirja.tekijat = ['Waltari'];
    kirja.isbn = 'kj435sd9';

    return kirja;

}


function displayResult() {

    var sinuhe = createKirjaPN();

    document.getElementById("tulostus").innerHTML = sinuhe.tekijat[0] + ': ' + sinuhe.nimi + '.';
  
}

window.onload = displayResult; 