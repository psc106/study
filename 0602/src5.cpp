#include <iostream>
#include <stdlib.h>
#include <time.h>

using namespace std;

void main() {
	int randomNum=0;
	srand((unsigned int)time(NULL));

	randomNum = (rand() % 333-31) + 31;
	printf("%d\n", randomNum);
	randomNum = (rand() % 6) + 1;
	printf("%d\n", randomNum);

}
