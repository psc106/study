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

	//초기화
	for (int i = 0; i < 5; i++) 
	{
		board[i] = '*';
		if (i == 0) 
		{
			board[i] = '0';
		}
	}

	//알고리즘
	while (position < 5) {

		//다시 그리기
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
			//왼쪽
		case 'a':
		case 'A':
			repaint = true;
			board[position] = '*';
			position -= 1;

			//반대편으로 이동
			if (position <= -1)
			{
				position += 5;
			}
			board[position] = '0';
			break;

			//오른쪽
		case 'd':
		case 'D':
			repaint = true;
			board[position] = '*';
			position += 1;

			//반대편으로 이동
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