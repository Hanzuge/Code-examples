/* File: Languages.h
 * Authors: Hanna Salminen and Aki Rönnkvist
 * Description: Define LanguageInfo.c functions
 */

#ifndef LANGUAGES_H
#define LANGUAGES_H

/* creating struct */
struct languages {
	char language[30];
	char iso[10];
	char family[30];
	double speakers;
};

/* adding language */
void addLanguages(struct languages *, int amount);

/* print struct */
void printLanguages(struct languages *, int amount);

void languages(struct languages *, int amount);

#endif
