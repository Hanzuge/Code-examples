#include <LiquidCrystal.h>

int FAN_PIN = 3; // Tuuletin pinni
int pinA = 2;  // Yhdistettynä CLK pinniin enkooderissa
int pinB = 4;  // Yhdistettynä DT pinniin enkooderissa
int encoderPosCount = 0; // Nollataan enkooderin asento
int pinALast;
int aVal;
boolean bCW;
int fan_speed;
int percent = 0;
const int rs = 5, en = 6, d4 = 7, d5 = 8, d6 = 9, d7 = 10;
LiquidCrystal lcd(rs, en, d4, d5, d6, d7);
unsigned long previousMillis = 0; // edellinen aika kun näyttö päivitettiin
const long interval = 2000; // näytön päivitysväli

void setup() {
  pinMode(FAN_PIN,OUTPUT);
  pinMode(pinA,INPUT);
  pinMode(pinB,INPUT);
  pinALast = digitalRead(pinA); // lue enkooderin nykyinen tila viimeiseksi tilaksi
  lcd.begin(16, 2);
}

void loop() {
  aVal = digitalRead(pinA); // lue enkooderin tila
    if (aVal != pinALast){ // jos enkooderi pyörii
      if (digitalRead(pinB) != aVal) {  // Jos pinA muuttuu ensin liikkuu enkooderi myötäpäivään
        encoderPosCount += 15; // liiku 15 askelta
        bCW = true; // myötäpäivään, bCW = boolean ClockWise
      } else { // muussa tapauksessa liikkuu enkooderi vastapäivään
        bCW = false;
        encoderPosCount -= 15;
      }
        if (encoderPosCount >= 255) { // jos enkooderin arvo on yli 255 laita tuuletin täysille
          fan_speed = 255;
          percent = 100;
        }
        else if (encoderPosCount <= 0) { // jos enkooderin arvo on negatiivinen laita tuuletin pois päältä
          fan_speed = 0;
          percent = 0;
        }
        else { // muussa tapauksessa enkooderin arvo on tuulettimen nopeuden arvo
          fan_speed = encoderPosCount;
          percent = encoderPosCount*0.39; // lasketaan prosentti arvosta
        }
    }
    analogWrite(FAN_PIN, fan_speed); // lähetä nopeuden arvo tuulettimelle
    pinALast = aVal; // laita enkooderin nykyinen arvo viimeiseksi arvoksi
    unsigned long currentMillis = millis(); // laske millisekunteja

    if (currentMillis - previousMillis >= interval) { // jos aikaa on kulunut edellisestä päivityksestä enemmän tai saman verran kuin intervalli
      previousMillis = currentMillis; // siirrä nykyinen aika edelliseksi ajaksi
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("The Fan is at:");
      lcd.setCursor(0, 1);
      lcd.print(percent);
      lcd.println("%              ");
    }
}
