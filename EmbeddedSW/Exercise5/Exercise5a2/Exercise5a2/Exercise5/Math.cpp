/* File: Exercise5a2
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Checking lowest significant bit of voltage value
 */
#include <Arduino.h>
#include "Math.h"

int checkBit(int checkVoltage) {
	int lsb = checkVoltage % 2; // if number is odd LSB is 1, otherwise LSB is 0
	// Returns 1 or 0
	return lsb;
}
