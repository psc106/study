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

	printf("입력 : \n");
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


	//입력 후 되돌림
	printf("입력 : \n");
	std::cin >> tmp;

	//입력한 길이를 찾는다
	size = GetArrSize(tmp);

	//글자가 적혀있는 마지막 인덱스값
	int lastCharIndex = size - 1;

	//마지막 글자부터 출력or변경
	for (int i = lastCharIndex; i>=0; i--) {
		str2[lastCharIndex - i] = tmp[i];

		printf("%c", tmp[i]);
	}
	str2[size] = '\0';


	printf("\n%s", str2);

}

//소문자
bool IsSmall(char char_) {
	return char_ >= 'a' && char_ <= 'z';
}

//대문자
bool IsBig(char char_) {
	return char_ >= 'A' && char_ <= 'Z';
}

//문자열 배열 실제적 크기
int GetArrSize(char arr[]) {
	int size = 0;
	for (int i = 0; arr[i] != '\0'; i++) {
		size++;
	}
	return size;
}
