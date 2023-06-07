#include <iostream>
#include <conio.h>

const int ARRAY_BOARD_LENGTH = 5;
const int MOVE_COUNT = 1;

//동작 함수
void MoveFunc();
//case 1
//반대편으로 이동
int ChangePosition_Move(int getPosition, int moveCount, int getLength);
//case 2
//벽에 닿으면 이동 안됨
int ChangePosition_Hold(int getPosition, int moveCount, int getLength);


void main() {

	MoveFunc();

}	//[main] end


void MoveFunc() {

	char board[ARRAY_BOARD_LENGTH] = { 0, };
	int position = 0;
	int action = 0;
	bool repaint = true;
	bool moveMode = true;

	//초기화
	for (int i = 0; i < ARRAY_BOARD_LENGTH; i++)
	{
		board[i] = '*';
		if (i == 0)
		{
			board[i] = '0';
		}
	}

	//알고리즘
	while (true)
	{

		//그리기
		if (repaint) {
			for (int i = 0; i < ARRAY_BOARD_LENGTH; i++)
			{
				printf("%c ", board[i]);
			}
			printf("\n");
			repaint = false;
		}

		//키 입력
		action = _getch();

		switch (action) {
			//왼쪽
		case 'a':
		case 'A':
			repaint = true;
			board[position] = '*';

			if (moveMode)
			{
				position = ChangePosition_Move(position, -MOVE_COUNT, ARRAY_BOARD_LENGTH);
			}
			else if (!moveMode)
			{
				position = ChangePosition_Hold(position, -MOVE_COUNT, ARRAY_BOARD_LENGTH);
			}

			board[position] = '0';
			break;

			//오른쪽
		case 'd':
		case 'D':
			repaint = true;
			board[position] = '*';

			if (moveMode)
			{
				position = ChangePosition_Move(position, MOVE_COUNT, ARRAY_BOARD_LENGTH);
			}
			else if (!moveMode)
			{
				position = ChangePosition_Hold(position, MOVE_COUNT, ARRAY_BOARD_LENGTH);
			}

			board[position] = '0';
			break; 

			//외곽 넘으면 끝부분에 출력
		case 'h':
		case 'H':
			if (moveMode)
			{
				moveMode = false;
				printf("모드 변경[HOLD]\n");
			}
			break;

			//외곽 넘으면 반대편에 출력
		case 'm':
		case 'M':
			if (!moveMode)
			{
				moveMode = true;
				printf("모드 변경[MOVE]\n");
			}
			break;

			//종료
		case 'q':
		case 'Q':
			printf("종료\n");
			return;

		default:
			//입력 키(a,d) 이외일 경우 현상 유지
			repaint = false;
		}

	}//[MoveFunc-while] 종료


}	//[MoveFunc] 종료


//case 1
//반대편으로 이동
int ChangePosition_Move(int getPosition, int moveCount, int getLength) {
	getPosition += moveCount;

	if (getPosition <= -1) {
		while (getPosition <= -1) {
			getPosition = (getPosition + getLength);
		}
	}
	else if (getLength >= getLength)
	{
		getPosition = (getPosition) % getLength;

	}

	return getPosition;
}	//[ChangePosition] 종료

//case 2
//벽에 닿으면 이동 안됨
int ChangePosition_Hold(int getPosition, int moveCount, int getLength) {
	getPosition += moveCount;

	if (getPosition <= -1)
	{
		getPosition = 0;
	}
	else if (getPosition >= getLength)
	{
		getPosition = getLength - 1;
	}

	return getPosition;
}	//[ChangePosition] 종료
