/* File: Exercise5a2
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Learning Arduino timer interrupts and using multiple files  
 */
#include <Arduino.h>
#include "Thermistor.h"
#include "Led.h"
#include "Math.h"

void setup() {
	pinMode(1, OUTPUT);
	pinMode(2, OUTPUT);
	pinMode(3, OUTPUT);
	pinMode(4, OUTPUT);
	
	cli(); //stop interrupts

	//set timer1 interrupt at 0,5Hz
	TCCR1A = 0; // set entire TCCR1A register to 0
	TCCR1B = 0; // same for TCCR1B
	TCNT1 = 0;  //initialize counter value to 0
	// set compare match register for 0,5Hz increments
	OCR1A = 31249; // = (16*10^6) / (0,5*1024) - 1 (must be <65536)
	// turn on CTC mode
	TCCR1B |= (1 << WGM12);
	// Set CS10 and CS12 bits for 1024 prescaler
	TCCR1B |= (1 << CS12) | (1 << CS10);
	// enable timer compare interrupt
	TIMSK1 |= (1 << OCIE1A);

	sei(); //allow interrupts
}

ISR(TIMER1_COMPA_vect) { //timer1 interrupt 0,5Hz toggles Red LED 2
	redLed2OnOff();
}

void loop() {
	double c = getTemperature();
	// If temperature is under 25, green LED on, otherwise red LED on
	if (c < 25.0) {
		greenLedOn();
	}
	else {
		redLedOn();
	}
	// LEDs off
	ledOff();

	int voltage = getVoltage();
	int bit = checkBit(voltage);
	// yellow blinks 5 times
	for (int i = 0; i < 5; i++) {
		if (bit == 1) {
			yellowLedBlink(1000);
		}
		else {
			yellowLedBlink(200);
		}
	}
}
