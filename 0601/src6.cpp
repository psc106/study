#include <iostream>

bool isRight(int num) {
	return num >= 3 && num <= 10;
}

void main() {

	int count = 0;

	//�Է�(3~10)
	while (count < 3 || count > 10) {
		printf("�Է�(3~10) : ");
		scanf_s("%d", &count);

		//�ƴϸ� �ٽ� �Է�
		if (!isRight(count)) {
			printf("�ٽ� �Է��ϼ���\n");
		}
	}

	//�ݺ��� 2��
	//����Ƚ��x����Ƚ��
	//ù��° �ݺ��� : ����
	//�ι�° �ݺ��� : ����
	
	//������ n�� �����Ѵ�.
	int vertical = 0;
	while (vertical < count) {
		//ù ���� ���� ���
		printf("%d. \t", vertical + 1);

		//������ n�� �����Ѵ�.
		int horizen = 0;
		while (horizen < count) {
			printf("* ");
			horizen++;
		}

		//���� ��� �Է��� ���� �������� �̵�
		printf("\n");
		vertical++;
	}

	printf("\n\n");

	//�ݺ��� 1��
	//n*n�� ����ϴ� ����
	int newCount = count * count;
	int horizen = 0;
	while (horizen < newCount) {
		//ù ���� ���� ���
		if (horizen % count == 0) {
			printf("%d.\t", (horizen / count) + 1);
		}

		//������ n*n�� ���
		printf("* ");

		//������ ���� ���� ��������
		if (horizen % count == count - 1) {
			printf("\n");
		}

		horizen++;
	}







	printf("*"); printf("*"); printf("*"); printf("\n"); 
	printf("*"); printf("*"); printf("*"); printf("\n");  
	printf("*"); printf("*"); printf("*"); printf("\n");

	printf("* * * \n");
	printf("* * * \n");
	printf("* * * \n");

}