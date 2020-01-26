// the setup function runs once when you press reset or power the board
void setup() {
  pinMode(3, OUTPUT);                 // Valitaan pin 3 outputiksi
  pinMode(12, INPUT_PULLUP);
}

// toistaa luuppia ikuisesti
void loop()
{
  if (digitalRead(12) == LOW)
  {
    digitalWrite(3, HIGH);
  }
  if (digitalRead(12) == HIGH)
  {
    digitalWrite(3, LOW);
  }
}
