#include<iostream> 
#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

//문자열 스왑
void SwapString(char** a, char** b) {
	char* tmp = *a;
	*a = *b;
	*b = tmp;
}

const char* cardpattern(int num) {
	if (num == 0) {

		return "diamond";
	}
	else if (num == 1) {

		return "spade";
	}
	else if (num == 2) {

		return "heart";
	}
	else if (num == 3) {

		return "clover";
	}

	return "";
}

// 파라미터로 주어지는 문자열은 const로 주어집니다. 변경하려면 문자열을 복사해서 사용하세요.
char* solution(const char* str1, const char* str2) {
	// return 값은 malloc 등 동적 할당을 사용해주세요. 할당 길이는 상황에 맞게 변경해주세요.
	int strSize = 0;

	for (int i = 0; str1[i] != '\0'; i++) {
		strSize++;
	}

	char* answer = (char*)malloc(sizeof(char*)*((strSize*2)+1));

	for (int i = 0; i < strSize; i++) {
		*(answer+2*i) = str1[i];
		*(answer+2*i+1) = str2[i];
	}
	answer[strSize * 2] = '\0';


	return answer;
}


// 파라미터로 주어지는 문자열은 const로 주어집니다. 변경하려면 문자열을 복사해서 사용하세요.
char* solution(const char* my_string, const char* overwrite_string, int s) {
	// return 값은 malloc 등 동적 할당을 사용해주세요. 할당 길이는 상황에 맞게 변경해주세요.
	int mySize=0, overSize=0;

	for (int i = 0; my_string[i] != '\0'; i++) {
		mySize++;
	}
	for (int i = 0; overwrite_string[i] != '\0'; i++) {
		overSize++;
	}

	printf("%d %d \n", overSize, mySize);

	int bigSize = overSize + s > mySize? overSize + s : mySize;

	bool isOver = false;
	char* answer = (char*)malloc(sizeof(char)*(bigSize+1));
	

	for (int i = 0; i<mySize; i++) {
		if (i == s) {
			for (int j = 0; overwrite_string[j] != '\0'; j++) {
				answer[s+j] = overwrite_string[j];
				i = s+j+1;
				printf("`%d %d\n",j, s+j);
			}
		}
		i;

		printf("_%d\n", i);
		answer[i] = my_string[i];
	}
	answer[bigSize] = '\0';
	printf("%s\n", answer);

	return answer;
}


void main() {

	printf("%s\n", solution("He11oWor1d", "lloWorl", 2));

	//문자열 저장
	char* str[7] = {NULL,};

	char patternString1[] = { "diamond" };
	char patternString2[] = { "heart" };
	char patternString3[] = { "spade" };
	char patternString4[] = { "clover" };

	str[0] = patternString1;
	str[1] = patternString2;
	str[2] = patternString3;
	str[3] = patternString4;
	str[4] = patternString1;
	str[5] = patternString2;
	str[6] = patternString3;

	for (int i = 0; i < 7; i++) {
		printf("%s\n", *(str +i));
	}
	printf("\n");

	SwapString(&str[0], &str[1]);

	for (int i = 0; i < 7; i++) {
		printf("%s\n", *(str + i));
	}
}

