/************************** ��� ���� ********************************/
#include <iostream>
#include <cstdlib>
/************************** �Լ� ���� ********************************/


float criticalHit(int damage) {
	return damage * 1.5f;
}

void criticalHit2(int damage) {

	printf("ũ��Ƽ�� ��Ʈ %f\n",damage*1.5f);

	return;
}


void criticalHit3(int damage, float criticalPercent) {

	float criticalDamage = criticalPercent * 0.01f;

	printf("ũ��Ƽ�� ��Ʈ %f\n", damage * criticalDamage);

	return;
}



/************************** ���� �Լ� ********************************/
int main() {

	int damage = 100;

	printf("ũ��Ƽ�� ��Ʈ %f\n", criticalHit(damage));
	criticalHit2(100);
	criticalHit3(100, 150);






	for (int i = 0; i < 10; i++){
		if (rand() % 100 > 50) {
			printf("%d. ", i + 1);
			criticalHit3(100, 150);
		}
	}

	return 0;
}