#include <iostream>

bool isNum(char);
bool isCapital(char);
bool isNonCapital(char);
bool isNotSpText(char);

bool isRight(int num) {
	return num >= 3 && num <= 10;
}

void main() {
	////입력
	//char character = 0;
	//scanf_s("%c", &character);


	////조건문
	//if (isCapital(character)) {
	//	printf("대문자\n");
	//}
	//else if (isNonCapital(character)) {
	//	printf("소문자\n");
	//}
	//else if (isNum(character)) {
	//	printf("숫자\n");
	//}
	//else {
	//	printf("특수문자\n");
	//}

	////while문1
	//printf("\n");
	//int loop = 100, loopOut = 0;
	//while (loop> loopOut) {
	//	printf("%3d. hello\n",101-loop--);
	//}	//while 종료

	////while문2
	//printf("\n");
	//loop = 0; loopOut = 10;
	//while (loop < loopOut) {
	//	printf("%2d\n", ++loop);
	//}

	int count=0;
	
	while (count < 3 || count > 10) {
		printf("입력(3~10) : ");
		scanf_s("%d", &count);

		if (!isRight(count)) {
			printf("다시 입력하세요\n");
		}
	}

	//반복문 2개
	int vertical = 0;
	while (vertical < count) {
		int horizen = 0;

		printf("%d. \t", vertical+1);
		while (horizen <count) {
			printf("*");
			horizen++;
		}
		printf("\n");
		vertical++;
	}
	printf("\n\n");

	//반복문 1개
	int newCount = count * count;
	int horizen = 0;
	while (horizen < newCount) {
		if (horizen % count == 0) {
			printf("%d.\t",(horizen/count)+1);
		}
		if (horizen % count != count-1) {
			printf("*");
		}
		else {
			printf("\n");
		}
		horizen++;
	}



	//for문
/*	printf("\n");
	for (int j = 0; j < 10; j++) {
		printf("%d\n", j + 1);
	}	*///for 종료


}

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