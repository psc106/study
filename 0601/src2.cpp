#include <iostream>

int result1(int x, int y, int z) {

	return x + y * z;
}
int result2(int x, int y, int z) {

	return (x - y) * (y + z) * (z % x);
}

void input(int* p1, int* p2, int* p3) {

	printf("x, y, z �Է�(����� ����) : ");
	scanf_s("%d", p1);
	scanf_s("%d", p2);
	scanf_s("%d", p3);
}


//x,y,z�Է� -> x+y*z���
//scanf_s, �Լ� ���
//��Ģ���� ���� ����
void main() {

	//���� ����
	int X=0, Y=0, Z=0;
	int *pX = NULL, *pY = NULL, *pZ = NULL;

	pX = &X;
	pY = &Y;
	pZ = &Z;


	//�Է� x, y, z
	input(pX, pY, pZ);

	//���1 x + y * z
	printf("\n��� 1 : %d + %d * %d = %d\n", X, Y, Z, result1(X, Y, Z));

	//���2 (x - y) * (y + z) * (z % x)
	printf("\n��� 2 : ");
	printf("(%d - %d) * ", X, Y);
	printf("(%d + %d) * ", Y, Z);
	printf("(%d %% %d) = ", Z, X);
	printf("%d\n",  result2(X, Y, Z));
	

	return;
}	//main