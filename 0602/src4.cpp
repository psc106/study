#include <iostream>
#include <string>
#include <conio.h>

void modeTest(int mode) {

	//break������ �������� ����
	switch (mode) {
	case 2346782:
		printf("2346782 -> ");
	case 1:
		printf("1 -> ");
	case 2:
		printf("2\t");
		printf("�극��ũ\n");
		break;
	case 3:
		printf("3 -> ");
	case 4:
		printf("4 -> ");
	case 5:
		printf("5 -> ");
	case 'a':
		printf("a -> ");
	default:
		printf("���ܻ���\t");
		printf("�극��ũ\n");
		break;
	}//modeTest switch edge

}//modeTest edge

void main() {

	int mode = 0;

	mode = 3;
	modeTest(mode);

	printf("\n\n");

	mode = 1;
	modeTest(mode);

	//�ѱ��� �Է�
	mode = _getch();


	
	for (int i = 0; i < 100; i++) {
		
		printf("%d\n", i+1);

	}//for edge



	//���� �ݺ�
	for (; 1; ) {
		printf("����");
	}
	while (1) {
		printf("����");
	}

}	//main edge