/* File: Exercise3.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Reading a file and adding numbers in elementary school style
 */


#include <stdio.h>
int addition(int readBuffer[][100]) {
	int result[51];
	int temp = 0;
	for(int i=49; i>=0; i--) {
		for(int a=0; a<100; a++) {
			temp += readBuffer[i][a];
		}
		result[i + 1] = temp % 10;
		temp = temp - temp % 10;
		temp = temp / 10;
	}
	result[0] = temp;
	printf("Result is: ");
	for(int i = 0; i < 51; i++) {
		printf("%d", result[i]);
	}
	printf("\n");
	return 0;
}

int main() {
	FILE *filePointer = NULL;
	int readBuffer[50][100];
	filePointer = fopen("Numbers.txt", "r");
	
	for(int a=0; a < 100; a++) {
		for(int i=0; i < 50; i++) {
			fscanf(filePointer, "%1d", &readBuffer[i][a]);
		}
	}
	addition(readBuffer);
	fclose(filePointer);
	return 0;
}


