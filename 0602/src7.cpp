#include <iostream>
#include <stdlib.h>
#include <conio.h>
#include <Windows.h>

const float CRITICAL_PERCENT = 60.0f;
const float CRITICAL_POWER = 150;
const float SLEEP_TIME = 500;

//입력값 중 쓰레기값 삭제
void bufferEmpty() {

	char inputRemove = 0;

	//엔터(\n)가 나오기 전까지 한글자씩 비워준다.
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

		printf("입력(1.공격 2.도망) : ");
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
				printf("!!!!! 크리티컬 !!!! (%.2f) <= (%.2f) : %.2f   !!!!\n", 
					CRITICAL_PERCENT, currCriPercent, damage * CRITICAL_POWER * 0.01f);
				printf("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
				printf("%.0f- %.2f = %.0f\n\n", enemyHP, damage * CRITICAL_POWER * 0.01f, enemyHP - damage * CRITICAL_POWER * 0.01f);
				enemyHP -= damage * CRITICAL_POWER * 0.01f;

			}
			else {
				enemyHP -= damage * 1.0f;
				printf("\n      일반공격      (%.2f) >  (%.2f) : %.2f   \n\n", 
					CRITICAL_PERCENT, currCriPercent, damage * 1.0f);
				printf("%.0f- %.2f = %.0f\n\n", enemyHP, damage * 1.0f, enemyHP- damage * 1.0f);
				enemyHP -= damage * 1.0f;
			}

			if (enemyHP <= 0) {
				printf("몬스터 사망\n");
				return;
			}

			break;

		case '2':
			printf("도망\n\n");
			return;
		default:
			printf("오류\n\n");
		}
		//Sleep(SLEEP_TIME);
	}


}