#include <iostream>
#include <string>

void main() {
	wchar_t a = '¡Ú';
	printf("%wc", a);


	int* p = NULL;
	int size = 4;

	scanf_s("%d", &size);

	int* num = new int[size];


	p = (int*)malloc(sizeof(int) * size);
	int **pt = (int**)malloc(sizeof(int*)*4);

	pt[0] = p;
	pt[1] = p;
	pt[2] = p;
	pt[3] = p;

	if (p != NULL) 
	{
		*(p) = 1;
		*(p + 1) = 2;
		*(p + 2) = 3;
		*(p + 3) = 4;

		for (int i = 0; i < size; i++)
		{
			printf("%d\n", p[i]);
		}
	}

	if (p != NULL)
	{
		*(p) = 1;
		*(p + 1) = 2;
		*(p + 2) = 3;
		*(p + 3) = 4;

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++) {
				printf("%d ", (pt[i])[j]);
			}
			printf("\n");
		}
	}

	free(*pt);
	free(pt);
	free(p);

	delete &num;
}