#include <iostream> 
#include <conio.h> 

void Func() {

	int map[6][6] = { 0, };
	int size = 0;

	while (size < 3 || size >6) {
		scanf_s("%d", &size);
	}

	while (true) {
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				map[j][i] = (j + 1) + size*i;
			}
		}

		map[size - 1][size - 1] = 0;


		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				printf("%2d ", map[j][i]);
			}
			printf("\n");
		}
		_getch();
	}

}

void main() {

	Func();
}