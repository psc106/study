#include <iostream>
#include <stdlib.h>
#include <conio.h>


//입력 예외처리 함수
void bufferEmpty();
bool isEsterCharacter();

//결과 출력 함수
void printDiceNumbers(int, int);
void printComOdd(int);
void printWinRate(int, int);
void printMyOdd(int);
void printAll(int, int, int, int, int, int);



void main() {

	int diceNum = 0;
	int sum = 0;
	int answer=0, result=0;
	int win = 0, lose = 0;

	srand((unsigned int)time(NULL));


	//1번 구현

	//2번 주사위 굴리기
	for (int i = 0; i < 2; i++) {
		diceNum = (rand() % 6) + 1;
		printf("[%d] %d \n", i + 1, diceNum);
		sum += diceNum;
	}
	
	//합 mod 2 연산 <- 0짝, 1홀
	if (sum % 2 == 0) {
		printf("합(%d)은 짝수\n", sum);
	}
	else {
		printf("합(%d)은 홀수\n", sum);
	}

	printf("\n\n");


	//2번 구현
	
	//시드 변수 선언
	time_t seedTime = time(NULL);

	//의도적인 무한루프
	while (true) {
		//초기화
		sum = 0;
		srand((unsigned)seedTime);

		//굴리기 2회
		//1번째 굴리기값 : 합 - 2번째 값
		//2번째 굴리기값 : 2번째 값
		for (int i = 0; i < 2; i++) {
			diceNum = (rand() % 6) + 1;
			sum += diceNum;
		}

		//합 mod 2 연산 <- 0짝, 1홀
		result = sum % 2;

		//숫자입력(0~2)
		printf("홀/짝?(1홀, 2짝, 0종료) : ");
		scanf_s("%d", &answer);

		//문자입력 예외처리
		if (!isEsterCharacter()) {
			printf("입력 오류\n\n");
			bufferEmpty();
			continue;
		}//[if edge]

		//입력 범위 초과시(음수, 3이상)
		if (answer < 0 || answer > 2) {
			printf("입력 오류\n\n");
			continue;
		}//[if edge]

		//종료 함수
		if (answer == 0) {
			printf("종료\n");
			break;
		} //[if edge]

		//계산 쉽게 하려고 입력 변수 후처리
		answer %= 2;

		//같을 경우 정답
		if (answer == result) {
			win += 1;
			printAll((sum - diceNum), diceNum, result, answer, win, lose);
			printf("정답\n");
		}
		//다를 경우 오답
		else {
			lose += 1;
			printAll((sum - diceNum), diceNum, result, answer, win, lose);
			printf("오답\n");
		}//[else edge]

		//시드 변경
		seedTime += sum;
		printf("\n\n");

	}//[while edge]
}//[main edge]


void printAll(int dnum1, int dnum2, int comOdd, int myOdd, int win, int lose) {

	printDiceNumbers(dnum1, dnum2);
	printComOdd(comOdd);
	printWinRate(win, lose);

	printMyOdd(myOdd);
}//[printAll edge]

void printDiceNumbers(int num1, int num2) {
	printf("%d + %d = %d", num1, num2, num1 + num2);
}//[printDiceNumbers edge]

void printComOdd(int odd) {
	printf("[%s]", odd == 0 ? "짝" : "홀");
}//[printComOdd edge]

void printMyOdd(int odd) {
	printf("입력 : %s\n", odd == 0 ? "짝" : "홀");
}//[printMyOdd edge]

void printWinRate(int win, int lose) {
	printf("\t\t\t승%d/패%d\n", win, lose);
}//[printWinRate edge]


//입력값 중 쓰레기값 삭제
void bufferEmpty() {

	char inputRemove = 0;

	//엔터(\n)가 나오기 전까지 한글자씩 비워준다.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge

}	//[bufferEmpty edge]


//현재 입력한 값에 문자가 남아있는지 확인
//'\n'이 아니면 예외처리
bool isEsterCharacter() {

	char trashInput = 0;
	scanf_s("%c", &trashInput);

	return trashInput == '\n';
}	//[isEsterCharacter edge]
