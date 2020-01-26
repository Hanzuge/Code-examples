/* File: Exercise5.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: 
 */

#include <stdio.h>
#include <stdlib.h>


char *decimal_to_binary(int);
 
main() {
	int number = 0;
	int t = 0;
	char *pointer;
	   
	printf("Enter an integer\n");
	scanf("%d",&number);
	   
	pointer = decimal_to_binary(number);
	printf("Binary string of %d is: %d\n", number, t);
   
	free(pointer);
	   
	return 0;
}
 
char *decimal_to_binary(int number) {
	int count = 0;
	char *pointer;
	   
	pointer = (char*)malloc(32+1);
	   
	if (pointer == NULL)
		exit(EXIT_FAILURE);
     
	for (int i = 31 ; i >= 0 ; i--) {
		int d = number >> i;
     
		if (d & 1)
			*(pointer+count) = 1 + '0';
		else
			*(pointer+count) = 0 + '0';
     
		count++;
	}
	*(pointer+count) = '\0';
   
	return  pointer;
}
