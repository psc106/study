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

	//�Է�
	InputArray(arr, size);

	//�Է°� ���
	printf("=======�Է°�=======\n");
	for (int i = 0; i < size; i++) {
		printf("%d ", arr[i]);
	}
	printf("\n");

	//����
	//int count = SortSelection(arr, size);
	//int count = SortBubble(arr, size);
	int count = SortInsert(arr, size);
	
	//���İ� ���
	printf("=======���İ�=======\n");
	for (int i = 0; i < size; i++) {
		printf("%d ", arr[i]);
	}
	printf(" (%d��)\n", count);
}//[Func] end

//size ��ŭ �Է�
void InputArray(int* arr, int size) {

	for (int i = 0; i < size; i++) {
		scanf_s("%d", (arr+i));
	}

}//[InputArray] end


//���� ����
//���� �ȵ� ó���� ~ size-1������ ���� ã�� ó������ �����Ŵ
//size-1 ���� �ϴ� ���� : size-1���� �ϸ� �ڵ����� ���ĵ�
int SortSelection(int* arr, int size) {
	//for�� Ƚ��
	int count = 0;
	int min = 0;

	//0~size-1���� ��
	for (int i = 0; i < size - 1; i++) {
		min = i;
		// i���� size����
		for (int j = i; j < size; j++) {

			//���� �� ���� �������� ����(��������)
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

//���� ����
//���� �ȵ� ó���� ~ size-1-i���� ��� ���ϸ� ���Ľ�Ŵ
int SortBubble(int* arr, int size) {
	//for�� Ƚ��
	int count = 0;

	//0 ~ size-1 ���� 
	for (int i = 0; i < size - 1; i++) {
		
		//0 ~ size-1-i ����
		for (int j = 0; j < size - 1 - i; j++) {
			
			//��� ���ؼ� ����(��������)
			if (arr[j] > arr[j + 1]) {
				Swap(&arr[j], &arr[j + 1]);
			}
			count++;
		}
		count++;
	}

	return count;
}//[SortBubble] end

//���� ����
int SortInsert(int* arr, int size) {
	//for�� Ƚ��
	int count = 0;
	int target = 0;

	for (int i = 0; i < size; i++) {

		//Ÿ�� ����
		target = i;

		//Ÿ���ε��� ���� �迭 ��
		for (int j = target-1; j >= 0; j--) {

			//���� �ȵɶ����� ��� ��
			if (arr[target] < arr[j]) {
				Swap(&arr[target], &arr[j]);
				target = j;
			}
			//���� �ȵɰ�� ���ĵǾ� �ִٰ� �����ϰ� �� ����
			else {
				break;
			}
			count++;
		}
		count++;
	}

	return count;
}//[SortInsert] end


//����
void Swap(int* num1, int* num2) {
	int tmp = *num1;
	*num1 = *num2;
	*num2 = tmp;
}//[Swap] end