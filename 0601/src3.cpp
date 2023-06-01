#include <iostream>

const float PI = 3.141592f;

float circleArea(int);


void main() {

	//������ Ÿ�� ũ��
	{
		int decimalSize = 0;

		decimalSize = sizeof(char);
		printf("char\t: %d\n", decimalSize);	//-128+127

		decimalSize = sizeof(short);
		printf("short\t: %d\n", decimalSize);	//+-3.2��

		decimalSize = sizeof(int);
		printf("int\t: %d\n", decimalSize);		//+-21��

		decimalSize = sizeof(long);
		printf("long\t: %d\n", decimalSize);	//+-21��

		decimalSize = sizeof(long long);
		printf("llong\t: %d\n", decimalSize);	//+-900��


		printf("\n");

		int floatSize = 0;

		floatSize = sizeof(float);
		printf("float\t: %d\n", floatSize);		//7�ڸ���

		floatSize = sizeof(double);
		printf("double\t: %d\n", floatSize);	//15�ڸ���

		floatSize = sizeof(long double);
		printf("ldouble\t: %d\n", floatSize);	//15�ڸ���
	}

	//������ �Է�
	int radius = 0;
	printf("������ �Է� : ");
	scanf_s("%d", &radius);

	//���� ���ϴ� �Լ�
	circleArea(radius);

}//main


float circleArea(int radius) {


	printf("���� : %d x PI (%.3f)",radius*radius, PI * radius * radius);
	
	return PI* radius* radius;
}