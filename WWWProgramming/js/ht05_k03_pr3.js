
function createEmployees() {

    var employee = {};

    employee.nimi = {
        first = 'Mikael',
        last = 'Karvajalka'
    };
    employee.jobId = ['123', '432', '444'];
    employee.palkka = 2400;

    return employee;

}

function displayResult() {

    var Mikael = createEmployees();

    document.getElementById("tulostus").innerHTML = /*Mikael.nimi.first + ', ' + Mikael.nimi.last + ' palkka on: ' + */Mikael.palkka + '€';

}

window.onload = displayResult; 