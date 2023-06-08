#include <iostream> 

void Func();

void InputArray(int*, int);
void Swap(int*, int*);

int SortSelection(int*, int);
int SortBubble(int*, int);
int SortInsert(int*, int);

void main() {

	Func();

}//[main] end

void Func() {

	const int size = 5;
	int arr[size] = { 0, };

	//입력
	InputArray(arr, size);

	//입력값 출력
	printf("=======입력값=======\n");
	for (int i = 0; i < size; i++) {
		printf("%d ", arr[i]);
	}
	printf("\n");

	//정렬
	//int count = SortSelection(arr, size);
	//int count = SortBubble(arr, size);
	int count = SortInsert(arr, size);
	
	//정렬값 출력
	printf("=======정렬값=======\n");
	for (int i = 0; i < size; i++) {
		printf("%d ", arr[i]);
	}
	printf(" (%d번)\n", count);
}//[Func] end

//size 만큼 입력
void InputArray(int* arr, int size) {

	for (int i = 0; i < size; i++) {
		scanf_s("%d", (arr+i));
	}

}//[InputArray] end


//선택 정렬
//정렬 안된 처음값 ~ size-1까지의 값을 찾아 처음값에 저장시킴
//size-1 까지 하는 이유 : size-1까지 하면 자동으로 정렬됨
int SortSelection(int* arr, int size) {
	//for문 횟수
	int count = 0;
	int min = 0;

	//0~size-1까지 비교
	for (int i = 0; i < size - 1; i++) {
		min = i;
		// i부터 size까지
		for (int j = i; j < size; j++) {

			//작은 값 제일 왼쪽으로 정렬(오름차수)
			if (arr[min] > arr[j]) {
				min = j;
			}
			count++;
		}
		Swap(&arr[min], &arr[i]);
		count++;
	}
	return count;
}//[SortSelection] end

//버블 정렬
//정렬 안된 처음값 ~ size-1-i까지 계속 비교하며 정렬시킴
int SortBubble(int* arr, int size) {
	//for문 횟수
	int count = 0;

	//0 ~ size-1 까지 
	for (int i = 0; i < size - 1; i++) {
		
		//0 ~ size-1-i 까지
		for (int j = 0; j < size - 1 - i; j++) {
			
			//계속 비교해서 정렬(오름차수)
			if (arr[j] > arr[j + 1]) {
				Swap(&arr[j], &arr[j + 1]);
			}
			count++;
		}
		count++;
	}

	return count;
}//[SortBubble] end

//삽입 정렬
int SortInsert(int* arr, int size) {
	//for문 횟수
	int count = 0;
	int target = 0;

	for (int i = 0; i < size; i++) {

		//타겟 지정
		target = i;

		//타겟인덱스 이전 배열 비교
		for (int j = target-1; j >= 0; j--) {

			//스왑 안될때까지 계속 비교
			if (arr[target] < arr[j]) {
				Swap(&arr[target], &arr[j]);
				target = j;
			}
			//스왑 안될경우 정렬되어 있다고 가정하고 비교 종료
			else {
				break;
			}
			count++;
		}
		count++;
	}

	return count;
}//[SortInsert] end


//스왑
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}//[Swap] end