#include <iostream>

/************** 함수 전방 선언 *****************/
bool isNotSpText(char);
bool isNum(char);
bool isCapital(char);
bool isNonCapital(char);
/*********************************************/


void main() {
	/*{
	char character = 'A';


	printf("%c\n", character);
	printf("%c\n", 65);

	printf("%d\n", character);
	printf("%d\n", 65);

	bool isSame = false;

	isSame = (65 == 'A');
	printf("%d\n", isSame);


	bool isAlphabat = false;
	isAlphabat = (character >= 'a') && (character <= 'z');
	printf("%d\n", isAlphabat);
	}*/


	char inputChar;
	printf("문자 입력 : ");
	scanf_s("%c", &inputChar);							


	printf("\n");

	//결과1. 특문 확인
	isNotSpText(inputChar)?printf("특수문자 아님\n"):printf("특수문자\n\n");

	//결과2. 문자 구분
	printf("%s", isCapital(inputChar) ? "대문자" : 
				 isNonCapital(inputChar) ? "소문자" :
				 isNum(inputChar) ? "숫자" : "특수문자");

}	//main


/********************************** 함수 정의 ******************************************/
bool isNum(char inputChar) {
	return ('0' <= inputChar) && (inputChar <= '9');
}

bool isCapital(char inputChar) {
	return ('A' <= inputChar) && (inputChar <= 'Z');
}

bool isNonCapital(char inputChar) {
	return ('a' <= inputChar) && (inputChar <= 'z');
}

bool isNotSpText(char inputChar) {
	return	isCapital(inputChar) || isNonCapital(inputChar) || isNum(inputChar);
}
/************************************************************************************/