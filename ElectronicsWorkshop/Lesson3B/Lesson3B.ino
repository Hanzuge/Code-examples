int sensorPin = A0; // select the input pin for LDR

int sensorValue = 0; // variable to store the value coming from the sensor
void setup() {
  pinMode(12, OUTPUT);
}
void loop() {
sensorValue = analogRead(sensorPin); // read the value from the sensor
if (sensorValue >= 300)
{
  digitalWrite(12, LOW);
}
else
{
  digitalWrite(12, HIGH);
}

delay(100);

}
