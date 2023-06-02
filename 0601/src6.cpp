#include <iostream>

bool isRight(int num) {
	return num >= 3 && num <= 10;
}

void main() {

	int count = 0;

	//입력(3~10)
	while (!isRight(count)) {
		printf("입력(3~10) : ");
		scanf_s("%d", &count);

		//아니면 다시 입력
		if (!isRight(count)) {
			printf("다시 입력하세요\n");
		}
	}

	//반복문 2개
	//수직횟수x수평횟수
	//첫번째 반복문 : 수직
	//두번째 반복문 : 수평
	
	//수직을 n번 수행한다.
	int vertical = 0;
	while (vertical < count) {
		//첫 문자 숫자 출력
		printf("%2d. \t", vertical + 1);

		//수평을 n번 수행한다.
		int horizen = 0;
		while (horizen < count) {
			printf("* ");
			horizen++;
		}

		//수평 모두 입력후 다음 문단으로 이동
		printf("\n");
		vertical++;
	}

	printf("\n\n");

	//반복문 1개
	//n*n번 출력하는 형태
	int newCount = count * count;
	int horizen = 0;
	while (horizen < newCount) {
		//첫 문자 숫자 출력
		if (horizen % count == 0) {
			printf("%2d.\t", (horizen / count) + 1);
		}

		//무조건 n*n번 출력
		printf("* ");

		//마지막 문자 다음 문단으로
		if (horizen % count == count - 1) {
			printf("\n");
		}

		horizen++;
	}


}