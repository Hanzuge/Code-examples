/* File: Exercise5a2
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Getting ADC from thermistor and changing it to Celcius 
 */
#include <Arduino.h>
#include "Thermistor.h"

double getTemperature() {
	
		// Rounding ADC to nearest 25
		float therm = 0.0;
		therm = analogRead(THERMISTORPIN);
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
		// Returns temperature
		double temperature = cTable[adcIndex];
		return temperature; 
}

int getVoltage() {
	
	// Getting ADC from thermistor
	int thermistor = 0;
	thermistor = analogRead(THERMISTORPIN);
	return thermistor;
}