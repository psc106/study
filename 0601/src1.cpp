#include <iostream>




void main() {

	int number1=0, number2=0;
	int result = 0;

	printf("2개 입력(띄어쓰기로 구분) : ");
	scanf_s("%d %d", &number1, &number1);
	scanf_s("%d%d", &number2, &number2);

	result = number1 + number2;

	printf("출력 : %d + %d = %d", number1, number2, result);

	
	return;
}	//main()