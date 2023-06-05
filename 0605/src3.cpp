#include <iostream>

const int COUNT_MAX = 9;


void Calc(int arr1[], int arr2[]) {

	for (int i = 0; i < 9; i++) {
		for (int j = 0; j < 9; j++) {
			printf("%d x %d = %d\t", i + 1, j + 1, arr1[i] * arr2[j]);
		}
		printf("\n");
	}
}

void main() {

	int firstCount[COUNT_MAX] = { 0, };
	int secondCount[COUNT_MAX] = { 0, };

	for (int i = 0; i < sizeof(firstCount) / sizeof(int); i++) {
		firstCount[i] = i + 1;
	}
	for (int i = 0; i < sizeof(secondCount) / sizeof(int); i++) {
		secondCount[i] = i + 1;
	}


	Calc(firstCount, secondCount);

}