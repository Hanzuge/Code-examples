/* File: Exercise4.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: 
 */

#include <stdio.h> 
#include <ctype.h>
#include <stdlib.h>
#include <time.h>

int main(int argc, char *argv[]) {
	/* warning if the user don't give any arguments */
	if(argc <= 1) {
		printf("You did not give a integer");	
	}
	/* warning if the user gives multiple arguments */
	else if(argc >= 3) {
		printf("You can only give one integer");
	}
	else {
		char *ptr = '\0';
		long result = strtol(argv[1], &ptr, 10);
		/* warning if the user gives something else than integer */
		if ((ptr == argv[1]) || (*ptr != '\0')) {
			printf ("'%s' is not valid, give only integer\n", argv[1]);
 		}
		/* when the user gives integer: */
		else {
			int *randomIntegers = NULL;
			randomIntegers = (int*) malloc(result*sizeof(int));
			if(randomIntegers == NULL) { 
				printf("ERROR; memory allocation did not succeed.");	
			}
			else {
				srand(time(NULL));
				for(int i = 0; i < result; i++) {
					int random = rand() % 10 + 1;
					randomIntegers[i] = random;
					printf("%d\n", randomIntegers[i]);
				}
			}
		}
	}
}
