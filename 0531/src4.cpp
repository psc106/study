#include <iostream>

void whatPos(int posNum) {
	posNum == 1 ? printf("����\n") : posNum == 2 ? printf("����\n") : posNum == 3 ? printf("��\n") : printf("��Ģ\n");
}

void fight(int playerPos, int comPos) {
	playerPos<=0||playerPos>=4? printf("��Ģ��\n") 
		:playerPos == comPos 
			? printf("����\n") 
			: (playerPos == 1 && comPos == 2) || 
			  (playerPos == 2 && comPos == 3) || 
			  (playerPos == 3 && comPos == 1) 
				? printf("����\n") 
				: printf("�̰��\n");
}

int main() {

	int comPos = 2;

	int myPos = 0;

	int num = 0;

	num = comPos;
	num = num + 1;

	//1, 2, 3
	
	printf("�Է� : ");
	scanf_s("%d", &myPos); //1,2,3

	printf("��� : ");
	whatPos(myPos); //1,2,3

	printf("��ǻ�� : ");
	whatPos(comPos); //2

	fight(myPos, comPos);

	return 0;
}






