#include <iostream> 
#include <conio.h> 
#include <windows.h>

const int DIRECTION_Y[] = { 1, -1, 0, 0, 0 };
const int DIRECTION_X[] = { 0, 0, 1, -1, 0 };
const int MAP_SIZE_MIN = 3;
const int MAP_SIZE_MAX = 6;
const int SWAP_COUNT = 300;

void Func();

void SaveDirection(int*, int);
void Swap(int*, int*);

void SavePosition(int*, int*, int*, int*, int);
void MoveEdge(int*, int*, int);

bool CheckSuccess(int(*)[MAP_SIZE_MAX], int);

void main() {

	srand(time(NULL));
	Func();
}

void Func() {

	//변수 선언+초기화
	int map[MAP_SIZE_MAX][MAP_SIZE_MAX] = { 0, };
	int size = 0;
	bool isSuccess = false;

	int direction = 4;
	int action = -1;
	int myX = -1, myY = - 1;
	int blockX = - 1, blockY = - 1;

	//사이즈 입력(3~6)
	while (size < MAP_SIZE_MIN || size >MAP_SIZE_MAX) {
		printf("사이즈 입력(3~6): ");
		scanf_s("%d", &size);
	}

	//맵 번호 초기화
	//ex) 사이즈 3일 경우
	//		1 2 3
	//		4 5 6
	//		7 8 9
	for (int vertical = 0; vertical < size; vertical++) {
		for (int horizen = 0; horizen < size; horizen++) {
			map[vertical][horizen] = (horizen + 1) + size * vertical;
		}
	}
	//마지막 자리에 빈칸 생성(내 위치)
	map[size - 1][size - 1] = 0;
	
	//내 위치 초기화
	myX = size - 1;
	myY = size - 1;
	blockX = size - 1;
	blockY = size - 1;

	// 섞기
	for (int i = 0; i < SWAP_COUNT; i++) {
		SavePosition(&myX, &myY, &blockX, &blockY, rand() % 4);
		MoveEdge(&myX, &myY, size);
		Swap(&map[myY][myX], &map[blockY][blockX]);
	}

	//게임 시작
	while (true) {

		//그리기
		system("cls");
		printf("조작 : W,A,S,D\n");
		printf("(%d, %d)\n", myX, myY);
		for (int vertical = 0; vertical < size; vertical++) {
			for (int horizen = 0; horizen < size; horizen++) {
				printf("%2d ", map[vertical][horizen]);
			}
			printf("\n");
		}

		//성공 여부 확인
		if (myX == size - 1 && myY == size - 1) {
			
			if (CheckSuccess(map, size)) {
				printf("퍼즐 성공\n");
				return;
			}
		}

		//키 입력
		action = _getch();

		//방향->이동좌표->외곽처리->데이터 스왑
		SaveDirection(&direction, action);
		SavePosition(&myX, &myY, &blockX, &blockY, direction);
		MoveEdge(&myX, &myY, size);
		Swap(&map[myY][myX], &map[blockY][blockX]);

	}
}

//방향에 따라서 이동할 x,y 좌표 저장
void SavePosition(int* previousX, int* previousY, int* blockX, int* blockY, int direction) {
	*blockY = *previousY;
	*blockX = *previousX;
	*previousY -= DIRECTION_Y[direction];
	*previousX -= DIRECTION_X[direction];
}

//외곽 처리
//외곽을 넘으면 이동하지않음
void MoveEdge(int* x, int* y, int size) {
	if (*y >= size) {
		*y = size - 1;
	}
	else if (*y <= -1) {
		*y = 0;
	}

	if (*x >= size) {
		*x = size - 1;
	}
	else if (*x <= -1) {
		*x = 0;
	}
}

//방향 저장(정수)
//인덱스처럼 쓰기 위해서 0~4까지 설정
void SaveDirection(int* direction, int action) {
	switch (action) {
	case 'w':
	case 'W':
		*direction = 0;
		break;
	case 's':
	case 'S':
		*direction = 1;
		break;
	case 'a':
	case 'A':
		*direction = 2;
		break;
	case 'd':
	case 'D':
		*direction = 3;
		break;
	default:
		*direction = 4;
		break;
	}
}

//내 위치가 마지막 칸일때 들어옴
//처음부터 끝까지 모두 검사해서 끝까지 돌면 true 반환
//그외의 경우 false 반환
bool CheckSuccess(int(*map)[MAP_SIZE_MAX], int size) {

	for (int i = 0; i < size; i++) {
		for (int j = 0; j < size; j++) {

			if (i == size - 1 && j == size - 1) {
				return true;
			}

			if (map[i][j] != (j + 1) + size * i) {
				return false;
			}
		}

	}
}


//데이터 스왑
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}