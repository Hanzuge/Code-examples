/* File: Exercise2.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: functions, loops and conditional statements
 */

#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int randomNumber();
void collatzSequence(int number, int *counter);
void primeNumber(int counter);



int main() {
	int random = 0;
	int counter = 1;
	randomNumber(&random);
	collatzSequence(random, &counter);
	primeNumber(counter);
	return 0;

}

int randomNumber(int *random) {
	/* Get new seed for random generator */
	srand(time(NULL));
	/* Random number between 0 and 10000 */
	*random = rand() % (10001);
	printf("Random number: %d\n", *random);
	return *random;
}

void collatzSequence(int number, int *counter) {
	
	int exitLoop = 0;
	printf("Collatz sequenced for %d is: ", number);
	while(exitLoop == 0)
	{
		if(number == 1)
		/* print last number at the end of the sequence and the length then exit the loop*/
		{
			printf("%d", number);
			printf("\nLength of the sequence is: %d\n", *counter);
			exitLoop = 1;
		}
		else if(number > 0)
		{
			if(number % 2 == 0) 
			/* if number is even*/
			{
				printf("%d - ", number);
				number = number / 2;
				*counter = *counter + 1;
			}
			else
			/* if number is odd */
			{
				printf("%d - ", number);
				number = 3 * number + 1;
				*counter = *counter + 1;
			}
		}
		else
		/* tells the user that there has been error */
		{
			printf("0");
			printf("\nNumber can't be zero or negative\n");
			exitLoop = 1;
		}
	}	
}

void primeNumber(int counter) {

	int flag = 0;
	for(int i = 2; i <= counter/2; ++i)
	/* checks if the number can be divided by anything other than itself (divided by 2) and 1 */
	{
		if(counter%i == 0)
		{
			flag = 1;
			break;
		}
	}

	if (counter == 1 || counter == 0)
	/* case 1 and 0 */
	{
		printf("%d is neither a prime nor a composite number\n", counter);
	}
	else
	{
	if (flag == 0)
	/* case prime number */
		{
		printf("%d is a prime number\n", counter);
		}
	else
	/* case not prime number */
		{
		printf("%d is not a prime number\n", counter);
		}
	}
}
