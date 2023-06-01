#include <iostream>

int result1(int x, int y, int z) {

	return x + y * z;
}
int result2(int x, int y, int z) {

	return (x - y) * (y + z) * (z % x);
}

void input(int* p1, int* p2, int* p3) {

	printf("x, y, z 입력(띄어쓰기로 구분) : ");
	scanf_s("%d", p1);
	scanf_s("%d", p2);
	scanf_s("%d", p3);
}


//x,y,z입력 -> x+y*z출력
//scanf_s, 함수 사용
//사칙연산 순서 적용
void main() {

	//변수 선언
	int X=0, Y=0, Z=0;
	int *pX = NULL, *pY = NULL, *pZ = NULL;

	pX = &X;
	pY = &Y;
	pZ = &Z;


	//입력 x, y, z
	input(pX, pY, pZ);

	//결과1 x + y * z
	printf("\n결과 1 : %d + %d * %d = %d\n", X, Y, Z, result1(X, Y, Z));

	//결과2 (x - y) * (y + z) * (z % x)
	printf("\n결과 2 : ");
	printf("(%d - %d) * ", X, Y);
	printf("(%d + %d) * ", Y, Z);
	printf("(%d %% %d) = ", Z, X);
	printf("%d\n",  result2(X, Y, Z));
	

	return;
}	//main