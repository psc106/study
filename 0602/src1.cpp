#include <iostream>


void main() {

	int a=0;

	while (true) {

		//�� ������ a���� 1 ����
		a += 1;

		//a==8�� ��� while���� �� �����Ѵ�.(continue)
		if (a == 8) {
			continue;
		}
		
		//���� a ���
		printf("%d\n", a);
	
		//a==10�� ��� while���� �����Ѵ�.	(break)
		if (a==10) {
			break;
		}


	}//while edge


}	//main edge

