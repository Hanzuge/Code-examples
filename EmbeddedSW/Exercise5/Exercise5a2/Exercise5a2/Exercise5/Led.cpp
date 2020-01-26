/* File: Exercise5a2
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Turning LEDs on, off and blinking them
 */
#include <Arduino.h>
#include "Led.h"

#define REDLED 2
#define REDLED2 4
#define YELLOWLED 1
#define GREENLED 3

bool state = 0;

// Green LED on
void greenLedOn() {
	digitalWrite(GREENLED, HIGH);
	delay(1000);
}
// Red LED on
void redLedOn() {
	digitalWrite(REDLED, HIGH);
	delay(1000);
}
// Red LED 2 on and off
void redLed2OnOff() {
	digitalWrite(REDLED2, state);
	state = !state;
}
// yellow Led blinks
void yellowLedBlink(int delay1) {
	digitalWrite(YELLOWLED, HIGH);
	delay(delay1);
	digitalWrite(YELLOWLED, LOW);
	delay(delay1);
}
// Green and red LEDs off
void ledOff() {
	digitalWrite(GREENLED, LOW);
	digitalWrite(REDLED, LOW);
}