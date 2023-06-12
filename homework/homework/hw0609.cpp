#include <iostream>
#include <windows.h>
#include <conio.h>

//��� ����
const unsigned short POSITION_STATUS_1 = 0;
const unsigned short POSITION_GRAPHIC = 2;
const unsigned short POSITION_STATUS_2 = 17;
const unsigned short POSITION_INPUT = 23;

const int DIFFICULT_COUNT_MIN = 2;
const int DIFFICULT_COUNT_MAX = 4;
const int DECK_COUNT_MAX = 52;
const int MIX_COUNT = 300;
const float MULTIPLE = 0.9f;

const int DUMMY_DECK_DEFAULT = -1;
const int CHIP_DEFAULT = 100;
const int TARGET_DEFAULT = 10000;

const char* CARD_PATTERN[] = { "��","��","��","��" };
const char* CARD_NUMBER[] = { "A","2","3","4","5","6","7","8","9","10","J","Q","K" };


//�Լ� ����
void init();
void StartGame();

void Shuffle(int count, int* arr, int length);

int SelectCard(int* deck, int deckLength, int* dummy, int dummyLength, int index);
int SelectCard(int* deck, int deckLength, int* dummy, int dummyLength);

bool isBatting(int diff, int index);
int inputNumber(char* str, int length);

bool Fight(int* deck, int length, int card);

void PrintCard_Com(int* arr, int index);
int PrintString_Card_Com(const char* str, int index, int offset);
int PrintString_Card_Com(int cardNumber, bool isTopPosition, int index, int offset);

void PrintCard_Player(int card);
int PrintString_Card_Player(const char* str, int offset);
int PrintString_Card_Player(int cardNumber, bool isTopPosition, int offset);

int DrawTop(int* arr, int length);
int DrawTop(int* arr, int length, int index);
void Discard(int number, int* arr, int length);
void Discard(int number, int* arr, int length, int index);

void Swap(int* num1, int* num2);
void ChangeCursor(short x, short y);
int SortSelection(int* arr, int size);

void ClearLine();
void ClearLine(int y);
void ClearLine(short xCount);
void ClearLine(int y, int yCount);

void ClearLineBack(int y);
void ClearLineBack(int y, int count);
void ClearLineBack(int y, int backY, int count);


//����
void main() {
	init();
	StartGame();
}//[main] end

 
//���� ������ �ʱ�ȭ
void init() {
	srand((unsigned int)time(NULL));
	system("mode con cols=40 lines=50");
}//[init] end

//���� ����
void StartGame() {
	//�� : ī�� �̴°�
	//���� ī�庸�� ���� ī�尡 ������ ���� ��� �̴´�.
	int* deck = new int[DECK_COUNT_MAX];
	//���� : ī�� �����°�
	int* dummy = new int[DECK_COUNT_MAX];


	int chip = CHIP_DEFAULT;		//�ǵ�
	int round = 0;					//����
	int target = TARGET_DEFAULT;	//��ǥ����
	int deckDiff = 1;				//���̵�

	int chipSum = 0;			//�ش� ���忡 ���� Ĩ�� ��
	int incomeSum = 0;			//�ش� ���� �¸��� �޴� Ĩ�� ��
	
	int action = -1;			//�Է°� ����
	bool batting = false;		//�����̺�Ʈ FLAG
	bool deckEmpty = false;		//�� ���� �̺�Ʈ FLAG

	int* comSelectCard = NULL;	//��ǻ�Ͱ� �̴� ī�� ����
	int playerSelectCard = 0;	//���� �̴� ī�� ����

	//���̵� �Է�
	char* str = NULL;
	while (deckDiff < DIFFICULT_COUNT_MIN || deckDiff > DIFFICULT_COUNT_MAX) {
		str = new char[4];

		ClearLineBack(0);
		printf("���̵� �Է�(2~4) : ");

		deckDiff = inputNumber(str, 4);
	}//���̵� �Է� ����


	//��ǻ�ʹ� ���̵� ��ŭ ī�带 �̴´�. 
	comSelectCard = new int[deckDiff];

	//ī�� �ʱ�ȭ+����
	for (int i = 0; i < DECK_COUNT_MAX; i++) {
		deck[i] = i;
		dummy[i] = -1;
	}
	Shuffle(MIX_COUNT, deck, DECK_COUNT_MAX);

	//���� ����
	while (true) {
		//���� ������� ���
		//�� �ٽ� ���� �̺�Ʈ
		if (deckEmpty) {
			round = 0;
			for (int i = 0; i < DECK_COUNT_MAX; i++) {
				if (deck[i] != -1) {
					dummy[i] = deck[i];
					deck[i] = -1;
				}
			}

			_getch();
			//���̿� ���� �ٲ�
			int* tmp = deck;
			deck = dummy;
			dummy = tmp;
			//����

			Shuffle(MIX_COUNT, deck, DECK_COUNT_MAX);
		}//[StartGame-while-if] end �� ����

		//���� �޼��� ���
		ClearLineBack(0);
		printf("�ܰ� %5d\t", chip);
		printf("��ǥ %5d\t", target);
		printf("%2d����\t", round+1);

		//�й�ó��
		if (chip <= 0) {
			ClearLine(0, 1);
			printf("\n�й� [�ܰ� 0]\n\n\n\n\n\n\n\n\n\n\n");

			return;
		}
		//�¸�ó��
		if (chip >= target) {
			ClearLine(0, 1);
			printf("\n�¸� [�ܰ� %d]\n\n\n\n\n\n\n\n\n\n\n", chip);

			return;
		}

		//��ǻ�� ī��̱� -> ����
		for (int i = 0; i < deckDiff; i++) {
			batting = false;

			//��ǻ�� ī�� ����̾Ƽ� ������ �׷��� ���
			comSelectCard[i] = SelectCard(deck, DECK_COUNT_MAX, dummy, DECK_COUNT_MAX, i + round * (1 + deckDiff));
			PrintCard_Com(comSelectCard, i);


			//������ �ܰ������ �ڵ� ��ŵ
			if (chip == 0) {
				ClearLineBack(POSITION_INPUT,1);
				printf("���� �Ұ���(�ܰ� 0)");
				continue;
			}

			//�������� ����
			batting = isBatting(deckDiff, i);

			//������ ��� 1�̻� �ۼ�
			while (batting) {

				//���� �ȳ� �ؽ�Ʈ
				ClearLineBack(POSITION_INPUT, 1);
				int currBatting = 0;
				printf("���ñݾ�[1~%d] : ", chip);

				//���� �Է�
				char* str = NULL;
				while(currBatting > chip || currBatting < 1) {
					str = new char[10];
					currBatting = inputNumber(str, 10);

					if (currBatting > chip || currBatting < 1) {
						ClearLineBack(POSITION_INPUT+1);
						printf("�Է� ����");
						ChangeCursor(0,POSITION_INPUT);
						ClearLineBack(POSITION_INPUT);
						printf("���ñݾ�[1~%d] : ", chip);
						continue;
					}
				}//���� �Է� ����
				ClearLine(POSITION_INPUT, 2);

				//���ý� ó��				
				chip -= currBatting;
				chipSum += currBatting;
				incomeSum += currBatting * (deckDiff - (i * MULTIPLE));
					
				//����â�� ���
				ChangeCursor(0, POSITION_STATUS_2 + i);
				printf("����[%d] %d(%d) ", i + 1, (int)(currBatting * (deckDiff - (i * MULTIPLE))), currBatting);
				ChangeCursor(0, POSITION_STATUS_2 + deckDiff);
				printf("������ : %d(%d) ", incomeSum, chipSum);
				
				//�Է�â ����
				ClearLine(POSITION_INPUT, 1);
				
				//���� �̺�Ʈ ������
				batting = false;				
			}//[StartGame-while-for-while] end ���� �̺�Ʈ
		}//[StartGame-while-for] end ��ǻ�� ī�� �̱�

		//��ǻ�� ���� ī�� ����
		SortSelection(comSelectCard, deckDiff);

		//��ī�� �̱�
		playerSelectCard = SelectCard(deck, DECK_COUNT_MAX, dummy, DECK_COUNT_MAX);
		PrintCard_Player(playerSelectCard);

		ClearLine(POSITION_INPUT);

		//������ �غ�
		_getch();

		//���� ī�尡 ��ǻ�Ͱ� ���� ī�� ���̿� ������ Ȯ���Ѵ�.
		//���� ī�尡 ��ǻ�Ͱ� ���� ī��� �ٸ��� Ȯ���Ѵ�.
		if (Fight(comSelectCard, deckDiff, playerSelectCard)) {
			//�޼��� ���
			ChangeCursor(20, POSITION_STATUS_2);
			printf("���߱� ����");
			ChangeCursor(20, POSITION_STATUS_2+1);
			printf("%d + %d = %d", chip, incomeSum, chip+incomeSum);

			//������ ����+�ʱ�ȭ
			chip += incomeSum;
			incomeSum = 0;
			chipSum = 0;
		}
		else {
			//�޼��� ���
			ChangeCursor(20, POSITION_STATUS_2);
			printf("���߱� ����");
			ChangeCursor(20, POSITION_STATUS_2+1);
			printf("%d - %d = %d", chip+chipSum, chipSum, chip);
			
			//���н� �ʱ�ȭ�� ����
			incomeSum = 0;
			chipSum = 0;
		}

		_getch();

		//��ü ���� Ŭ����
		ClearLine(POSITION_GRAPHIC, POSITION_STATUS_2 - POSITION_GRAPHIC);
		ClearLine(POSITION_STATUS_2, POSITION_INPUT - POSITION_STATUS_2);
		ClearLine(POSITION_INPUT, 30 - POSITION_INPUT);

		//�̾ƾ� �ϴ� ī�尡 ���� ��� �ٽ� �����ش�.
		for (int i = DECK_COUNT_MAX - 1; i >= DECK_COUNT_MAX - deckDiff; i--) {
			if (deck[i] == -1) {
				deckEmpty = true;
				ChangeCursor(0, POSITION_STATUS_2);
				printf("���� ���� ī�尡 �ʹ� �����ϴ�.\nī�带 �ٽ� �����ϴ�.");
				_getch();
				break;
			}
		}

		//���� ����
		round++;
	}//[StartGame-while] end ���� ����

}//[StartGame] end


//���� ��� ���
//Ű �Է�
bool isBatting(int diff, int index) {
	while (true) {

		ClearLineBack(POSITION_INPUT);

		printf("�����Ͻðڽ��ϱ�?[x%.1f](N/Y) : ", diff - (index * MULTIPLE));
		int action = _getch();

		switch (action) {
		case 'n':
		case 'N':
			return false;
			break;
		case 'y':
		case 'Y':
			return true;
			break;
		default:
			break;
		}
	}
}//[isBatting] end

//�Է��Լ�
//getch�� ���� �ϳ��� �ν��ѵ� ���ڸ� �Է��Ѵ�.
//Ư�� ������ ������ �ڵ� �Է�.
int inputNumber(char* str, int length) {
	//�ʱ�
	int number = 2;

	//������ 1���ڴ� '\0'�� �Է��ϱ� ���� �����ʴ´�.
	for (int i = 0; i < length; i++) {
		//�� ���ھ� �Է�
		int character = _getch();


		//������ �ε��� or ����(getch������ '\r') �Է½� 
		//���ڿ� -> ���� �Լ� ��� : atoi()
		if (i==length-1 || character == '\r') {
			str[i + 1] = '\0';
			number = atoi(str);
			break;
		}

		//���� �Է½� print�ϰ� ����
		if (character >= '0' && character <= '9') {
			str[i] = character;
			printf("%c", str[i]);
			continue;
		}

		//�齺���̽� �Է½� �����
		//���� ��ġ���� �ε��� �ٽ� ����
		if (character == 8) {
			if (i == 0) {
				i = i - 1;
				continue;
			}
			str[i-1] = 0;
			printf("\b \b");
			i = i - 2;
			continue;
		}

		//Ư������ �Է½� getch�� �ѹ� �� �Է� �޴´�.
		//�ε��� ��ġ ����
		if (str[i] == 224) {
			i = i - 1;
			_getch();
		}
		//��Ÿ �ٸ� ���� �Է½� �ε��� ��ġ ����
		else {
			i = i - 1;
		}
		
	}

	return number;
}//[inputNumber] end


//�÷��̾� ī�� �׸��� �Լ�(�ܺ�)
//�̰��� ȣ���Ѵ�.
void PrintCard_Player(int card) {
	int offset = 2;

	PrintString_Card_Player("��������������������������", offset++);
	PrintString_Card_Player(card, true, offset++);
	PrintString_Card_Player("��           ��", offset++);
	PrintString_Card_Player("��           ��", offset++);
	PrintString_Card_Player("��           ��", offset++);
	PrintString_Card_Player("��           ��", offset++);
	PrintString_Card_Player("��           ��", offset++);
	PrintString_Card_Player(card, false, offset++);
	PrintString_Card_Player("��������������������������", offset++);

}//[PrintCard_Player] end

//�÷��̾� ī�� �׸��� �Լ�(����)
//#1 ��� ���ڿ�
int PrintString_Card_Player(const char* str, int offset) {
	ChangeCursor(19, offset);
	printf("%s", str);
	return offset + 1;
}
//#2 ī�� ����
int PrintString_Card_Player(int cardNumber, bool isTopPosition, int offset) {

	ChangeCursor(19, offset);
	printf("��");
	if (!isTopPosition) {
		printf("       ");
	}
	printf("%s%2s", CARD_PATTERN[cardNumber / 13], CARD_NUMBER[cardNumber % 13]);
	if (isTopPosition) {
		printf("       ");
	}
	printf("��");

	return offset + 1;

}//[PrintString_Card_Player] end

//��ǻ�� ī�� �׸��� �Լ� (�ܺ�)
//�̰��� ȣ���Ѵ�.
void PrintCard_Com(int* arr, int index) {
	int currCard = arr[index];
	int offset = 0;

	PrintString_Card_Com("��������������������������", index, offset++);
	PrintString_Card_Com(currCard, true, index, offset++);
	PrintString_Card_Com("��           ��", index, offset++);
	PrintString_Card_Com("��           ��", index, offset++);
	PrintString_Card_Com("��           ��", index, offset++);
	PrintString_Card_Com("��           ��", index, offset++);
	PrintString_Card_Com("��           ��", index, offset++);
	PrintString_Card_Com(currCard, false, index, offset++);
	PrintString_Card_Com("��������������������������", index, offset++);

	//ī�� ��ĥ�� ���� ó��
	if (index > 0) {
		ChangeCursor(12 + (index), POSITION_GRAPHIC + (index * 2));
		printf("��");
	}

}//[PrintCard_Com] end

//��ǻ�� ī�� �׸��� �Լ�(����)
//2���� ���
//��� ���ڿ��� ���� ��� �ٷ� ����Ѵ�.
//��������
int PrintString_Card_Com(const char* str, int index, int offset) {
	ChangeCursor(1, POSITION_GRAPHIC + (index * 2) + offset);
	for (int i = 1; i < (offset / 7) * index; i++) {
		printf(" ");
	}

	ChangeCursor(1 + (index * 1), POSITION_GRAPHIC + (index * 2) + offset);
	printf("%s", str);
	return offset + 1;
}
//������ ���� ��� Ư�� ��ġ�� ����� ��ȣ�� ����Ѵ�.
int PrintString_Card_Com(int cardNumber, bool isTopPosition, int index, int offset) {
	ChangeCursor(1, POSITION_GRAPHIC + (index * 2) + offset);
	for (int i = 1; i < (offset / 7) * index; i++) {
		printf(" ");
	}

	ChangeCursor(1 + (index * 1), POSITION_GRAPHIC + (index * 2) + offset);
	printf("��");
	if (!isTopPosition) {
		printf("       ");
	}
	printf("%s%2s", CARD_PATTERN[cardNumber / 13], CARD_NUMBER[cardNumber % 13]);
	if (isTopPosition) {
		printf("       ");
	}
	printf("��");

	//���� �����
	if (!isTopPosition) {
		ClearLine((short)(19 - 11 + (index * 1)));
	}
	return offset + 1;
}//[PrintString_Card_Com] end


//ī�带 ���� �̰� ���̿� �ִ´�.
//���� return�Ѵ�
//#1
int SelectCard(int* deck, int deckLength, int* dummy, int dummyLength, int index) {
	int getNumber = DrawTop(deck, deckLength, index);
	Discard(getNumber, dummy, dummyLength, index);
	return getNumber;
}
//#2
int SelectCard(int* deck, int deckLength, int* dummy, int dummyLength) {
	int getNumber = DrawTop(deck, deckLength);
	Discard(getNumber, dummy, dummyLength);
	return getNumber;
}//[SelectCard] end

//ī�带 ���� ����
//#1
int DrawTop(int* arr, int length) {

	int getNumber = DUMMY_DECK_DEFAULT;

	for (int i = 0; i < length; i++) {
		getNumber = *(arr + i);
		if (getNumber != DUMMY_DECK_DEFAULT) {
			*(arr + i) = DUMMY_DECK_DEFAULT;
			return getNumber;
		}
	}
	return getNumber;

}
//#2
int DrawTop(int* arr, int length, int index) {

	if (index >= length) {
		return DUMMY_DECK_DEFAULT;
	}
	int number = *(arr + index);
	*(arr + index) = -1;

	return number;

}//[DrawTop] end

//���� ī�带 ���̿� �߰���
//#1
void Discard(int number, int* arr, int length) {
	for (int i = 0; i < length; i++) {
		if (arr[i] == DUMMY_DECK_DEFAULT) {
			*(arr+i) = number;
			return;
		}
	}
}
//#2
void Discard(int number, int* arr, int length, int index) {

	if (index >= length) {
		return;
	}

	arr[index] = number;
}//[Discard] end


//���� ���� ���� ���� ū �� ���� + �� �� ī���� ���ڿ� ������ �й�
//�� ���� ���
bool Fight(int* deck, int length, int card) {


	if (deck[0]%13 >= card%13 || deck[length - 1]%13 <= card%13) {
		return false;
	}
	else {
		for (int i = 1; i < length - 1; i++) {
			if (deck[i]%13 == card%13) {
				return false;
			}
		}
	}
	return true;
}//[Fight] end

void Shuffle(int count, int* arr, int length) {
	for (int i = 0; i < count; i++) {
		Swap(&arr[rand() % length], &arr[rand() % length]);
	}
}//[Shuffle] end


//����
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}//[Swap] end

//Ŀ���̵��Լ�
//coordinate(��ǥ��)
//windows.h
void ChangeCursor(short x, short y) {
	COORD Cur = { x,y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Cur);
}//[ChangeCursor] end

//������ ����
void ClearLine() {
	for (int i = 0; i < 40; i++) {
		printf(" ");
	}
}
void ClearLine(int y) {
	ChangeCursor(0, y);
	for (int i = 0; i < 40; i++) {
		printf(" ");
	}
}
void ClearLine(short xCount) {
	for (int i = 0; i < xCount; i++) {
		printf(" ");
	}
}
void ClearLine(int y, int yCount) {
	for (int i = 0; i < yCount; i++) {
		ChangeCursor(0, y+i);
		for (int j = 0; j < 40; j++) {
			printf(" ");
		}
	}
}//[ClearLine] end


//���� Ŭ���� �� �Է��� �������� �̵�
void ClearLineBack(int y) {
	ChangeCursor(0, y);
	for (int i = 0; i < 40; i++) {
		printf(" ");
	}
	ChangeCursor(0, y);
}
void ClearLineBack(int y, int count) {
	for (int i = 0; i < count; i++) {
		ChangeCursor(0, y + i);
		for (int j = 0; j < 40; j++) {
			printf(" ");
		}
	}
	ChangeCursor(0, y);
}
void ClearLineBack(int y, int backY, int count) {
	for (int i = 0; i < count; i++) {
		ChangeCursor(0, y + i);
		for (int j = 0; j < 40; j++) {
			printf(" ");
		}
	}
	ChangeCursor(0, backY);
}//[ClearLineBack] end

//��������
int SortSelection(int* arr, int size) {
	//for�� Ƚ��
	int count = 0;
	int min = 0;

	//0~size-1���� ��
	for (int i = 0; i < size - 1; i++) {
		min = i;
		// i���� size����
		for (int j = i; j < size; j++) {

			//���� �� ���� �������� ����(��������)
			if (arr[min] % 13 > arr[j] % 13) {
				min = j;
			}
			count++;
		}
		Swap(&arr[min], &arr[i]);
		count++;
	}
	return count;
}//[SortSelection] end
