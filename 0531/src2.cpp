/************************** 헤더 파일 ********************************/
#include <iostream>
#include <cstdlib>
/************************** 함수 정의 ********************************/


float criticalHit(int damage) {
	return damage * 1.5f;
}

void criticalHit2(int damage) {

	printf("크리티컬 히트 %f\n",damage*1.5f);

	return;
}


void criticalHit3(int damage, float criticalPercent) {

	float criticalDamage = criticalPercent * 0.01f;

	printf("크리티컬 히트 %f\n", damage * criticalDamage);

	return;
}



/************************** 메인 함수 ********************************/
int main() {

	int damage = 100;

	printf("크리티컬 히트 %f\n", criticalHit(damage));
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