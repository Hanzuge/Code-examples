/*File: Exercise4a2
Authors: Hanna Salminen and Aki Rönnkvist
Description: Blinking multiple led using interrupts */
#include <Arduino.h>

// defining LEDs
#define GREENLED 2
#define REDLED1 7
#define YELLOWLED 3
#define REDLED2 8

volatile boolean greenOutput = HIGH;
volatile boolean yellowOutput = HIGH;

// Blinking red LED1 opposite to green LED and changing green LED to HIGH/LOW
void blinkRedLED1() {
	greenOutput = !greenOutput;
	digitalWrite(REDLED1, greenOutput);
}

// Blinking red LED2 at the same time as yellow LED and changing yellow LED to HIGH/LOW
void blinkRedLED2() {
	digitalWrite(REDLED2, yellowOutput);
	yellowOutput = !yellowOutput;
}

void setup() {
	pinMode(GREENLED, OUTPUT);
	pinMode(REDLED1, OUTPUT);
	pinMode(YELLOWLED, OUTPUT);
	pinMode(REDLED2, OUTPUT);
	attachInterrupt(digitalPinToInterrupt(GREENLED), blinkRedLED1, CHANGE);
	attachInterrupt(digitalPinToInterrupt(YELLOWLED), blinkRedLED2, CHANGE);
}

void loop() {
	// Green led blinking on/off in 1 second delay and yellow led on/off in 0,5 second delay
	digitalWrite(GREENLED, greenOutput);
	digitalWrite(YELLOWLED, yellowOutput);
	delay(500);
	digitalWrite(YELLOWLED, yellowOutput);
	delay(500);
	digitalWrite(GREENLED, greenOutput);
	digitalWrite(YELLOWLED, yellowOutput);
	delay(500);
	digitalWrite(YELLOWLED, yellowOutput);
	delay(500);
}
