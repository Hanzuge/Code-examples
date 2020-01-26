/* File: Exercise1.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Practicing loops
 */

#include <stdio.h>

/* Do not change these. */
void oddNumbers(int lower, int upper);
void divisibleNumbers();
void sumOfMultiples();


/* Do not change main function at all. */
int main() {

	printf("Print out odd numbers between 40 and 0:\n");

	oddNumbers(0, 40);

	printf("\n\nPrint out numbers divisible by both 2 and 3 between 0 and 100:\n");

	divisibleNumbers();

	printf("\n\nPrint out the sum of multiples of 4 and 7 between 0 and 999:\n");

	sumOfMultiples();

	return 0;

}

void oddNumbers(int lower, int upper) {
	
	int i = 0;
	for(i = upper; i > lower; i--)
	{
		/* prints out all odd numbers from 40 to 0 */
		if( i % 2 == 1)
		{
			printf("%d\n", i);
		}	
	}
}

void divisibleNumbers() {

	int a = 0;
	for(a = 0; a <= 100; a++)
	{
		/* print all numbers between 0 and 100 that are divisible by 2 and 3 */
		if( a % 2 == 0 && a % 3 == 0)
		{
			printf("%d\n", a);
		}	
	}

}

void sumOfMultiples() {
	
	int u = 0;
	int sum = 0;
	for(u = 0; u <= 999; u++)
	{
		/* if u is multiple of 4, add to sum */
		if( u % 4 == 0 )
		{
			sum = sum + u;
		}
		/* if u is multiple of 7, add to sum */	
		else if( u % 7 == 0 )
		{
			sum = sum + u;
		}
	}
	/* print sum of multiples of 4 and 7 */
	printf("%d\n", sum);
}
