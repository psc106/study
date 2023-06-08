#include<iostream>
#include<conio.h>
#include<windows.h>

const int MAP_SIZE_X = 6;
const int MAP_SIZE_Y = 6;

void PrintMap(char(*)[MAP_SIZE_Y]);
void InputKey(int, int*);

void Move(char(*)[MAP_SIZE_Y], int*, int*, int);
void Block(char(*)[MAP_SIZE_Y], int*, int*, int);
void MoveEdge(int*, int*);

void Func();

void main() {
	Func();
}


//�� ���
//map[y��][x��]
void PrintMap(char(*map)[MAP_SIZE_Y]) {
	system("cls");

	for (int vertical = MAP_SIZE_Y - 1; vertical >= 0; vertical--) {
		for (int horizen = 0; horizen < MAP_SIZE_X; horizen++) {
			printf("%c ", map[vertical][horizen]);

		}
		printf("\n");
	}
}

//Ű �Է�
void InputKey(int action, int* direction) {
	switch (action) {
	case 'w':
	case 'W':
		*direction = 1;
		break;
	case 'a':
	case 'A':
		*direction = 2;
		break;
	case 's':
	case 'S':
		*direction = 3;
		break;
	case 'd':
	case 'D':
		*direction = 4;
		break;
	default:
		*direction = 0;
		break;
	}
}

//direction ������� �̵� ó��
void Move(char(*map)[MAP_SIZE_Y], int* X, int* Y, int direction) {


	// ��ġ ��ó��
	switch (direction) {
	case 1:
		*Y += 1;
		break;
	case 2:
		*X -= 1;
		break;
	case 3:
		*Y -= 1;
		break;
	case 4:
		*X += 1;
		break;
	}

}

//������ �̵��� 
//�ȿ����̰� ó��
void Block(char(*map)[MAP_SIZE_Y], int* X, int* Y, int direction) {

	//���� ��
	if (map[*Y][*X] == '&') {

		// ��ġ ��ó��
		switch (direction) {
		case 1:
			*Y -= 1;
			break;
		case 2:
			*X += 1;
			break;
		case 3:
			*Y += 1;
			break;
		case 4:
			*X -= 1;
			break;
		}
	}
}

//�ܰ� �Ѿ� �� �� 
//�ݴ������� �̵� 
void MoveEdge(int* X, int* Y) {

	if (*Y >= MAP_SIZE_Y) {
		*Y = *Y % MAP_SIZE_Y;
	}
	else if (*Y < 0) {
		*Y = *Y + MAP_SIZE_Y;
	}
	if (*X >= MAP_SIZE_X) {
		*X = *X % MAP_SIZE_X;
	}
	else if (*X < 0) {
		*X = *X + MAP_SIZE_Y;
	}
}


//���������� ���ư��� �Լ�
void Func() {

	//���� ���� + �ʱ�ȭ
	char map[MAP_SIZE_Y][MAP_SIZE_X] = { 0, };
	int X = 2, Y = 2;
	int direction = 0;

	//�� �ʱ�ȭ
	for (int vertical = 0; vertical < MAP_SIZE_Y; vertical++) {
		for (int horizen = 0; horizen < MAP_SIZE_X; horizen++) {
			
			//�� �ܰ� ��ֹ�
			if (vertical == 0 || vertical == MAP_SIZE_Y - 1) {
				map[vertical][horizen] = '&';
			}
			else if (horizen == 0 || horizen == MAP_SIZE_X - 1) {
				map[vertical][horizen] = '&';
			}
			
			//�⺻ �ʵ�
			else {
				map[vertical][horizen] = '*';
			}
		}
	}

	//�÷��̾�
	map[Y][X] = '0';

	//���� ����
	map[0][X] = '*';
	map[Y][0] = '*';
	map[Y][MAP_SIZE_X - 1] = '*';


	//���� �ݺ�
	//��->Ű�Է�->�̵�->edgeó��->��->edgeó��
	while (true) {

		PrintMap(map);
		int action = _getch();

		map[Y][X] = '*';

		InputKey(action, &direction);

		Move(map, &X, &Y, direction);
		MoveEdge(&X, &Y);

		Block(map, &X, &Y, direction);
		MoveEdge(&X, &Y);

		map[Y][X] = '0';

	}

}