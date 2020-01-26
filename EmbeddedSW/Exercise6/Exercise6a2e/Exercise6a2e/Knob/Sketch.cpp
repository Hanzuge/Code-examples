/* File: Exercise5a2b
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: 
 */
#include <Arduino.h>
#include <Servo.h>
#include "Led.h"
#define BUTTONPIN 2

Servo myservo;  // create servo object to control a servo

int potpin = A1;  // analog pin used to connect the potentiometer
int mappedValue;    // variable to read the value from the analog pin

volatile int buttonState = LOW;
void changeButtonState() {
	buttonState = !buttonState;
}

// turning servo on
void mappedValueToOutputPin(int mappedValue) {
	myservo.write(mappedValue);
}
void setup() {
  myservo.attach(A2);  // attaches the servo on pin 9 to the servo object
  attachInterrupt(digitalPinToInterrupt(BUTTONPIN), changeButtonState, RISING);
}

void loop() {
	mappedValue = 0;
	// if button is pushed, get value from potentiometer, else turn led off
	if (buttonState == HIGH) {
		mappedValue = analogRead(potpin);            // reads the value of the potentiometer (value between 0 and 1023)
	}
	else {
		ledsOff();
		mappedValue = 0;
	}
	// if potentiometer value is > 500, blink green led and turn servo on, else red led on
	if (mappedValue >= 501) {
		ledsOff();
		blinkGreenLed(1000);
		mappedValue = map(mappedValue, 0, 1023, 0, 180);     // scale it to use it with the servo (value between 0 and 180)
		myservo.write(mappedValue);                  // sets the servo position according to the scaled value
	}
	else {
		solidRedLed();
	}
}
