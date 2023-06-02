#include <iostream>

//입력값 중 쓰레기값 삭제
void remove() {

	char inputRemove = 0;

	//엔터(\n)가 나오기 전까지 한글자씩 비워준다.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge

}	//remove edge


//현재 입력한 값에 문자가 남아있는지 확인
//'\n'이 아니면 예외처리
bool isEsterCharacter() {

	char trashInput = 0;
	scanf_s("%c", &trashInput);

	return trashInput == '\n';
}	//check edge


void main() {

	int number = 0;

	//무한루프
	while (true) {

		//입력
		printf("홀짝 판별(0. 종료) : ");
		scanf_s("%d", &number);

		//문자 예외처리
		if (!isEsterCharacter()) {
			printf("오류\n\n");
			remove();
			continue;
		}

		//0일 경우 종료
		if (number == 0) {
			printf("종료\n");
			break;
		}
		//mod연산 결과값 0일 경우 짝수
		else if (number % 2 == 0) {
			printf("%d는 짝수\n\n", number);
			continue;
		}

		//mod연산 결과값 1/-1일 경우 홀수
		printf("%d는 홀수\n\n", number);

	}//while edge


}//main edge