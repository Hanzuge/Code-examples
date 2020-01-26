int led = 12; // Nimetään pinni 12 lediksi

void setup() {
  
  Serial.begin(9600); // Avataan serial yhteys
  pinMode(led, OUTPUT); // Merkitään led outputiksi
  
}

void loop() {
  
  int LDR = analogRead(A0); // Luetaan LDR sensorin arvo
  Serial.println(LDR); // Printataan LDR sensorin arvo näytölle
  if (LDR >= 250){
    
    digitalWrite(led, LOW); // Jos sensorin arvo on yli 250 LED on pois päältä
  
    
  }
  
  else {
   
    digitalWrite(led, HIGH); // Jos sensorin arvo on alle 250 LED syttyy päälle
  
    
  }
  delay(100); // Odotetaan 100 millisekuntia, jottei arduino mene sekaisin
}
