#include <iostream>
#include <conio.h>
#include <windows.h>

void CardFunc();
void PrintCard(int);
void Swap(int*, int*);

void main() {
	srand((unsigned)time(NULL));
	CardFunc();
}


//카드 뽑는 함수
void CardFunc() {


	//a2345 67890 jqk
	//클로버, 스페이드, 하트, 다이아
	int deck[52] = { 0, };

	//값 대입(0~51)
	for (int i = 0; i < 52; i++) {
		deck[i] = i;
	}

	//셔플
	for (int i = 0; i < 100; i++) {
		Swap(&deck[rand() % 52], &deck[rand() % 52]);
	}

	//제일 앞장부터 출력
	for (int i = 0; i < 52; i++) {
		PrintCard(deck[i]);
		_getch();
	}

}// [CardFunc] end


//카드 이미지 출력
void PrintCard(int deck) {
	const char pattern[4][4] = { "♣","♠","♥","◆" };
	const char number[13][4] = { "A ", "2 ", "3 ", "4 ", "5 ",
								 "6 ", "7 ", "8 ", "9 ", "10",
								 "J ", "Q ", "K " };

	int patternNum = (int)(deck / 13);

	printf("┌───────────────┐\n");
	printf("│ %s%s          │\n", pattern[patternNum], number[deck % 13]);
	printf("│               │\n");
	printf("│               │\n");
	printf("│               │\n");
	printf("│               │\n");
	printf("│               │\n");
	printf("│               │\n");
	printf("│               │\n");
	printf("│               │\n");
	printf("│           %s%s│\n", pattern[patternNum], number[deck % 13]);
	printf("└───────────────┘\n\n");
}// [PrintCard] end


//스왑
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}// [Swap] end

