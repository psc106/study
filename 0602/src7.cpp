#include <iostream>
#include <stdlib.h>
#include <conio.h>
#include <Windows.h>

const float CRITICAL_PERCENT = 60.0f;
const float CRITICAL_POWER = 150;
const float SLEEP_TIME = 500;

//�Է°� �� �����Ⱚ ����
void bufferEmpty() {

	char inputRemove = 0;

	//����(\n)�� ������ ������ �ѱ��ھ� ����ش�.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge

}	//[bufferEmpty edge]


void main() {

	float enemyHP = 3000;
	int damage = 0;
	printf("\n");

	time_t seed = time(NULL);
	char action;

	while (true) {

		printf("�Է�(1.���� 2.����) : ");
		scanf_s("%c", &action);
		system("cls");
		bufferEmpty();
		float currCriPercent = (rand() % 10001) * 0.01;

		switch (action) {

		case '1':

			damage = (rand() % 50) + 50;
			if (CRITICAL_PERCENT <= currCriPercent) {
				enemyHP -= damage * CRITICAL_POWER * 0.01f;
				printf("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
				printf("!!!!! ũ��Ƽ�� !!!! (%.2f) <= (%.2f) : %.2f   !!!!\n", 
					CRITICAL_PERCENT, currCriPercent, damage * CRITICAL_POWER * 0.01f);
				printf("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
				printf("%.0f- %.2f = %.0f\n\n", enemyHP, damage * CRITICAL_POWER * 0.01f, enemyHP - damage * CRITICAL_POWER * 0.01f);
				enemyHP -= damage * CRITICAL_POWER * 0.01f;

			}
			else {
				enemyHP -= damage * 1.0f;
				printf("\n      �Ϲݰ���      (%.2f) >  (%.2f) : %.2f   \n\n", 
					CRITICAL_PERCENT, currCriPercent, damage * 1.0f);
				printf("%.0f- %.2f = %.0f\n\n", enemyHP, damage * 1.0f, enemyHP- damage * 1.0f);
				enemyHP -= damage * 1.0f;
			}

			if (enemyHP <= 0) {
				printf("���� ���\n");
				return;
			}

			break;

		case '2':
			printf("����\n\n");
			return;
		default:
			printf("����\n\n");
		}
		//Sleep(SLEEP_TIME);
	}


}