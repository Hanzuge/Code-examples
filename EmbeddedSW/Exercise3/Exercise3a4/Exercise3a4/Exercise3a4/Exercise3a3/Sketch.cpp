/*File: Exercise3a4
Authors: Hanna Salminen and Aki Rönnkvist
Description: reading temperature with thermistor and lookup table and working with common anode 7-segment display */
#include <Arduino.h>
#define THERM_PIN   A0

//Thermistor lookup Table, Small Version
const int adcTable[] = {250, 275, 300, 325, 350, 375, 400, 425, 450, 475, 500, 525, 550, 575, 600, 625, 650, 675, 700, 725, 750, 775, 800, 825, 850, 875, 900, 925, 950, 975, 1000};
const double cTable[] = {1.4, 4.0, 6.4, 8.8, 11.1, 13.4, 15.6, 17.8, 20.0, 22.2, 24.4, 26.7, 29.0, 31.3, 33.7, 36.1, 38.7, 41.3 ,44.1, 47.1 ,50.2, 53.7, 55.0, 61.5, 66.2, 71.5, 77.9, 85.7, 90.3, 96.0, 111.2, 139.5};


// All leds on
void PinsToLow(){
	digitalWrite(2, LOW);
	digitalWrite(3, LOW);
	digitalWrite(4, LOW);
	digitalWrite(5, LOW);
	digitalWrite(6, LOW);
	digitalWrite(7, LOW);
	digitalWrite(8, LOW);
}

// All leds off
void PinsToHigh(){
	digitalWrite(2, HIGH);
	digitalWrite(3, HIGH);
	digitalWrite(4, HIGH);
	digitalWrite(5, HIGH);
	digitalWrite(6, HIGH);
	digitalWrite(7, HIGH);
	digitalWrite(8, HIGH);
}

// Setting leds to numbers
void numbers(int digit){
	switch (digit) {
		case 1:
		digitalWrite(2, HIGH);
		digitalWrite(3, HIGH);
		digitalWrite(4, HIGH);
		digitalWrite(5, HIGH);
		digitalWrite(6, LOW);
		digitalWrite(7, HIGH);
		digitalWrite(8, LOW);
		break;
		case 2:
		digitalWrite(2, LOW);
		digitalWrite(3, HIGH);
		digitalWrite(4, LOW);
		digitalWrite(5, LOW);
		digitalWrite(6, HIGH);
		digitalWrite(7, LOW);
		digitalWrite(8, LOW);
		break;
		case 3:
		digitalWrite(2, LOW);
		digitalWrite(3, HIGH);
		digitalWrite(4, HIGH);
		digitalWrite(5, LOW);
		digitalWrite(6, LOW);
		digitalWrite(7, LOW);
		digitalWrite(8, LOW);
		break;
		case 4:
		digitalWrite(2, HIGH);
		digitalWrite(3, LOW);
		digitalWrite(4, HIGH);
		digitalWrite(5, HIGH);
		digitalWrite(6, LOW);
		digitalWrite(7, LOW);
		digitalWrite(8, LOW);
		break;
		case 5:
		digitalWrite(2, LOW);
		digitalWrite(3, LOW);
		digitalWrite(4, HIGH);
		digitalWrite(5, LOW);
		digitalWrite(6, LOW);
		digitalWrite(7, LOW);
		digitalWrite(8, HIGH);
		break;
		case 6:
		digitalWrite(2, LOW);
		digitalWrite(3, LOW);
		digitalWrite(4, LOW);
		digitalWrite(5, LOW);
		digitalWrite(6, LOW);
		digitalWrite(7, LOW);
		digitalWrite(8, HIGH);
		break;
		case 7:
		digitalWrite(2, LOW);
		digitalWrite(3, HIGH);
		digitalWrite(4, HIGH);
		digitalWrite(5, HIGH);
		digitalWrite(6, LOW);
		digitalWrite(7, HIGH);
		digitalWrite(8, LOW);
		break;
		case 8:
		PinsToLow();
		break;
		case 9:
		digitalWrite(2, LOW);
		digitalWrite(3, LOW);
		digitalWrite(4, HIGH);
		digitalWrite(5, LOW);
		digitalWrite(6, LOW);
		digitalWrite(7, LOW);
		digitalWrite(8, LOW);
		break;
		case 0:
		digitalWrite(2, LOW);
		digitalWrite(3, LOW);
		digitalWrite(4, LOW);
		digitalWrite(5, LOW);
		digitalWrite(6, LOW);
		digitalWrite(7, HIGH);
		digitalWrite(8, LOW);
		break;
	}
}

void setup() {
	// setup pins
	pinMode(2, OUTPUT);
	pinMode(3, OUTPUT);
	pinMode(4, OUTPUT);
	pinMode(5, OUTPUT);
	pinMode(6, OUTPUT);
	pinMode(7, OUTPUT);
	pinMode(8, OUTPUT);
	
}

void loop() {
	// blinking number 3 and letter E, 3 rounds
	for (int i = 0; i < 3; i++){
		PinsToLow();
		digitalWrite(3, HIGH);
		digitalWrite(4, HIGH);
		delay(1000);
		PinsToLow();
		digitalWrite(8, HIGH);
		digitalWrite(6, HIGH);
		delay(1000);
	}
	// 2 sec delay without leds
	PinsToHigh();
	delay(2000);
	
	// Rounding ADC to nearest 25
	float therm = 0.0;
	therm = analogRead(THERM_PIN);
	float rounding = (round(therm / 25))*25;
	
	// Changing ADC to Celsius with lookup table
	int adcIndex = 0;
	for (int i = 0; i < 31; i++)
	{
		if (rounding == adcTable[i])
		{
			adcIndex = i;
			break;
		}
	}
	int temperature = floor(cTable[adcIndex]);

	// First digit of temperature
	int digit1 = floor(temperature / 10);
	numbers(digit1);	
	delay(1000);
	
	// 2 sec delay without leds
	PinsToHigh();
	delay(2000);	
	
	// Second digit of temperature
	int digit2 = temperature % 10;
	numbers(digit2);
	delay(1000);
	
	// 2 sec delay without leds
	PinsToHigh();
	delay(2000);	
	
	// clockwise circle, 3 rounds
	for (int i = 0; i < 3; i++){
		PinsToHigh();
		digitalWrite(6, LOW);
		delay(300);
		digitalWrite(6, HIGH);
		digitalWrite(5, LOW);
		delay(300);
		digitalWrite(5, HIGH);
		digitalWrite(4, LOW);
		delay(300);
		digitalWrite(4, HIGH);
		digitalWrite(3, LOW);
		delay(300);
		digitalWrite(3, HIGH);
		digitalWrite(2, LOW);
		delay(300);
		digitalWrite(2, HIGH);
		digitalWrite(8, LOW);
		delay(300);
		digitalWrite(8, HIGH);
	}
	
	// 2 sec delay without leds
	PinsToHigh();
	delay(2000);	
}

