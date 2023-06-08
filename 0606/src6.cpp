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


//ī�� �̴� �Լ�
void CardFunc() {


	//a2345 67890 jqk
	//Ŭ�ι�, �����̵�, ��Ʈ, ���̾�
	int deck[52] = { 0, };

	//�� ����(0~51)
	for (int i = 0; i < 52; i++) {
		deck[i] = i;
	}

	//����
	for (int i = 0; i < 100; i++) {
		Swap(&deck[rand() % 52], &deck[rand() % 52]);
	}

	//���� ������� ���
	for (int i = 0; i < 52; i++) {
		PrintCard(deck[i]);
		_getch();
	}

}// [CardFunc] end


//ī�� �̹��� ���
void PrintCard(int deck) {
	const char pattern[4][4] = { "��","��","��","��" };
	const char number[13][4] = { "A ", "2 ", "3 ", "4 ", "5 ",
								 "6 ", "7 ", "8 ", "9 ", "10",
								 "J ", "Q ", "K " };

	int patternNum = (int)(deck / 13);

	printf("����������������������������������\n");
	printf("�� %s%s          ��\n", pattern[patternNum], number[deck % 13]);
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��               ��\n");
	printf("��           %s%s��\n", pattern[patternNum], number[deck % 13]);
	printf("����������������������������������\n\n");
}// [PrintCard] end


//����
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}// [Swap] end

