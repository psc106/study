#include <iostream>


void main() {

	int a=0;

	while (true) {

		//매 루프당 a값이 1 증가
		a += 1;

		//a==8일 경우 while문을 재 실행한다.(continue)
		if (a == 8) {
			continue;
		}
		
		//현재 a 출력
		printf("%d\n", a);
	
		//a==10일 경우 while문을 종료한다.	(break)
		if (a==10) {
			break;
		}


	}//while edge


}	//main edge

