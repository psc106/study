#include <iostream>

void main() {

	char string_1[10] = "123456789";
	char string_2[] = "1234567890";
	char* string_3 = string_1;

	printf("%d\n", sizeof(string_1));
	printf("%d\n", sizeof(string_2));
	printf("%d\n", sizeof(*string_3));

	char str[300];
	std::cin >> str;
	printf("%s\n", str);

}