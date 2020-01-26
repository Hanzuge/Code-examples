/* File: Exercise5a2b
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Blinking leds
 */
#include <Arduino.h>
#include "Led.h"

#define REDLED 1
#define GREENLED 3

// leds off
void ledsOff() {
	digitalWrite(REDLED, LOW);
	digitalWrite(GREENLED, LOW);
}
// blinking green led
void blinkGreenLed(int delay1) {
	digitalWrite(GREENLED, HIGH);
	delay(delay1);
	digitalWrite(GREENLED, LOW);
	delay(delay1);
}
// red led on	
void solidRedLed() {
	digitalWrite(REDLED, HIGH);
}