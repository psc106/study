#include <iostream>
#include <stdio.h>
using namespace std;

int sum(int num1, int num2) {
	//반환형은 int(정수)고 이름은 sum인 함수이다.
	//정수형 num1과 정수형num2를 매개변수로 받았다.
	return num1 + num2;
}
int sub(int num1, int num2) {
	return num1 - num2;
}
int mult(int num1, int num2) {
	return num1 * num2;
}
float divide(int num1, int num2) {
	return num2!=0? (float)num1/(float)num2:0.0f;
}
int mod(int num1, int num2) {
	return num1 % num2;
}

int healMaxHp(int sumHP, int maxHP) {
	return sumHP > maxHP ? maxHP : sumHP % maxHP;
}

void main() {
	int num1, num2;
	//변수다. num1, num2이름을 갖고있다.
	//초기화는 안했다.

	num1 = 10;
	//num1에 10을 넣었다.
	num2 = 6;
	//num2에 6을 넣었다.
	printf("%d + %d = %d\n", num1, num2, sum(num1, num2));
	//printf 함수를 썼다. %d를 3개 썼다. 덧셈을 사용했다.
	//정수형으로 num1, num2을 썼고 sum함수를 썼다.
	//sum함수에는 num1과 num2를 받아서 썼다.


	printf("%d - %d = %d\n", num1, num2, sub(num1, num2));
	printf("%d * %d = %d\n", num1, num2, mult(num1, num2));
	printf("%d / %d = %f\n", num1, num2, divide(num1, num2));
	printf("%d %% %d = %d\n", num1, num2, mod(num1, num2));


	int currHP = 120;
	int maxHP = 120;
	int heal = 12;
	int sumHP = currHP + heal;
	int subHP = maxHP - sumHP;

	/*int overHP = 0; 
	overHP = ((currHP + heal) % maxHP);
	int overHP2 = 0; 
	overHP2 = ((currHP + heal) / maxHP) % (((currHP + heal) / maxHP)) * maxHP;*/

	/*printf("1 %d \n", overHP);
	printf("2 %d \n", overHP2);*/
	//int overHP = (((sumHP) % maxHP));
	//printf("%d\n", overHP);
	//int overHP2 = ((((sumHP) / maxHP) % ((((sumHP) / maxHP) + 1))) * maxHP);
	//printf("%d\n", overHP2);

	printf("\n현재체력 : %d\n", currHP);
	printf("최대 : %d\n", maxHP);
	printf("힐 : %d\n", heal);
	printf("체력 : %d\n\n", healMaxHp(sumHP, maxHP));


	bool trueA = true, trueB = true, falseA = false, falseB = false;
	//printf("%d", result);
	//0 거짓, !0 참

	
	int result1, result2, result3, result4;
	result1 = result2 = result3 = result4 = 0;

	result1 = trueA && trueB;
	result2 = trueA && falseB;
	result3 = falseA && trueB;
	result4 = falseA && falseB;
	printf("[AND]\n%d\t%d\n%d\t%d\n\n",result1,result2,result3,result4);


	result1 = trueA || trueB;
	result2 = trueA || falseB;
	result3 = falseA || trueB;
	result4 = falseA || falseB;
	printf("[OR]\n%d\t%d\n%d\t%d\n\n", result1, result2, result3, result4);


	return;
}