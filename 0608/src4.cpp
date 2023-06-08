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


//맵 출력
//map[y축][x축]
void PrintMap(char(*map)[MAP_SIZE_Y]) {
	system("cls");

	for (int vertical = MAP_SIZE_Y - 1; vertical >= 0; vertical--) {
		for (int horizen = 0; horizen < MAP_SIZE_X; horizen++) {
			printf("%c ", map[vertical][horizen]);

		}
		printf("\n");
	}
}

//키 입력
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

//direction 기반으로 이동 처리
void Move(char(*map)[MAP_SIZE_Y], int* X, int* Y, int direction) {


	// 위치 후처리
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

//벽으로 이동시 
//안움직이게 처리
void Block(char(*map)[MAP_SIZE_Y], int* X, int* Y, int direction) {

	//문자 비교
	if (map[*Y][*X] == '&') {

		// 위치 후처리
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

//외곽 넘어 갈 시 
//반대편으로 이동 
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


//실제적으로 돌아가는 함수
void Func() {

	//변수 선언 + 초기화
	char map[MAP_SIZE_Y][MAP_SIZE_X] = { 0, };
	int X = 2, Y = 2;
	int direction = 0;

	//맵 초기화
	for (int vertical = 0; vertical < MAP_SIZE_Y; vertical++) {
		for (int horizen = 0; horizen < MAP_SIZE_X; horizen++) {
			
			//맵 외곽 장애물
			if (vertical == 0 || vertical == MAP_SIZE_Y - 1) {
				map[vertical][horizen] = '&';
			}
			else if (horizen == 0 || horizen == MAP_SIZE_X - 1) {
				map[vertical][horizen] = '&';
			}
			
			//기본 필드
			else {
				map[vertical][horizen] = '*';
			}
		}
	}

	//플레이어
	map[Y][X] = '0';

	//예외 지형
	map[0][X] = '*';
	map[Y][0] = '*';
	map[Y][MAP_SIZE_X - 1] = '*';


	//무한 반복
	//맵->키입력->이동->edge처리->벽->edge처리
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