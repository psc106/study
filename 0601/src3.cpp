#include <iostream>

const float PI = 3.141592f;

float circleArea(int);




void main() {
	{
		int decimalSize = 0;

		decimalSize = sizeof(char);
		printf("char\t: %d\n", decimalSize);	//-128+127

		decimalSize = sizeof(short);
		printf("short\t: %d\n", decimalSize);	//+-3.2만

		decimalSize = sizeof(int);
		printf("int\t: %d\n", decimalSize);		//+-21억

		decimalSize = sizeof(long);
		printf("long\t: %d\n", decimalSize);	//+-21억

		decimalSize = sizeof(long long);
		printf("llong\t: %d\n", decimalSize);	//+-900경


		printf("\n");

		int floatSize = 0;

		floatSize = sizeof(float);
		printf("float\t: %d\n", floatSize);		//7자릿수

		floatSize = sizeof(double);
		printf("double\t: %d\n", floatSize);	//15자릿수

		floatSize = sizeof(long double);
		printf("ldouble\t: %d\n", floatSize);	//15자릿수
	}
	
	//반지름 입력후 넓이
	int radius = 0;
	printf("반지름 입력 : ");
	scanf_s("%d", &radius);
	circleArea(radius);


}




float circleArea(int radius) {

	printf("넓이 : %dxPI (%f)",radius*radius, PI * radius * radius);

	return PI* radius* radius;
}