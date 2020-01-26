/* File: Exercise5a2
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Changing LEDs depending on brightness
 */
#include <Arduino.h>
#define REDLED 2
#define YELLOWLED 1
#define GREENLED 3
#define PHOTORESISTOR A0

volatile bool state = 1;

void greenLedOn() {
	digitalWrite(GREENLED, state);
	state = !state;
}

void setup() {
	pinMode(PHOTORESISTOR, INPUT);
	pinMode(REDLED, OUTPUT);
	pinMode(YELLOWLED, OUTPUT);
	pinMode(GREENLED, OUTPUT);
	attachInterrupt(digitalPinToInterrupt(REDLED), greenLedOn, RISING); // green led on if red led goes on
}

void loop() {
	int brightness = analogRead(PHOTORESISTOR); // Read brightness 
	brightness = map(brightness, 0, 1023, 0, 255); // Mapping resistance value to 0-255 to match UML
	// if medium brightness, yellow led on
	if (brightness > 86 && brightness <= 170) {
		digitalWrite(YELLOWLED, HIGH);
		delay(2000);
	}
	// if low brightness, red and yellow led on
	else if (brightness <= 85){
		digitalWrite(YELLOWLED, HIGH);
		digitalWrite(REDLED, HIGH);
		delay(1000);
		greenLedOn();
		delay(1000);
	}
	// if high brightness, no leds on
	else {
		delay(2000);
	}
	digitalWrite(YELLOWLED, LOW);
	digitalWrite(REDLED, LOW);
	delay(500);
}
