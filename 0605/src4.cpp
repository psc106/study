#include <iostream>
#include <conio.h>

const int ARRAY_BOARD_LENGTH = 5;
const int MOVE_COUNT = 1;

//���� �Լ�
void MoveFunc();
//case 1
//�ݴ������� �̵�
int ChangePosition_Move(int getPosition, int moveCount, int getLength);
//case 2
//���� ������ �̵� �ȵ�
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

	//�ʱ�ȭ
	for (int i = 0; i < ARRAY_BOARD_LENGTH; i++)
	{
		board[i] = '*';
		if (i == 0)
		{
			board[i] = '0';
		}
	}

	//�˰���
	while (true)
	{

		//�׸���
		if (repaint) {
			for (int i = 0; i < ARRAY_BOARD_LENGTH; i++)
			{
				printf("%c ", board[i]);
			}
			printf("\n");
			repaint = false;
		}

		//Ű �Է�
		action = _getch();

		switch (action) {
			//����
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

			//������
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

			//�ܰ� ������ ���κп� ���
		case 'h':
		case 'H':
			if (moveMode)
			{
				moveMode = false;
				printf("��� ����[HOLD]\n");
			}
			break;

			//�ܰ� ������ �ݴ��� ���
		case 'm':
		case 'M':
			if (!moveMode)
			{
				moveMode = true;
				printf("��� ����[MOVE]\n");
			}
			break;

			//����
		case 'q':
		case 'Q':
			printf("����\n");
			return;

		default:
			//�Է� Ű(a,d) �̿��� ��� ���� ����
			repaint = false;
		}

	}//[MoveFunc-while] ����


}	//[MoveFunc] ����


//case 1
//�ݴ������� �̵�
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
}	//[ChangePosition] ����

//case 2
//���� ������ �̵� �ȵ�
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
}	//[ChangePosition] ����
