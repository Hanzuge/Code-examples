/* Sketch.cpp
Author: Sanna Maatta
This is a pretty stupid test program for learning how to watch variable values
*/

#include <Arduino.h>

#define LED 13

void blinkLed();

void setup() {

	pinMode(LED, OUTPUT);
}

void loop() {
	
	int rounds = 7;
	int counter = 0;

	for (int i = 0; i < rounds; i++) {

		blinkLed();
		counter = counter + 3;
	}
}

void blinkLed() {
	
	for (int i = 0; i < 3; i++) {

		digitalWrite(LED, HIGH);
		delay(500);
		digitalWrite(LED, LOW);
		delay(500);

	}
}