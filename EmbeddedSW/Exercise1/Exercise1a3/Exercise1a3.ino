#define LED 1-3 //connect LEDs to digital pin1-3
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
