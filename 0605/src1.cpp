#include <iostream> 

void func1();
void func2();
void func3();

void func(int* a, int b) {

	(*a)++;
	b++;
}

static int a = 0;

void main() {

	int value1=0;
	int value2=0;
	int* p = NULL;

	func(&value1, value2);

	printf("%#x %p\n", &value1, &value1);
	printf("%d %d\n", value1, value2);
	

	printf("=========����\n");

	//printf("%d\n", *p);


	//func1();
}


void func1() {
	int value1 = 1;
	int value2 = 2;
	printf("=========�Լ�1\n");

	printf("%p\n", &value1);
	func2();

}
void func2() {
	int value1 = 1;
	int value2 = 2;
	printf("=========�Լ�2\n");

	printf("%p\n", &value1);
	func3();

}
void func3() {
	int value1 = 1;
	int value2 = 2;
	printf("=========�Լ�3\n");

	printf("%p\n", &value1);

}