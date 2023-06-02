#include <iostream>
#include <windows.h>
#include <conio.h>
#include <stdlib.h>

const int UP = 80;
const int DOWN = 72;
const int RIGHT = 77;
const int LEFT = 75;


int moveX(int movePosition) {
	//������ ����Ű
	if (movePosition == RIGHT) {
		return 1;
	
	}
	//���� ����Ű
	else if (movePosition == LEFT) {
		return -1;
	}
	//�׿�
	else {
		return 0;
	}
}
int moveY(int movePosition) {
	//�� ����Ű
	if (movePosition == UP) {
		return 1;

	}
	//�Ʒ� ����Ű
	else if (movePosition == DOWN) {
		return -1;
	}
	//�׿�
	else {
		return 0;
	}
}

void main() {

	int currX=3, currY=3;
	int currMonsterX=5, currMonsterY=5;
	int currField = 0;
	int exitX = 0, exitY = 0;

	while (true) {
		int action = _getch();

		switch (action) {
		case 224 :
			action = _getch();
			currX += moveX(action);
			currY += moveY(action);

			printf("%d %d\n", currX, currY);

			if (currX == -1) {
				currX += 5;
			}
			else if (currX == 5) {
				currX -= 5;
			}
			if (currY == -1) {
				currY += 5;
			}
			else if (currY == 5) {
				currY -= 5;
			}
			if (currX == exitX && currY == exitY) {
				currX = 3; currY = 3;
				currField += 1;
				exitX = (exitX + 2) % 5;
				exitY = (exitY + 2) % 5;
			}
			break;
		case 'q':
			printf("����");
			return;
		}
		system("cls");

		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (j == currX && i == currY) {
					printf("��");
				}
				else if (j == currMonsterX && i== currMonsterY) {
					printf("��");
				}
				else if (j == exitX && i == exitY) {
					printf("Ż");
				}
				else if (currField == 0) {
					printf("��");
				}
				else if (currField == 1) {
					printf("��");
				}
				else if (currField == 2) {
					printf("��");
				}
			} 
			printf("\n");
		}


	}


}