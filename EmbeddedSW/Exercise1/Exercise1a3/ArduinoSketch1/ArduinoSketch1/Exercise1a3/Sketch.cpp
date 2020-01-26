/*File: Exercise1a3
Authors: Hanna Salminen and Aki Rönnkvist
Description: Coding traffic light in Arduino */
#include <Arduino.h>

/*End of auto generated code by Atmel studio */


//Beginning of Auto generated function prototypes by Atmel Studio
//End of Auto generated function prototypes by Atmel Studio

#define LED 1 //connect LEDs to digital pin1-3
#define LED 2
#define LED 3
void setup() {                
 // initialize the digital pin1-3 as an outputs.
 pinMode(1, OUTPUT); // Red
 pinMode(2, OUTPUT); // Yellow
 pinMode(3, OUTPUT); // Green
}

void loop() {
 digitalWrite(1, HIGH);   // set the red LED on
 delay(3000);             // for 3s
 digitalWrite(2, HIGH);	 // set the yellow LED on
 delay(1000);			 // for 1s
 digitalWrite(1, LOW);   // set the red LED off
 digitalWrite(2, LOW);	 // set the yellow LED off
 digitalWrite(3, HIGH);	 // set the green LED on
 delay(3000);			 // for 3s
 digitalWrite(3, LOW);	 // set the green LED off
 digitalWrite(2, HIGH);	 // set the yellow LED on
 delay(1000);			 // for 1s
 digitalWrite(2, LOW);	 // set the yellow LED off
}
