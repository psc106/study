#include<stdio.h>

void printAge(int num) {

	
	printf("%d\n", num);
	return;
}


int hamsu(int num) {

	return num-1;
}



int main() {
	int phoneSecond, phoneThird;

	phoneSecond = 5580;

	phoneThird = 4310;

	printf("�ڼ�ö\n");
	printAge(33);
	printf("010-%d-%d\n", phoneSecond, phoneThird);


	printf("�̸� : �ڼ�ö\n");
	printf("���� : 323\n");
	printf("������ : %d��\n", hamsu(323));
	printf("��ȭ��ȣ : 010-5580-4310\n\n");

	printf("�̸� : ���ؿ�\n");
	printf("���� : 100\n");
	printf("������ : %d��\n", hamsu(100));
	printf("��ȭ��ȣ : 020-3033-3333\n\n");

	return 0;
}
