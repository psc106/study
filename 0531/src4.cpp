#include <iostream>

void whatPos(int posNum) {
	posNum == 1 ? printf("가위\n") : posNum == 2 ? printf("바위\n") : posNum == 3 ? printf("보\n") : printf("반칙\n");
}

void fight(int playerPos, int comPos) {
	playerPos<=0||playerPos>=4? printf("반칙패\n") :playerPos == comPos ? printf("비겼다\n") : (playerPos == 1 && comPos == 2) || (playerPos == 2 && comPos == 3) || (playerPos == 3 && comPos == 1) ? printf("졌다\n") : printf("이겼다\n");
}

int main() {

	int comPos = 2;

	int myPos = 0;
	
	printf("입력 : ");
	scanf_s("%d", &myPos);

	printf("당신 : ");
	whatPos(myPos);

	printf("컴퓨터 : ");
	whatPos(comPos);

	fight(myPos, comPos);

	return 0;
}






