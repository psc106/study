#include <iostream>
#include <stdio.h>
using namespace std;

int sum(int num1, int num2) {
	//��ȯ���� int(����)�� �̸��� sum�� �Լ��̴�.
	//������ num1�� ������num2�� �Ű������� �޾Ҵ�.
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
	//������. num1, num2�̸��� �����ִ�.
	//�ʱ�ȭ�� ���ߴ�.

	num1 = 10;
	//num1�� 10�� �־���.
	num2 = 6;
	//num2�� 6�� �־���.
	printf("%d + %d = %d\n", num1, num2, sum(num1, num2));
	//printf �Լ��� ���. %d�� 3�� ���. ������ ����ߴ�.
	//���������� num1, num2�� ��� sum�Լ��� ���.
	//sum�Լ����� num1�� num2�� �޾Ƽ� ���.


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

	printf("\n����ü�� : %d\n", currHP);
	printf("�ִ� : %d\n", maxHP);
	printf("�� : %d\n", heal);
	printf("ü�� : %d\n\n", healMaxHp(sumHP, maxHP));


	bool trueA = true, trueB = true, falseA = false, falseB = false;
	//printf("%d", result);
	//0 ����, !0 ��

	
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