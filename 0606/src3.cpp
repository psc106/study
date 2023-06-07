#include<iostream>

void Swap(char* first, char* second) {

	char tmp = *first;
	*first = *second;
	*second = tmp;

}
void Swap(char first, char second) {

	char tmp = first;
	first = second;
	second = tmp;

}

void main() {

	char str[10] = "hello";
	printf("%s\n", str);

	Swap(&str[0], &str[4]);
	printf("%s\n", str);

	char tmp = '\0';
	tmp = str[4];
	str[4] = str[0];
	str[0] = tmp;
	printf("%s\n", str);

	Swap(str[0], str[4]);
	printf("%s\n", str);

}