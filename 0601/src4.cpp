#include <iostream>

/************** �Լ� ���� ���� *****************/
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
	printf("���� �Է� : ");
	scanf_s("%c", &inputChar);							


	printf("\n");

	//���1. Ư�� Ȯ��
	isNotSpText(inputChar)?printf("Ư������ �ƴ�\n"):printf("Ư������\n\n");

	//���2. ���� ����
	printf("%s", isCapital(inputChar) ? "�빮��" : 
				 isNonCapital(inputChar) ? "�ҹ���" :
				 isNum(inputChar) ? "����" : "Ư������");

}	//main


/********************************** �Լ� ���� ******************************************/
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