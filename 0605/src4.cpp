#include <iostream>
#include <conio.h>


void Func();

void main() {

	Func();


}	//[main] end


void Func() {

	char board[5] = { 0, };
	int position = 0;
	int action = 0;
	bool repaint = true;

	//�ʱ�ȭ
	for (int i = 0; i < 5; i++) 
	{
		board[i] = '*';
		if (i == 0) 
		{
			board[i] = '0';
		}
	}

	//�˰���
	while (position < 5) {

		//�ٽ� �׸���
		if (repaint) {
			for (int i = 0; i < 5; i++)
			{
				printf("%c ", board[i]);
			}
			printf("\n");
			repaint = false;
		}
		action = _getch();

		switch (action) {
			//����
		case 'a':
		case 'A':
			repaint = true;
			board[position] = '*';
			position -= 1;

			//�ݴ������� �̵�
			if (position <= -1)
			{
				position += 5;
			}
			board[position] = '0';
			break;

			//������
		case 'd':
		case 'D':
			repaint = true;
			board[position] = '*';
			position += 1;

			//�ݴ������� �̵�
			if (position >= 5)
			{
				position -= 5;
			}
			board[position] = '0';
			break;

		default:
			repaint = false;
		}

	}
}