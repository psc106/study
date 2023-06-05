#include <iostream>
#include <array>

const int MAX_ARRAY = 5;

void Func1() {

	char char_ = 'a';
	int int_ = char_;

	float f = 100.123;

	int_ = f;

	printf("%f\n", (float)int_);//100.00;

}

void Func2() {

	int arr[MAX_ARRAY] = {0,};

	printf("arr :\t %#x\n", arr);
	printf("arr :\t %#x\n", &*(arr+1));

	for (int j = 0; j < sizeof(arr) / sizeof(int); j++) {
		printf("arr[%d] : %#x\n", j, &arr[j]);
	}
	
	for (int i = 0; i < MAX_ARRAY; i++) {
		arr[i] = i + 1;
	}

	for (int j = 0; j < sizeof(arr)/ sizeof(int); j++) {
		printf("arr[%d] : %d\n", j, arr[j]);
	}

	int num1 = 1;
	int num2 = 1;
	int num3 = 1;
	int num4 = 1;
	int num5 = 1;

	printf("num : %#x\n", &num1);

	printf("num : %#x\n", &num2);
	printf("num : %#x\n", &num3);
	printf("num : %#x\n", &num4);
	printf("num : %#x\n", &num5);
}

void main() {

	Func1();
	Func2();
}