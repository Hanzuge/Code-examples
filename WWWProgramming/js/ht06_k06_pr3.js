var alustaSovellus = () => {
    document.querySelector('#lisaa').addEventListener('click', () => {

        const uusiViesti =
            document.querySelector('#uusiviesti').value;

        if (uusiViesti && uusiViesti.length > 0) {

            const uusiElementtiP = document.createElement('p');
            const uusiTekstiSolmu = document.createTextNode(uusiViesti);
            uusiElementtiP.appendChild(uusiTekstiSolmu);
            const viestit = document.querySelector('#viestit');
            viestit.appendChild(uusiElementtiP);
            document.querySelector('#uusiviesti').value = '';

        }
    })
};

window.addEventListener('load', () => {
    alustaSovellus();
});