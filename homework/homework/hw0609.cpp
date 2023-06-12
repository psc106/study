#include <iostream>
#include <windows.h>
#include <conio.h>

//상수 선언
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

const char* CARD_PATTERN[] = { "♠","♥","◆","♣" };
const char* CARD_NUMBER[] = { "A","2","3","4","5","6","7","8","9","10","J","Q","K" };


//함수 선언
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


//메인
void main() {
	init();
	StartGame();
}//[main] end

 
//게임 시작전 초기화
void init() {
	srand((unsigned int)time(NULL));
	system("mode con cols=40 lines=50");
}//[init] end

//게임 시작
void StartGame() {
	//덱 : 카드 뽑는곳
	//뽑을 카드보다 남은 카드가 적으면 새로 섞어서 뽑는다.
	int* deck = new int[DECK_COUNT_MAX];
	//더미 : 카드 버리는곳
	int* dummy = new int[DECK_COUNT_MAX];


	int chip = CHIP_DEFAULT;		//판돈
	int round = 0;					//라운드
	int target = TARGET_DEFAULT;	//목표점수
	int deckDiff = 1;				//난이도

	int chipSum = 0;			//해당 라운드에 걸은 칩의 수
	int incomeSum = 0;			//해당 라운드 승리시 받는 칩의 수
	
	int action = -1;			//입력값 저장
	bool batting = false;		//배팅이벤트 FLAG
	bool deckEmpty = false;		//덱 섞기 이벤트 FLAG

	int* comSelectCard = NULL;	//컴퓨터가 뽑는 카드 저장
	int playerSelectCard = 0;	//내가 뽑는 카드 저장

	//난이도 입력
	char* str = NULL;
	while (deckDiff < DIFFICULT_COUNT_MIN || deckDiff > DIFFICULT_COUNT_MAX) {
		str = new char[4];

		ClearLineBack(0);
		printf("난이도 입력(2~4) : ");

		deckDiff = inputNumber(str, 4);
	}//난이도 입력 종료


	//컴퓨터는 난이도 만큼 카드를 뽑는다. 
	comSelectCard = new int[deckDiff];

	//카드 초기화+셔플
	for (int i = 0; i < DECK_COUNT_MAX; i++) {
		deck[i] = i;
		dummy[i] = -1;
	}
	Shuffle(MIX_COUNT, deck, DECK_COUNT_MAX);

	//게임 시작
	while (true) {
		//덱이 비어있을 경우
		//덱 다시 섞기 이벤트
		if (deckEmpty) {
			round = 0;
			for (int i = 0; i < DECK_COUNT_MAX; i++) {
				if (deck[i] != -1) {
					dummy[i] = deck[i];
					deck[i] = -1;
				}
			}

			_getch();
			//더미와 덱을 바꿈
			int* tmp = deck;
			deck = dummy;
			dummy = tmp;
			//셔플

			Shuffle(MIX_COUNT, deck, DECK_COUNT_MAX);
		}//[StartGame-while-if] end 덱 섞기

		//맨위 메세지 출력
		ClearLineBack(0);
		printf("잔고 %5d\t", chip);
		printf("목표 %5d\t", target);
		printf("%2d라운드\t", round+1);

		//패배처리
		if (chip <= 0) {
			ClearLine(0, 1);
			printf("\n패배 [잔고 0]\n\n\n\n\n\n\n\n\n\n\n");

			return;
		}
		//승리처리
		if (chip >= target) {
			ClearLine(0, 1);
			printf("\n승리 [잔고 %d]\n\n\n\n\n\n\n\n\n\n\n", chip);

			return;
		}

		//컴퓨터 카드뽑기 -> 배팅
		for (int i = 0; i < deckDiff; i++) {
			batting = false;

			//컴퓨터 카드 한장뽑아서 저장후 그래픽 출력
			comSelectCard[i] = SelectCard(deck, DECK_COUNT_MAX, dummy, DECK_COUNT_MAX, i + round * (1 + deckDiff));
			PrintCard_Com(comSelectCard, i);


			//배팅중 잔고없으면 자동 스킵
			if (chip == 0) {
				ClearLineBack(POSITION_INPUT,1);
				printf("배팅 불가능(잔고 0)");
				continue;
			}

			//배팅할지 정함
			batting = isBatting(deckDiff, i);

			//배팅할 경우 1이상 작성
			while (batting) {

				//배팅 안내 텍스트
				ClearLineBack(POSITION_INPUT, 1);
				int currBatting = 0;
				printf("배팅금액[1~%d] : ", chip);

				//배팅 입력
				char* str = NULL;
				while(currBatting > chip || currBatting < 1) {
					str = new char[10];
					currBatting = inputNumber(str, 10);

					if (currBatting > chip || currBatting < 1) {
						ClearLineBack(POSITION_INPUT+1);
						printf("입력 오류");
						ChangeCursor(0,POSITION_INPUT);
						ClearLineBack(POSITION_INPUT);
						printf("배팅금액[1~%d] : ", chip);
						continue;
					}
				}//배팅 입력 종료
				ClearLine(POSITION_INPUT, 2);

				//배팅시 처리				
				chip -= currBatting;
				chipSum += currBatting;
				incomeSum += currBatting * (deckDiff - (i * MULTIPLE));
					
				//상태창에 출력
				ChangeCursor(0, POSITION_STATUS_2 + i);
				printf("배팅[%d] %d(%d) ", i + 1, (int)(currBatting * (deckDiff - (i * MULTIPLE))), currBatting);
				ChangeCursor(0, POSITION_STATUS_2 + deckDiff);
				printf("배팅합 : %d(%d) ", incomeSum, chipSum);
				
				//입력창 지움
				ClearLine(POSITION_INPUT, 1);
				
				//배팅 이벤트 나가기
				batting = false;				
			}//[StartGame-while-for-while] end 배팅 이벤트
		}//[StartGame-while-for] end 컴퓨터 카드 뽑기

		//컴퓨터 선택 카드 정렬
		SortSelection(comSelectCard, deckDiff);

		//내카드 뽑기
		playerSelectCard = SelectCard(deck, DECK_COUNT_MAX, dummy, DECK_COUNT_MAX);
		PrintCard_Player(playerSelectCard);

		ClearLine(POSITION_INPUT);

		//마음의 준비
		_getch();

		//뽑은 카드가 컴퓨터가 뽑은 카드 사이에 들어가는지 확인한다.
		//뽑은 카드가 컴퓨터가 뽑은 카드와 다른지 확인한다.
		if (Fight(comSelectCard, deckDiff, playerSelectCard)) {
			//메세지 출력
			ChangeCursor(20, POSITION_STATUS_2);
			printf("맞추기 성공");
			ChangeCursor(20, POSITION_STATUS_2+1);
			printf("%d + %d = %d", chip, incomeSum, chip+incomeSum);

			//성공시 보상+초기화
			chip += incomeSum;
			incomeSum = 0;
			chipSum = 0;
		}
		else {
			//메세지 출력
			ChangeCursor(20, POSITION_STATUS_2);
			printf("맞추기 실패");
			ChangeCursor(20, POSITION_STATUS_2+1);
			printf("%d - %d = %d", chip+chipSum, chipSum, chip);
			
			//실패시 초기화만 진행
			incomeSum = 0;
			chipSum = 0;
		}

		_getch();

		//전체 라인 클리어
		ClearLine(POSITION_GRAPHIC, POSITION_STATUS_2 - POSITION_GRAPHIC);
		ClearLine(POSITION_STATUS_2, POSITION_INPUT - POSITION_STATUS_2);
		ClearLine(POSITION_INPUT, 30 - POSITION_INPUT);

		//뽑아야 하는 카드가 적을 경우 다시 섞어준다.
		for (int i = DECK_COUNT_MAX - 1; i >= DECK_COUNT_MAX - deckDiff; i--) {
			if (deck[i] == -1) {
				deckEmpty = true;
				ChangeCursor(0, POSITION_STATUS_2);
				printf("덱에 남은 카드가 너무 적습니다.\n카드를 다시 섞습니다.");
				_getch();
				break;
			}
		}

		//라운드 증가
		round++;
	}//[StartGame-while] end 라운드 한판

}//[StartGame] end


//배팅 배수 출력
//키 입력
bool isBatting(int diff, int index) {
	while (true) {

		ClearLineBack(POSITION_INPUT);

		printf("배팅하시겠습니까?[x%.1f](N/Y) : ", diff - (index * MULTIPLE));
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

//입력함수
//getch로 문자 하나씩 인식한뒤 숫자만 입력한다.
//특정 범위를 벗어날경우 자동 입력.
int inputNumber(char* str, int length) {
	//초기
	int number = 2;

	//마지막 1글자는 '\0'을 입력하기 위해 적지않는다.
	for (int i = 0; i < length; i++) {
		//한 글자씩 입력
		int character = _getch();


		//마지막 인덱스 or 엔터(getch에서는 '\r') 입력시 
		//문자열 -> 숫자 함수 사용 : atoi()
		if (i==length-1 || character == '\r') {
			str[i + 1] = '\0';
			number = atoi(str);
			break;
		}

		//숫자 입력시 print하고 저장
		if (character >= '0' && character <= '9') {
			str[i] = character;
			printf("%c", str[i]);
			continue;
		}

		//백스페이스 입력시 지우기
		//지운 위치부터 인덱스 다시 시작
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

		//특수문자 입력시 getch로 한번 더 입력 받는다.
		//인덱스 위치 고정
		if (str[i] == 224) {
			i = i - 1;
			_getch();
		}
		//기타 다른 문자 입력시 인덱스 위치 고정
		else {
			i = i - 1;
		}
		
	}

	return number;
}//[inputNumber] end


//플레이어 카드 그리기 함수(외부)
//이것을 호출한다.
void PrintCard_Player(int card) {
	int offset = 2;

	PrintString_Card_Player("┌───────────┐", offset++);
	PrintString_Card_Player(card, true, offset++);
	PrintString_Card_Player("│           │", offset++);
	PrintString_Card_Player("│           │", offset++);
	PrintString_Card_Player("│           │", offset++);
	PrintString_Card_Player("│           │", offset++);
	PrintString_Card_Player("│           │", offset++);
	PrintString_Card_Player(card, false, offset++);
	PrintString_Card_Player("└───────────┘", offset++);

}//[PrintCard_Player] end

//플레이어 카드 그리기 함수(내부)
//#1 상수 문자열
int PrintString_Card_Player(const char* str, int offset) {
	ChangeCursor(19, offset);
	printf("%s", str);
	return offset + 1;
}
//#2 카드 변수
int PrintString_Card_Player(int cardNumber, bool isTopPosition, int offset) {

	ChangeCursor(19, offset);
	printf("│");
	if (!isTopPosition) {
		printf("       ");
	}
	printf("%s%2s", CARD_PATTERN[cardNumber / 13], CARD_NUMBER[cardNumber % 13]);
	if (isTopPosition) {
		printf("       ");
	}
	printf("│");

	return offset + 1;

}//[PrintString_Card_Player] end

//컴퓨터 카드 그리기 함수 (외부)
//이것을 호출한다.
void PrintCard_Com(int* arr, int index) {
	int currCard = arr[index];
	int offset = 0;

	PrintString_Card_Com("┌───────────┐", index, offset++);
	PrintString_Card_Com(currCard, true, index, offset++);
	PrintString_Card_Com("│           │", index, offset++);
	PrintString_Card_Com("│           │", index, offset++);
	PrintString_Card_Com("│           │", index, offset++);
	PrintString_Card_Com("│           │", index, offset++);
	PrintString_Card_Com("│           │", index, offset++);
	PrintString_Card_Com(currCard, false, index, offset++);
	PrintString_Card_Com("└───────────┘", index, offset++);

	//카드 겹칠때 연결 처리
	if (index > 0) {
		ChangeCursor(12 + (index), POSITION_GRAPHIC + (index * 2));
		printf("┴");
	}

}//[PrintCard_Com] end

//컴퓨터 카드 그리기 함수(내부)
//2가지 경우
//상수 문자열을 받을 경우 바로 출력한다.
//겹쳐진다
int PrintString_Card_Com(const char* str, int index, int offset) {
	ChangeCursor(1, POSITION_GRAPHIC + (index * 2) + offset);
	for (int i = 1; i < (offset / 7) * index; i++) {
		printf(" ");
	}

	ChangeCursor(1 + (index * 1), POSITION_GRAPHIC + (index * 2) + offset);
	printf("%s", str);
	return offset + 1;
}
//변수를 받을 경우 특정 위치에 문양과 번호를 출력한다.
int PrintString_Card_Com(int cardNumber, bool isTopPosition, int index, int offset) {
	ChangeCursor(1, POSITION_GRAPHIC + (index * 2) + offset);
	for (int i = 1; i < (offset / 7) * index; i++) {
		printf(" ");
	}

	ChangeCursor(1 + (index * 1), POSITION_GRAPHIC + (index * 2) + offset);
	printf("│");
	if (!isTopPosition) {
		printf("       ");
	}
	printf("%s%2s", CARD_PATTERN[cardNumber / 13], CARD_NUMBER[cardNumber % 13]);
	if (isTopPosition) {
		printf("       ");
	}
	printf("│");

	//글자 지우기
	if (!isTopPosition) {
		ClearLine((short)(19 - 11 + (index * 1)));
	}
	return offset + 1;
}//[PrintString_Card_Com] end


//카드를 한장 뽑고 더미에 넣는다.
//값만 return한다
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

//카드를 한장 뽑음
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

//뽑은 카드를 더미에 추가함
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


//제일 작은 수와 제일 큰 수 사이 + 그 외 카드의 숫자와 같으면 패배
//그 외의 경우
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


//스왑
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}//[Swap] end

//커서이동함수
//coordinate(좌표계)
//windows.h
void ChangeCursor(short x, short y) {
	COORD Cur = { x,y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Cur);
}//[ChangeCursor] end

//현재줄 비우기
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


//라인 클리어 후 입력한 라인으로 이동
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

//선택정렬
int SortSelection(int* arr, int size) {
	//for문 횟수
	int count = 0;
	int min = 0;

	//0~size-1까지 비교
	for (int i = 0; i < size - 1; i++) {
		min = i;
		// i부터 size까지
		for (int j = i; j < size; j++) {

			//작은 값 제일 왼쪽으로 정렬(오름차수)
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
