// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(1, OUTPUT);
}

// the loop function runs over and over again forever
void loop() {
  for (int x = 0; x < 3; x++) {
    digitalWrite(1, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(500);                       // wait for one time unit
    digitalWrite(1, LOW);    // turn the LED off by making the voltage LOW
    delay(500);                       // wait for one time unit
  }
  delay(1000);                        // wait for two time units
  for (int z = 0; z < 3; z++) {
    digitalWrite(1, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(1500);                       // wait for three time units
    digitalWrite(1, LOW);    // turn the LED off by making the voltage LOW
    delay(500);                       // wait for one time unit
  }
  delay(1000);                        // wait for two time units
  for (int x = 0; x < 3; x++) {
    digitalWrite(1, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(500);                       // wait for one time unit
    digitalWrite(1, LOW);    // turn the LED off by making the voltage LOW
    delay(500);                       // wait for one time unit
  }
  delay(3000);                        // wait for six time units
}
