  int pinA = 2;  // Yhdistettynä CLK pinniin enkooderissa
  int pinB = 4;  // Yhdistettynä DT pinniin enkooderissa
  int encoderPosCount = 0; // Nollataan enkooderin asento
  int pinALast;
  int aVal;
  boolean bCW;
  const int greenButton = 12;     // vihreän valon napin pinni
  const int yellowButton = 13;     // keltaisen valon napin pinni
  const int redButton = 7;     // punaisen valon napin pinni
  int greenState = 0;         // vihreän valon napin status
  int yellowState = 0;         // keltaisen valon napin status
  int redState = 0;         // punaisen valon napin status
  int x = 0;
  int bright = 0;

 void setup() { 
   pinMode(pinA,INPUT);
   pinMode(pinB,INPUT);
   pinMode(9,OUTPUT);
   pinMode(10,OUTPUT);
   pinMode(11,OUTPUT);
   pinMode(greenButton, INPUT);
   pinMode(yellowButton, INPUT);
   pinMode(redButton, INPUT);
   pinALast = digitalRead(pinA); // lue enkooderin nykyinen tila viimeiseksi tilaksi
   digitalWrite(9, HIGH); // Laita kaikki ledit päälle
   digitalWrite(10, HIGH);
   digitalWrite(11, HIGH);
 } 

 void loop() {
  greenState = digitalRead(greenButton); // lue nappien tilat
  yellowState = digitalRead(yellowButton);
  redState = digitalRead(redButton);

  if (greenState == HIGH) { // jos nappi painettuna kerro mitä lediä säätää
    x = 9;
  }
  if (yellowState == HIGH) {
    x = 10;
  }
  if (redState == HIGH) {
    x = 11;
  }
  
  aVal = digitalRead(pinA); // lue enkooderin tila
  if (aVal != pinALast){ // jos enkooderi pyörii
    if (digitalRead(pinB) != aVal) {  // Jos pinA muuttuu ensin liikkuu enkooderi myötäpäivään
      encoderPosCount += 15; // liiku 15 askelta
      bCW = true; // myötäpäivään, bCW = boolean ClockWise
    } else { // muussa tapauksessa liikkuu enkooderi vastapäivään
      bCW = false;
      encoderPosCount -= 15;
    }
      if (encoderPosCount >= 255) { // jos enkooderin arvo on yli 255 sytytä ledi täysille
        bright = 255;
      }
      else if (encoderPosCount <= 0) { // jos enkooderin arvo on negatiivinen laita led pois päältä
        bright = 0;
      }
      else { // muussa tapauksessa enkooderin arvo on ledin kirkkauden arvo
        bright = encoderPosCount;
      }
  }
  analogWrite(x, bright); // lähetä kirkkauden arvo ledille
  pinALast = aVal; // laita enkooderin nykyinen arvo viimeiseksi arvoksi
 } 
