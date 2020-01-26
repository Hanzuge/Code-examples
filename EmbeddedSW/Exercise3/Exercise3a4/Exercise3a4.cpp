/*File: Exercise2a3
Authors: Hanna Salminen and Aki Rönnkvist
Description: working with common anode 7-segment display */
#include <Arduino.h>

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
  // counterclockwise circle, 3 rounds
  for (int i = 0; i < 3; i++){
	  PinsToHigh();
	  digitalWrite(6, LOW);
	  delay(300);
	  digitalWrite(6, HIGH);
	  digitalWrite(8, LOW);
	  delay(300);
	  digitalWrite(8, HIGH);
	  digitalWrite(2, LOW);
	  delay(300);
	  digitalWrite(2, HIGH);
	  digitalWrite(3, LOW);
	  delay(300);
	  digitalWrite(3, HIGH);
	  digitalWrite(4, LOW);
	  delay(300);
	  digitalWrite(4, HIGH);
	  digitalWrite(5, LOW);
	  delay(300);
	  digitalWrite(5, HIGH);
  }
  // turn all leds on and then off one at time, 3 rounds
 for (int i = 0; i < 3; i++){
	 PinsToLow();
	 delay(500);
	 for (int a = 2; a < 9; a++)
	 {
		 digitalWrite(a, HIGH);
		 delay(500);
	 } 
 } 
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
}


