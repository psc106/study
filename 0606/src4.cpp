#include<iostream>

//������=�迭
void asd(int* arr) {
	printf("1");
	arr[0] = 1;
}

//������ ����
void asd2(int arr[]) {
	printf("2");
	arr[0] = 3;
}

void main() {

	int dec = 0;
	int arr[] = {0, 23, 24, 35, 42, 51 };
	int *p = &dec , *arrp = arr;
	
	asd2(arr);
	
	for (int i = (sizeof(arr) / sizeof(int))-1; i >=0  ; i--)
	{
		printf("%d \n", *(arrp+i));
		printf("%d \n", arrp[i]);
	}

	asd(arrp);

	for (int i = (sizeof(arr) / sizeof(int)) - 1; i >= 0; i--)
	{
		printf("%d \n", *(arrp + i));
		printf("%d \n", arrp[i]);
	}

}