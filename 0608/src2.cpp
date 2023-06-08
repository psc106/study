#include <iostream>


void Func();
void PrintArray(int* arr, int length);

void main() {

	Func();

}

void Func() {

	const int ARR_NUM_LENGTH = 10;

	int num[10] = { 0, };
	int* ptr = num;

	for (int i = 0; i < ARR_NUM_LENGTH; i++) {
		num[i] = i * 10;
	}

	for (int i = 0; i < ARR_NUM_LENGTH; i++) {
		printf("%d ", num[i]);
	}
	printf("\n");

	printf("3¹øÂ°[ %d ] + 1 = %d\n", *(ptr + 2), *(ptr + 2) + 1);

	PrintArray(num, sizeof(num) / sizeof(int));
}

void PrintArray(int* arr, int length) {
	for (int i = 0; i < length; i++) {
		printf("%d ", *(arr + i));
	}
}