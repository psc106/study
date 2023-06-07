#include <iostream>

bool isSmall(char);
bool isBig(char);
int getArrSize(char[]);

void main() {

	char str1[100] = {0, };
	char str2[100] = {0, };
	char tmp[100]  = {0, };
	int alphaDiff = 'a' - 'A';
	int size = 0;

	printf("�Է� : \n");
	std::cin >> str1;

	for (int i = 0; str1[i] != '\0'; i++) {

		if (i % 2 == 0) {
			if (IsSmall(str1[i])) {
				str1[i] -= alphaDiff;
			}
		}
		else if (i % 2 == 1) {
			if (IsBig(str1[i])) {
				str1[i] += alphaDiff;
			}
		}
	}
	printf("%s\n", str1);


	//�Է� �� �ǵ���
	printf("�Է� : \n");
	std::cin >> tmp;

	//�Է��� ���̸� ã�´�
	size = GetArrSize(tmp);

	//���ڰ� �����ִ� ������ �ε�����
	int lastCharIndex = size - 1;

	//������ ���ں��� ���or����
	for (int i = lastCharIndex; i>=0; i--) {
		str2[lastCharIndex - i] = tmp[i];

		printf("%c", tmp[i]);
	}
	str2[size] = '\0';


	printf("\n%s", str2);

}

//�ҹ���
bool IsSmall(char char_) {
	return char_ >= 'a' && char_ <= 'z';
}

//�빮��
bool IsBig(char char_) {
	return char_ >= 'A' && char_ <= 'Z';
}

//���ڿ� �迭 ������ ũ��
int GetArrSize(char arr[]) {
	int size = 0;
	for (int i = 0; arr[i] != '\0'; i++) {
		size++;
	}
	return size;
}
