#include<stdio.h>

void printAge(int num) {

	
	printf("%d\n", num);
	return;
}


int hamsu(int num) {

	return num-1;
}



int main() {
	int phoneSecond, phoneThird;

	phoneSecond = 5580;

	phoneThird = 4310;

	printf("박성철\n");
	printAge(33);
	printf("010-%d-%d\n", phoneSecond, phoneThird);


	printf("이름 : 박성철\n");
	printf("나이 : 323\n");
	printf("만나이 : %d살\n", hamsu(323));
	printf("전화번호 : 010-5580-4310\n\n");

	printf("이름 : 박준오\n");
	printf("나이 : 100\n");
	printf("만나이 : %d살\n", hamsu(100));
	printf("전화번호 : 020-3033-3333\n\n");

	return 0;
}
