/*File: Exercise4a3
Authors: Hanna Salminen and Aki Rönnkvist
Description: Coding traffic light in Arduino */
#include <Arduino.h>

#define REDLED 2
#define YELLOWLED 1
#define GREENLED 3

volatile boolean greenOutput = LOW;
volatile boolean yellowOutput = LOW;

void greenLedOn() {
	greenOutput = !greenOutput;
	yellowOutput = !yellowOutput;
}

void yellowLedOn() {
	yellowOutput = !yellowOutput;
}

void setup() {
	// initialize the digital pin1-3 as an outputs.
	pinMode(REDLED, OUTPUT);
	pinMode(YELLOWLED, OUTPUT);
	pinMode(GREENLED, OUTPUT);
	attachInterrupt(digitalPinToInterrupt(REDLED), greenLedOn, FALLING);
	attachInterrupt(digitalPinToInterrupt(GREENLED), yellowLedOn, FALLING);
}

void loop() {
	digitalWrite(YELLOWLED, yellowOutput);
	digitalWrite(REDLED, HIGH);
	delay(2000);
	yellowOutput = !yellowOutput;
	digitalWrite(YELLOWLED, yellowOutput);
	delay(1000);
	digitalWrite(REDLED, LOW);
	digitalWrite(YELLOWLED, yellowOutput);
	digitalWrite(GREENLED, greenOutput);
	delay(2000);
	greenOutput = !greenOutput;
	digitalWrite(GREENLED, greenOutput);
	digitalWrite(YELLOWLED, yellowOutput);
	delay(1000);
	yellowOutput = !yellowOutput;
}