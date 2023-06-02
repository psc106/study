#include <iostream>
#include <stdlib.h>
#include <conio.h>


//�Է� ����ó�� �Լ�
void bufferEmpty();
bool isEsterCharacter();

//��� ��� �Լ�
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


	//1�� ����

	//2�� �ֻ��� ������
	for (int i = 0; i < 2; i++) {
		diceNum = (rand() % 6) + 1;
		printf("[%d] %d \n", i + 1, diceNum);
		sum += diceNum;
	}
	
	//�� mod 2 ���� <- 0¦, 1Ȧ
	if (sum % 2 == 0) {
		printf("��(%d)�� ¦��\n", sum);
	}
	else {
		printf("��(%d)�� Ȧ��\n", sum);
	}

	printf("\n\n");


	//2�� ����
	
	//�õ� ���� ����
	time_t seedTime = time(NULL);

	//�ǵ����� ���ѷ���
	while (true) {
		//�ʱ�ȭ
		sum = 0;
		srand((unsigned)seedTime);

		//������ 2ȸ
		//1��° �����Ⱚ : �� - 2��° ��
		//2��° �����Ⱚ : 2��° ��
		for (int i = 0; i < 2; i++) {
			diceNum = (rand() % 6) + 1;
			sum += diceNum;
		}

		//�� mod 2 ���� <- 0¦, 1Ȧ
		result = sum % 2;

		//�����Է�(0~2)
		printf("Ȧ/¦?(1Ȧ, 2¦, 0����) : ");
		scanf_s("%d", &answer);

		//�����Է� ����ó��
		if (!isEsterCharacter()) {
			printf("�Է� ����\n\n");
			bufferEmpty();
			continue;
		}//[if edge]

		//�Է� ���� �ʰ���(����, 3�̻�)
		if (answer < 0 || answer > 2) {
			printf("�Է� ����\n\n");
			continue;
		}//[if edge]

		//���� �Լ�
		if (answer == 0) {
			printf("����\n");
			break;
		} //[if edge]

		//��� ���� �Ϸ��� �Է� ���� ��ó��
		answer %= 2;

		//���� ��� ����
		if (answer == result) {
			win += 1;
			printAll((sum - diceNum), diceNum, result, answer, win, lose);
			printf("����\n");
		}
		//�ٸ� ��� ����
		else {
			lose += 1;
			printAll((sum - diceNum), diceNum, result, answer, win, lose);
			printf("����\n");
		}//[else edge]

		//�õ� ����
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
	printf("[%s]", odd == 0 ? "¦" : "Ȧ");
}//[printComOdd edge]

void printMyOdd(int odd) {
	printf("�Է� : %s\n", odd == 0 ? "¦" : "Ȧ");
}//[printMyOdd edge]

void printWinRate(int win, int lose) {
	printf("\t\t\t��%d/��%d\n", win, lose);
}//[printWinRate edge]


//�Է°� �� �����Ⱚ ����
void bufferEmpty() {

	char inputRemove = 0;

	//����(\n)�� ������ ������ �ѱ��ھ� ����ش�.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge

}	//[bufferEmpty edge]


//���� �Է��� ���� ���ڰ� �����ִ��� Ȯ��
//'\n'�� �ƴϸ� ����ó��
bool isEsterCharacter() {

	char trashInput = 0;
	scanf_s("%c", &trashInput);

	return trashInput == '\n';
}	//[isEsterCharacter edge]
