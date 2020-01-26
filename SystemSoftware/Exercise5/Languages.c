/* File: Languages.c
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Add language information to struct
 */

#include <stdio.h>
#include "Languages.h"

// Filling the struct
void addLanguages(struct languages *ptr, int amount) {
	getchar();
	for (int i = 0; i < amount; i++) {
		//getchar();
		char buffer[10];
		// asking name of the language
		printf("\nEnter language name: ");
		fgets((ptr + i) -> language, 30, stdin);
		// if empty
		while ((ptr + i) -> language[0] == '\n') {
			printf("Input for language name cannot be empty!\n");
			printf("\nEnter language name: ");
			fgets((ptr + i) -> language, 30, stdin);
		}
		// asking ISO 639-1 code
		printf("Enter ISO 639-1 code: ");
		fgets((ptr + i) -> iso, 10, stdin);
		// asking language family
		printf("Enter family: ");
		fgets((ptr + i) -> family, 30, stdin);
		// asking number of speakers
		printf("Enter number of speakers in millions: ");
		fgets(buffer, 10, stdin);
		if (buffer[0] != '\n') {
			sscanf(buffer, "%lf", &(ptr + i) -> speakers);
			continue;
		}
	}
}
// printing the struct
void printLanguages(struct languages *ptr, int amount) {
	printf("\n*** LIST OF LANGUAGES ***\n\n");
	for (int i =0; i < amount; i++) {
		printf("Name:\t\t%s", (ptr + i) -> language);
		printf("Code:\t\t%s", (ptr + i) -> iso);
		printf("Family:\t\t%s", (ptr + i) -> family);
		printf("Speakers:\t%3.1f", (ptr + i) -> speakers);
		printf("\n\n");
	}
}
void languages(struct languages *ptr1, int amount) {
	addLanguages(ptr1, amount);
	printLanguages(ptr1, amount);
}


