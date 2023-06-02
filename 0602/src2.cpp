#include <iostream>

//입력값 중 쓰레기값 삭제
void remove() {
	char inputRemove = 0;

	//엔터(\n)가 나오기 전까지 한글자씩 비워준다.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge
}	//remove edge

void main() {

	//선언+초기화
	char inputPlayerAction = 0;
	int mode=-1;

	//무한 루프
	while (true) {
		printf("\n");

		//최초 1회 실행
		if (mode == -1) {
			printf("[튜토리얼]\n");
			printf("특정 문자를 입력하면 해당 문자에 맞는 액션을 실행합니다.\n");

			mode = 0;
		}

		//입력
		printf("a. 공격 d. 방어 q. 종료\n");
		printf("입력 : ");
		scanf_s("%c", &inputPlayerAction); 
		
		//q 입력시 종료
		if (inputPlayerAction == 'q'|| inputPlayerAction == 'Q') {
			printf("종료\n");
			break;
		}
		else if (inputPlayerAction == 'a' || inputPlayerAction == 'A') {
			printf("공격\n");
		}
		else if (inputPlayerAction == 'd' || inputPlayerAction == 'D') {
			printf("방어\n");
		}
		else{
			printf("오류\n");
		}
		
		//입력 초기화 함수
		remove();

	}//while edge

}//main edge