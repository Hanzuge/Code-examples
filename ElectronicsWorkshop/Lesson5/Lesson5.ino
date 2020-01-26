void setup() {
  pinMode(2, OUTPUT);
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  pinMode(8, OUTPUT); // Merkitään pinnit 2-8 outputeiksi
}

void loop() {
  digitalWrite(2, HIGH); // Laitetaan LEDit pois päältä koska kyseessä Anode näyttö joten oletuksena kaikki valot päällä
  digitalWrite(3, HIGH);
  digitalWrite(4, HIGH);
  digitalWrite(5, HIGH);
  digitalWrite(6, HIGH);
  digitalWrite(7, HIGH);
  digitalWrite(8, HIGH);
  for (int i=2; i <= 7; i++){ // For looppi toistuu kunnes on käyty kaikki LEDit paitsi 8 läpi ja siirtyy koodin alkuun
    digitalWrite(8, LOW); // LED 8 päälle
    digitalWrite(i, LOW); // Yksi LEDeistä päälle riippuen monesko kierros loopissa
    delay(200); // Odota 200ms
    digitalWrite(8, HIGH); // LED 8 pois päältä
    delay(300); // Odota 300ms, jotta LEDit eivät vilkkuisi samaan aikaan
    digitalWrite(i, HIGH); // Toinen LEDeistä pois päältä
    delay(100); // Odota 100ms, jotta siirtyminen olisi sulavaa ja siirry loopin alkuun, jolloin seuraava LED menee päälle
  }
}
