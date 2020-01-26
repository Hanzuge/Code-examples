int FAN_PIN = 3; // Tuuletin pinni
void setup() {
  Serial.begin(9600);
  pinMode(FAN_PIN,OUTPUT);
  analogWrite(FAN_PIN, 127); // laitetaan tuuletin pyörimään puoliteholla
  Serial.println("Control fan speed by serial monitor");
  Serial.println("Send 0 to 255 to control fan speed");
}

void loop() {
if (Serial.available() > 0) { // jos serial monitori on auki
    int fan_speed =  Serial.parseInt(); // lue mitä kirjoitetaan
    if (fan_speed > 0) { // jos arvo on yli 0 niin kirjoita monitoriin arvo ja lähetä se tuulettimelle
      Serial.println(fan_speed);
      analogWrite(FAN_PIN, fan_speed);
    }
}
}
