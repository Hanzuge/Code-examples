const http = require('fs'); 
  
false.readFile('runo.txt', (err, data) => {
    if (err) return console.error(err);
    console.log(data.toString());
});

console.log('Ohjelman viimeinen koodirivi suoritettiin!')