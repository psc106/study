#include <iostream>
#include <string>
#include <conio.h>

void modeTest(int mode) {

	//break없으면 다음문장 실행
	switch (mode) {
	case 2346782:
		printf("2346782 -> ");
	case 1:
		printf("1 -> ");
	case 2:
		printf("2\t");
		printf("브레이크\n");
		break;
	case 3:
		printf("3 -> ");
	case 4:
		printf("4 -> ");
	case 5:
		printf("5 -> ");
	case 'a':
		printf("a -> ");
	default:
		printf("예외사항\t");
		printf("브레이크\n");
		break;
	}//modeTest switch edge

}//modeTest edge

void main() {

	int mode = 0;

	mode = 3;
	modeTest(mode);

	printf("\n\n");

	mode = 1;
	modeTest(mode);

	//한글자 입력
	mode = _getch();


	
	for (int i = 0; i < 100; i++) {
		
		printf("%d\n", i+1);

	}//for edge



	//무한 반복
	for (; 1; ) {
		printf("무한");
	}
	while (1) {
		printf("무한");
	}

}	//main edge