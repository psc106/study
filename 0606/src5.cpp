#include<iostream> 
#include<windows.h>

const int SHUFFLE_COUNT = 2000000000;
const int SELECTION_COUNT = 6;
const int ARRAY_LOTTO_LENGTH = 45;

void Func();
void Swap(int*, int*);
void Sort(int*, int, int);

void main() {
	srand(time(NULL));

	Func();
}	//[main] end


void Func() {
	
	//���� ����+�ʱ�ȭ
	int numbers[ARRAY_LOTTO_LENGTH] = { 0, };
	int arrLength = sizeof(numbers) / sizeof(int);

	//�迭 �� ����
	for (int i = 0; i < arrLength; i++) {
		numbers[i] = i + 1;
	}

	//�迭 ���[���� ��]
	/*for (int i = 0; i < arrLength; i++) {
		printf("%d ", numbers[i]);
	}
	printf("\n\n");*/

	//�迭 ����
	for (int i = 0; i < SHUFFLE_COUNT; i++) {
		int randInx1 = rand() % arrLength;
		int randInx2 = rand() % arrLength;
		Swap(&numbers[randInx1], &numbers[randInx2]);
	}

	Sort(numbers, 0, SELECTION_COUNT);

	//�迭 ���[���� ��]
	for (int i = 0; i < SELECTION_COUNT+1; i++) {

		Sleep(1000);
		if (i == SELECTION_COUNT) {
			Sleep(1000);
			printf("���ʽ�( %d )", numbers[i]);
		}
		else {
			printf("%d\t", numbers[i]);
		}

	}
	printf("\n\n\n");

}	//[Func] end


//����
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}	//[Swap] end

//���� ����
void Sort(int* num, int start, int length) {
	if (start == length) {
		return;
	}

	for (int i = length-1; i > start; i--) {
		if (num[i-1] > num[i]) {
			Swap(&num[i-1], &num[i]);
		}
	}
	Sort(num, start + 1, length);
}	//[Sort] end