#include <iostream>

bool isNum(char);
bool isCapital(char);
bool isNonCapital(char);
bool isNotSpText(char);

bool isRight(int num) {
	return num >= 3 && num <= 10;
}

void main() {
	////�Է�
	//char character = 0;
	//scanf_s("%c", &character);


	////���ǹ�
	//if (isCapital(character)) {
	//	printf("�빮��\n");
	//}
	//else if (isNonCapital(character)) {
	//	printf("�ҹ���\n");
	//}
	//else if (isNum(character)) {
	//	printf("����\n");
	//}
	//else {
	//	printf("Ư������\n");
	//}

	////while��1
	//printf("\n");
	//int loop = 100, loopOut = 0;
	//while (loop> loopOut) {
	//	printf("%3d. hello\n",101-loop--);
	//}	//while ����

	////while��2
	//printf("\n");
	//loop = 0; loopOut = 10;
	//while (loop < loopOut) {
	//	printf("%2d\n", ++loop);
	//}

	int count=0;
	
	while (count < 3 || count > 10) {
		printf("�Է�(3~10) : ");
		scanf_s("%d", &count);

		if (!isRight(count)) {
			printf("�ٽ� �Է��ϼ���\n");
		}
	}

	//�ݺ��� 2��
	int vertical = 0;
	while (vertical < count) {
		int horizen = 0;

		printf("%d. \t", vertical+1);
		while (horizen <count) {
			printf("*");
			horizen++;
		}
		printf("\n");
		vertical++;
	}
	printf("\n\n");

	//�ݺ��� 1��
	int newCount = count * count;
	int horizen = 0;
	while (horizen < newCount) {
		if (horizen % count == 0) {
			printf("%d.\t",(horizen/count)+1);
		}
		if (horizen % count != count-1) {
			printf("*");
		}
		else {
			printf("\n");
		}
		horizen++;
	}



	//for��
/*	printf("\n");
	for (int j = 0; j < 10; j++) {
		printf("%d\n", j + 1);
	}	*///for ����


}

bool isNum(char inputChar) {
	return ('0' <= inputChar) && (inputChar <= '9');
}

bool isCapital(char inputChar) {
	return ('A' <= inputChar) && (inputChar <= 'Z');
}

bool isNonCapital(char inputChar) {
	return ('a' <= inputChar) && (inputChar <= 'z');
}

bool isNotSpText(char inputChar) {
	return	isCapital(inputChar) || isNonCapital(inputChar) || isNum(inputChar);
}