#include <iostream>

//�Է°� �� �����Ⱚ ����
void remove() {

	char inputRemove = 0;

	//����(\n)�� ������ ������ �ѱ��ھ� ����ش�.
	while (inputRemove != '\n') {
		scanf_s("%c", &inputRemove);
	}	//remove-while edge

}	//remove edge


//���� �Է��� ���� ���ڰ� �����ִ��� Ȯ��
//'\n'�� �ƴϸ� ����ó��
bool isEsterCharacter() {

	char trashInput = 0;
	scanf_s("%c", &trashInput);

	return trashInput == '\n';
}	//check edge


void main() {

	int number = 0;

	//���ѷ���
	while (true) {

		//�Է�
		printf("Ȧ¦ �Ǻ�(0. ����) : ");
		scanf_s("%d", &number);

		//���� ����ó��
		if (!isEsterCharacter()) {
			printf("����\n\n");
			remove();
			continue;
		}

		//0�� ��� ����
		if (number == 0) {
			printf("����\n");
			break;
		}
		//mod���� ����� 0�� ��� ¦��
		else if (number % 2 == 0) {
			printf("%d�� ¦��\n\n", number);
			continue;
		}

		//mod���� ����� 1/-1�� ��� Ȧ��
		printf("%d�� Ȧ��\n\n", number);

	}//while edge


}//main edge