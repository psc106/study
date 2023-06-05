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
	

	printf("=========메인\n");

	//printf("%d\n", *p);


	//func1();
}


void func1() {
	int value1 = 1;
	int value2 = 2;
	printf("=========함수1\n");

	printf("%p\n", &value1);
	func2();

}
void func2() {
	int value1 = 1;
	int value2 = 2;
	printf("=========함수2\n");

	printf("%p\n", &value1);
	func3();

}
void func3() {
	int value1 = 1;
	int value2 = 2;
	printf("=========함수3\n");

	printf("%p\n", &value1);

}