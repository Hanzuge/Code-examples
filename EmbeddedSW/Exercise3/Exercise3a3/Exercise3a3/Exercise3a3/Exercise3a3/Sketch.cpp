/*File: Exercise2a3
Authors: Hanna Salminen and Aki Rönnkvist
Description: reading temperature with thermistor and lookup table */
#include <Arduino.h>

#define THERM_PIN   A0

//Thermistor lookup Table, Small Version
const int adcTable[] = {250, 275, 300, 325, 350, 375, 400, 425, 450, 475, 500, 525, 550, 575, 600, 625, 650, 675, 700, 725, 750, 775, 800, 825, 850, 875, 900, 925, 950, 975, 1000};
const double cTable[] = {1.4, 4.0, 6.4, 8.8, 11.1, 13.4, 15.6, 17.8, 20.0, 22.2, 24.4, 26.7, 29.0, 31.3, 33.7, 36.1, 38.7, 41.3 ,44.1, 47.1 ,50.2, 53.7, 55.0, 61.5, 66.2, 71.5, 77.9, 85.7, 90.3, 96.0, 111.2, 139.5};

void setup() {

}

void loop() {


	// Rounding ADC to nearest 25
	float therm = 0.0;
	therm = analogRead(THERM_PIN);
	float rounding = (round(therm / 25))*25;
	int adcIndex = 0;
	
	// Changing ADC to C with lookup table
	for (int i = 0; i < 31; i++)
	{
		if (rounding == adcTable[i])
		{
			adcIndex = i;
			break;
		}
	}
	double temperature = cTable[adcIndex];
}