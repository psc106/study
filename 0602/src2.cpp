#include <iostream>

//�Է°� �� �����Ⱚ ����
void remove() {
	char inputRemove = 0;

	//����(\n)�� ������ ������ �ѱ��ھ� ����ش�.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge
}	//remove edge

void main() {

	//����+�ʱ�ȭ
	char inputPlayerAction = 0;
	int mode=-1;

	//���� ����
	while (true) {
		printf("\n");

		//���� 1ȸ ����
		if (mode == -1) {
			printf("[Ʃ�丮��]\n");
			printf("Ư�� ���ڸ� �Է��ϸ� �ش� ���ڿ� �´� �׼��� �����մϴ�.\n");

			mode = 0;
		}

		//�Է�
		printf("a. ���� d. ��� q. ����\n");
		printf("�Է� : ");
		scanf_s("%c", &inputPlayerAction); 
		
		//q �Է½� ����
		if (inputPlayerAction == 'q'|| inputPlayerAction == 'Q') {
			printf("����\n");
			break;
		}
		else if (inputPlayerAction == 'a' || inputPlayerAction == 'A') {
			printf("����\n");
		}
		else if (inputPlayerAction == 'd' || inputPlayerAction == 'D') {
			printf("���\n");
		}
		else{
			printf("����\n");
		}
		
		//�Է� �ʱ�ȭ �Լ�
		remove();

	}//while edge

}//main edge