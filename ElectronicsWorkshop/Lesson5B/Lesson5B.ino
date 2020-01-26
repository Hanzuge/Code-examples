
byte seven_seg_digits[10][7] = { { 0,0,1,0,0,0,0 },  // = 9 tehdään "arrayt" joihin tallennetaan jokaisen numeron tarvittavat HIGH/LOW tilat
                                 { 0,0,0,0,0,0,0 },  // = 8
                                 { 0,1,1,1,0,0,1 },  // = 7
                                 { 0,0,0,0,0,1,0 },  // = 6
                                 { 0,0,1,0,0,1,0 },  // = 5
                                 { 1,0,1,1,0,0,0 },  // = 4
                                 { 0,1,1,0,0,0,0 },  // = 3
                                 { 0,1,0,0,1,0,0 },  // = 2
                                 { 1,1,1,1,0,0,1 },  // = 1
                                 { 0,0,0,0,0,0,1 }   // = 0
                                 };

void setup() {                
  pinMode(2, OUTPUT);   // Merkitään pinnit 2-8 outputeiksi
  pinMode(3, OUTPUT);
  pinMode(4, OUTPUT);
  pinMode(5, OUTPUT);
  pinMode(6, OUTPUT);
  pinMode(7, OUTPUT);
  pinMode(8, OUTPUT);
}
    
void sevenSegWrite(byte digit) {
  byte pin = 2; // Aloitetaan pinni kakkosesta
  for (byte segCount = 0; segCount < 7; ++segCount) { // aloitetaan luvusta 0 "arrayssa" ja käydään läpi kaikki 8. Sen jälkeen siirrytään seuraavaan "arrayhin" eli seuraavaan numeroon
    digitalWrite(pin, seven_seg_digits[digit][segCount]); // kirjoitetaan ensin pinni 2:seen ensimmäisen arrayn ensimmäinen luku, jonka jälkeen pinnin lukemaa lisätään yhdellä ja myös "arrayn" lukua, eli seuraavaksi kirjoitetaan "arrayn" toinen luku pinniin 3 jne.
    ++pin;
  }
}

void loop() {
  for (byte count = 10; count > 0; --count) { // toistetaan kaikki kymmenen lukua
   delay(1000); // Odotetaan sekunti
   sevenSegWrite(count - 1); // Toistetaan sevenSegWrite koodi ylempää ja aloitetaan "array" ysistä ja liikutaan siitä aina "array" ykköseen
  }
}
