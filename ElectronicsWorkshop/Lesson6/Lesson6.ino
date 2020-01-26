int R1=1052; // vastus ennen termistoria
float Bt=3950.0;// B arvo
float Tc=0;// lämpötila celsiuksina
float Tf=0;// lämpötila fahrenheitteina
int i = 0;

#include <LiquidCrystal.h> // käytetään näytön koodia
const int temp = 13; // päivitysnappi
const int ldr = 5; // lämpötilan vaihto
int tempState = 0; // napin tila
int ldrState = 0; // napin tila
int sensorPin = A0; // LDR sensorin pinni
int sensorValue = 0; // sensorin arvo kirjataan tähän

LiquidCrystal lcd(7, 8, 9, 10, 11, 12); // Osoitetaan oikeat pinnit näytölle
void setup() {
  lcd.begin(16, 2); // näytön sarakkeet ja rivit
  pinMode(temp, INPUT);
  pinMode(ldr, INPUT);
}

void loop() {
  tempState = digitalRead(temp); // lue päivitysnapin tila
  sensorValue = analogRead(sensorPin); // lue ldr sensorin arvo

  if (tempState == LOW) { // jos päivitysnappi on painettuna päivitä arvot
    lcd.setCursor(0, 0);
    lcd.print("                "); // tyhjennä rivi ensin ja sen jälkeen kirjoita uudet tiedot
    lcd.setCursor(0, 0);
    updatetemp();
    lcd.setCursor(0, 1);
    lcd.print("                ");
    lcd.setCursor(0, 1);
    lcd.print(sensorValue);
    delay(500);
  }

  ldrState = digitalRead(ldr); // lue lämpötilanapin tila
  
  if (ldrState == HIGH) { // jos nappi on painettuna
    if (i == 0) { // jos näytössä celciukset niin vaihda fahrenheitiksi ja toisin päin
      i = 1;
    }
    else {
      i = 0;
    }
    lcd.setCursor(0, 0);
    lcd.print("                ");
    lcd.setCursor(0, 0);
    updatetemp();// kirjoita uusi arvo kenttään
    delay(500);
  }
}

void updatetemp() {// funktio joka laskee lämpötilan
  int x=analogRead(A2); // lue arvo pinnistä A2
  float Vout=x*(5/1023.0); // muuta se volteiksi
  float Rt=R1*Vout/(5.0-Vout); // termistorin vastus
  float a1=1/298.15; // 1/T0 jossa T0 on 25 celcius-astetta Kelvineinä
  float b1=1/Bt;
  float c1=log(Rt/1000.0); // 1000 on R0 nollassa asteessa
  float y1=a1+b1*c1; // kaavan tulos
  float T=1/y1;// lämpötila kelvineinä
  Tf=T*9/5-459.67; // lämpötila fahrenheitteina
  Tc=T-273.15; // lämpötila celciuksena
  lcd.setCursor(0, 0);
  if (i < 1) {
    lcd.print(Tc); // tulosta lämpötila celciuksena
  }
  else {
    lcd.print(Tf); // tulosta lämpötila fahrenheitteina
  }
}
