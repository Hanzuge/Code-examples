/* File: Exercise5.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: 
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>
#include "Languages.h"

/*int main() {
	struct languages *ptr1 = NULL;
	int amount = 0;
	printf("How many languages do you want to add?: ");
	scanf("%d", &amount);
	ptr1 = (struct languages*) malloc (amount * sizeof(struct languages));
	addLanguages(ptr1, amount);
	printLanguages(ptr1, amount);
	free(ptr1);	
	return 0;
}*/

int main() {
	/* creating struct and variables */
	int amount, length, test = 0;
	char input[15] = "";
	struct languages *ptr1 = NULL;
	/* asks the number of the languages to be added */
	while(!test) {
		printf("How many language you want to add? ");
		scanf("%s", input);
		char *pointer = input;
		length = strlen (input);
		for (int i = 0; i < length; i++) {
			if (!isdigit(*(pointer + i)))
			{
				/* Error message if other than number */
				printf ("\nInput was invalid, give only one integer.\n\n");
				break;
			}
			else {
				/* quit while loop */
				test = 1;
			}
		}
	}
	sscanf(input, "%d", &amount);
	/* allocate memory */
	ptr1 = (struct languages*) malloc(amount*sizeof(struct languages));
	/* if memory allocation didn't succeed */
	if(ptr1 == NULL) { 
		printf("ERROR: memory allocation did not succeed.\n");	
	}
	else {
		languages(ptr1, amount);
	}
	/* free memory */
	free(ptr1);
	return 0;
}
