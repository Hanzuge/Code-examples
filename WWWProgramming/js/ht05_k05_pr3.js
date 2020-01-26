
function createKirjaPN() {

    var kirja = {};
    
    kirja.nimi = 'Sinuhe egyptiläinen';
    kirja.tekijat = ['Waltari'];
    kirja.isbn = 'kj435sd9';

    return kirja;

}

function createKirjaKSN() {

    var kirja = {};

    kirja['nimi'] = 'Mikael Karvajalka';
    kirja['tekijat'] = ['Waltari'];
    kirja['isbn'] = 'kj43dfg34';

    return kirja;

}

function displayResult() {

    var sinuhe = createKirjaPN();
    var mikael = createKirjaKSN();
    
    mikael.nimi = 'Mikael Carvajal';

    //document.getElementById("tulostus").innerHTML = sinuhe.tekijat[0] + ': ' + sinuhe.nimi + '.';

    document.getElementById("tulostus").innerHTML = mikael['tekijat'][0] + ': ' + mikael['nimi'] + '.';
  
}

window.onload = displayResult; 