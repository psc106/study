#include <iostream>
#include <windows.h>
#include <conio.h>
#include <stdlib.h>

/****************** 상수 선언 시작 *********************/
const int UP = 72;
const int DOWN = 80;
const int LEFT = 75;
const int RIGHT = 77;

const int MAP_SIZE_X = 16;
const int MAP_SIZE_Y = 16;
const int MAP_FIELD_COUNT_MAX = 3;
const int MAP_FIELD_ROAD = 0;
const int MAP_FIELD_RIVER = 1;
const int MAP_FIELD_MOUNTAIN = 2;

const int PLAYER_HP_DEFAULT = 50;
const int PLAYER_HP_MAX = 100;
const int PLAYER_STR_DEFAULT = 10;

const int MONSTER_HP_DEFAULT = 30;
const int MONSTER_STR_DEFAULT = 5;

const int TRAP_DAMAGE = 2;
const int EDGE_DAMAGE = 4;
const int FIELD_DAMAGE = 1;

const int SCORE_WALK = 1;
const int SCORE_EXIT = 10;
const int SCORE_BATTLE = 15;

const int HEAL_POTION = 8;
const int HEAL_EXIT = 8;

const int TRAP_COUNT_MAX = 7;
const int POTION_COUNT_MAX = 3;

const float DIFFICULT_RANK_FIELD = 0.15f;
const float DIFFICULT_RANK_BATTLE = 0.2f;

const char KEY_QUIT = 'q';
const char KEY_HEAL = 'h';
/****************** 상수 선언 종료 *********************/


/****************** 함수 선언 시작 *********************/
int moveX(int);//x축 이동
int moveY(int);//y축 이동
int edgeMove(int, int);//가장 자리 이동처리

int diffCalc(int difficult, float count); //난이도 계수에 맞게 계산

void printStatus(int, int, int, int, int);//기본 정보 print
void printEnterField(int, int);			//
void printField(int);						//현재 필드 정보 print
/****************** 함수 선언 종료 *********************/

//스코어 존재. 가장자리로 가면 반대편으로 가짐
//이동시 체력이 일정량 깎임(난이도 높아질수록 체력 많이 깎임)
//맵마다 컨셉이 다름
//길 : 포션드랍  
//강 : 장애물 
//산 : 적생성
//포션은 전투중에는 못먹고 일정키를 입력해야 먹어짐
//장애물은 행마다 패턴이 똑같음.
//적은 내가 이동할때마다 나를 따라오며, 난이도에 따라 점점 강해짐

/****************** 메인 시작 *********************/
void main() {

	/*	
	isStart : 게임 시작 유무
	isTrapEvent : 장애물 밟았는지
	isEdgeEvent : 가장자리로 이동했는지
	isDeadEvent : 죽었는지
	isBattleEvent : 전투했는지
	isExitEvent : 탈출했는지
	isPotionEvent : 포션 획득
	isPotionUseEvent : 포션 사용
	isPotionEmptyEvent : 포션 없음
	*/
	bool isStart = false;
	bool isTrapEvent = false;
	bool isEdgeEvent = false;
	bool isDeadEvent = false;
	bool isBattleEvent = false;
	bool isExitEvent = false;
	bool isPotionEvent = false;
	bool isPotionUseEvent = false;
	bool isPotionEmptyEvent = false;

	int difficult = 0;	//난이도
	int score = 0;		//점수
	int action = -1;	//누른 키 값 저장
	int playerDamageSum = 0;	//전투에서 받은 데미지
	int potionCount = 0;		//포션 갯수
	int currfieldDamage = 1;	//현재 걷기 데미지

	//스탯 정보
	int playerHP = PLAYER_HP_DEFAULT;
	int playerSTR = PLAYER_STR_DEFAULT;
	int monsterHP = MONSTER_HP_DEFAULT;
	int monsterSTR = MONSTER_STR_DEFAULT;

	//위치정보
	int currX = 0, currY = 0;
	int currMonsterX = -100, currMonsterY = -100;
	int exitX = 6, exitY = 0;
	int potionX = -100, potionY = -100;

	int currMapField = 0;	//현재 맵 필드 종류
	int FieldSelector = 0;//다음 필드 뭔지 정하는 확률 비교 변수
	unsigned short pattern = 0;	//장애물 패턴. 2^17승-1 까지 저장해서 비트로 비교
	unsigned int offset = 0;		//장애물 패턴의 다양성을 만드는 변수. offset만큼 오른쪽으로 이동시킴


	srand((unsigned int)time(NULL));

	while (true) {
		//콘솔에 적혀있던 글자들 없애기
		if (!isStart) {
			Sleep(50);
			system("cls");
			printf("\n");
		}

		//맵 정보 출력
		printField(currMapField);
		printf("\t");
		printf(" 걷기[+%d] ", SCORE_WALK);
		printf(" 탈출[+%d] ", SCORE_EXIT);
		printf(" 전투승리[+%d]\t\t", SCORE_BATTLE);
		printf("걷기 데미지[-%2d]", currfieldDamage);
		printf("\n");

		//맵 그리기
		//(vertical, horizen)		 
		//(3,0) (3,1) ...
		//(2,0) (2,1) ...
		//(1,0) (1,1) ...
		//(0,0) (0,1) ...
		for (int vertical = MAP_SIZE_Y - 1; vertical >= 0; vertical--) {
			printf("\t\t\t");

			//비트 검사용 변수
			int PatternChecKer;

			//장애물 패턴을 좀더 다채롭게 하려고 offset만큼 밀어줌
			//필드가 강일 때만 계산
			if (currMapField == MAP_FIELD_RIVER) {
				PatternChecKer = 1 << (offset * vertical) % MAP_SIZE_X;
			}

			for (int horizen = 0; horizen < MAP_SIZE_X; horizen++) {
				bool isTrap = false;

				//비트 연산으로 장애물 판별
				// 0001 1101 0000 0011
				// 1000 0000 0000 0000
				// 식으로 하나씩 비교해서 값이 있으면 장애물임
				//필드가 강일 때만 계산
				if (currMapField == MAP_FIELD_RIVER) {
					//현재 위치가 장애물인지 확인
					isTrap = (pattern & PatternChecKer) == PatternChecKer ? true : false;

					//비트 검사용 변수 처리
					PatternChecKer = PatternChecKer >> 1;
					if (PatternChecKer == 0) {
						PatternChecKer = 1 << 16;
					}
				}

				//현재 위치에서 맞는 텍스트 출력

				//플레이어
				if (horizen == currX && vertical == currY) {
					//탈출후 바로 밟는건 데미지 처리 안함
					if (isTrap&&!isExitEvent) {
						playerHP -= TRAP_DAMAGE- FIELD_DAMAGE;
						isTrapEvent = true;
					}
					if (playerHP <= 0) {
						printf("♨");
						isDeadEvent = true;
					}
					else {
						printf("나");
					}
				}
				//몬스터
				else if (monsterHP > 0 && (horizen == currMonsterX && vertical == currMonsterY)) {
					printf("적");
				}
				//탈출구
				else if (horizen == exitX && vertical == exitY) {
					printf("문");
				}
				//포션
				else if (horizen == potionX && vertical == potionY) {
					printf("♡");
				}
				//장애물
				else if (isTrap) {
					printf("▣");
				}
				//도로 기본 이미지
				else if (currMapField == MAP_FIELD_ROAD) {
					printf("■");
				}
				//강 기본 이미지
				else if (currMapField == MAP_FIELD_RIVER) {
					printf("〓");
				}
				//산 기본 이미지
				else if (currMapField == MAP_FIELD_MOUNTAIN) {
					printf("▲");
				}
			}	//[main-while-for(horizen)] end
			printf("\n");

		}	//[main-while-for(vertical)] end


		//각종 정보 출력
		printStatus(action, playerHP, score, difficult, potionCount);

		printf("\t이동:방향키 포션:h 종료:q");
		printf("\n");

		//각종 이벤트 알람 출력
		if (isEdgeEvent) {
			printf("\t순간 이동. -%d\n", EDGE_DAMAGE);
			isEdgeEvent = false;
		}

		if (isTrapEvent) {
			printf("\t장애물을 밟았다. -%d\n", TRAP_DAMAGE);
			isTrapEvent = false;
		}

		if (isBattleEvent) {
			printf("\t전투 발생. -%d\n", playerDamageSum);
			playerDamageSum = 0;
			isBattleEvent = false;
		}
		if (isExitEvent) {
			printf("\t탈출 성공. +%d\n", SCORE_EXIT);
			printEnterField(currMapField, FieldSelector);
			isExitEvent = false;
		}
		if (isPotionEvent) {
			printf("\t포션 획득. \n");
			isPotionEvent = false;
		}
		if (isPotionUseEvent) {
			printf("\t포션 사용. [체력 +%d]\n",HEAL_POTION);
			isPotionUseEvent = false;
		}
		if (isPotionEmptyEvent) {
			printf("\t포션이 없습니다. \n");
			isPotionEmptyEvent = false;
		}

		//죽을 경우 종료
		if (isDeadEvent) {
			printf("당신은 죽었습니다.\n");
			break;
		}

		//[main-while-이벤트 처리] end

		//클릭시 바로 이동
		action = _getch();

		switch (action) {
		case 224:
			//방향키 입력이 아닐경우엔 처리하지않는다.
			action = _getch();
			if (!(action == UP || action == DOWN || action == LEFT || action == RIGHT)) {
				break;
			}

			//이동처리
			currX += moveX(action);
			currY += moveY(action);

			//가장자리 이동
			if (currX == -1 || currX == MAP_SIZE_X || currY == -1 || currY == MAP_SIZE_Y) {
				isEdgeEvent = true;
				playerHP -= EDGE_DAMAGE;
				currX += edgeMove(currX, MAP_SIZE_X);
				currY += edgeMove(currY, MAP_SIZE_Y);
			}
			//[main-while-switch-가장자리if] end


			//전투(선공)
			if (currX == currMonsterX && currY == currMonsterY) {
				isBattleEvent = true;
				printf("\t!!!!!!!!!!!!!!!!! [기습 성공] !!!!!!!!!!!!!!!!!!!\n");
				printf("\t!!!!!!!!!!!!!!! [플레이어 선공] !!!!!!!!!!!!!!!!!\n");
				Sleep(1000);

				float monsterAttck, playerAttck;
				playerDamageSum = 0;

				//매 턴마다 공격을 주고 받는다.
				//플레이어->몬스터
				while (true) {
					system("cls");
					printf("\n\n");
					printf("플레이어 : [체력 %d] [공격력 0~%d]\t", playerHP, playerSTR * 2);
					printf("몬스터 : [체력 %d] [공격력 0~%d]\n\n", monsterHP, monsterSTR * 2);
					Sleep(1000);

					//실제 데미지 계산
					monsterAttck = monsterSTR * ((rand() % 20)) * 0.1f;
					playerAttck = playerSTR * ((rand() % 20)) * 0.1f;

					printf("\n\n");

					//플레이어 공격
					monsterHP -= (int)playerAttck;
					printf("\t\t플레이어의 공격! [%d]\n", (int)playerAttck);
					printf("\t\t\t몬스터 남은 체력 [%d]\n", monsterHP>=0? monsterHP:0);
					Sleep(1000);

					//몬스터 체력 0되면 끝
					if (monsterHP <= 0) {
						printf("\n\t\t\t\t [전투 승리 +%d] \n", SCORE_BATTLE);
						score += SCORE_BATTLE;
						Sleep(1200);
						break;
					}
					printf("\n");

					//몬스터 공격
					playerHP -= (int)monsterAttck;
					playerDamageSum += (int)monsterAttck;
					printf("\t\t몬스터의 공격! [%d]\n", (int)monsterAttck);
					printf("\t\t\t플레이어 남은 체력 [%d]\n", playerHP >= 0 ? playerHP : 0);
					Sleep(1500);

					//플레이어 체력 0되면 끝
					if (playerHP <= 0) {
						printf("\n\t\t\t\t [전투 패배] \n");
						Sleep(1000);
						break;
					}

				}	//[main-while-switch-전투선공if-while] end
			}	//[main-while-switch-전투선공if] end

			//몬스터는 산 필드에서만 나옴, 앞에서 전투로 몬스터가 안죽었을 경우
			//몬스터는 무조건 2칸씩 이동
			//같은 x,y축일경우 같지않은 x,y축을 이동한다.
			if (currMapField == MAP_FIELD_MOUNTAIN && monsterHP>0) {
				//몬스터 X축 이동처리
				if ((currX - currMonsterX) < 0) {
					currMonsterX -= 1;
				}
				else if ((currX - currMonsterX) > 0) {
					currMonsterX += 1;
				}
				//같은 x축일때 처리
				else {
					if ((currY - currMonsterY) < 0) {
						currMonsterY -= 1;
					}
					else if ((currY - currMonsterY) > 0) {
						currMonsterY += 1;
					}
				}

				//몬스터 Y축 이동처리
				if ((currY - currMonsterY) < 0) {
					currMonsterY -= 1;
				}
				else if ((currY - currMonsterY) > 0) {
					currMonsterY += 1;
				}
				//같은 y축일때 처리
				else {
					if ((currX - currMonsterX) < 0) {
						currMonsterX -= 1;
					}
					else if ((currX - currMonsterX) > 0) {
						currMonsterX += 1;
					}
				}
			}	//[main-while-switch-몬스터이동if] end


			//전투(후공)
			if (!isBattleEvent && currX == currMonsterX && currY == currMonsterY) {
				isBattleEvent = true;
				printf("\t!!!!!!!!!!!!!!!!! [전투 발생] !!!!!!!!!!!!!!!!!!!\n");
				printf("\t!!!!!!!!!!!!!!! [플레이어 후공] !!!!!!!!!!!!!!!!!\n");
				Sleep(1000);

				float monsterAttck, playerAttck;
				playerDamageSum = 0;

				//전투실행 몬스터->플레이어
				while (true) {
					system("cls");
					printf("\n\n");
					printf("플레이어 : [체력 %d] [공격력 0~%d]\t", playerHP, playerSTR * 2);
					printf("몬스터 : [체력 %d] [공격력 0~%d]\n\n", monsterHP, monsterSTR * 2);

					Sleep(1000);

					
					//매턴 데미지 계산
					monsterAttck = monsterSTR * ((rand() % 20)) * 0.1f;
					playerAttck = playerSTR * ((rand() % 20)) * 0.1f;

					//몬스터 공격
					playerHP -= (int)monsterAttck;
					playerDamageSum += (int)monsterAttck;
					printf("\t\t몬스터의 공격! [%d]\n", (int)monsterAttck);
					printf("\t\t\t플레이어 남은 체력 [%d]\n", playerHP >= 0 ? playerHP : 0);
					Sleep(1000);

					//전투 패배
					if (playerHP <= 0) {
						printf("\n\t\t\t\t [전투 패배] \n");
						Sleep(1200);
						break;
					}
					printf("\n");

					//플레이어 공격
					monsterHP -= (int)playerAttck;
					printf("\t\t플레이어의 공격! [%d]\n", (int)playerAttck);
					printf("\t\t\t몬스터 남은 체력 [%d]\n", monsterHP >= 0 ? monsterHP : 0);
					Sleep(1500);

					//전투 승리
					if (monsterHP <= 0) {
						printf("\n\t\t\t\t [전투 승리 +%d] \n", SCORE_BATTLE);
						score += SCORE_BATTLE;
						Sleep(1000);
						break;
					}
				}	//[main-while-switch-전투후공if-while] end
			}	//[main-while-switch-전투후공if] end


			//죽었으면 이후 명령문 실행안함
			if (playerHP <= 0) {
				continue;
			}

			//출구도착
			if (currX == exitX && currY == exitY) {
				isExitEvent = true;

				//점수 올라감
				score += SCORE_EXIT;

				//난이도 올라감
				difficult += 1;
				currfieldDamage = FIELD_DAMAGE * (diffCalc(difficult, DIFFICULT_RANK_FIELD) + 1);

				//탈출시 플레이어 회복
				playerHP += HEAL_EXIT;
				if (playerHP > PLAYER_HP_MAX) {
					playerHP = PLAYER_HP_MAX;
				}

				//출구위치, 플레이어 위치 재설정
				exitX = rand() % MAP_SIZE_X;
				exitY = rand() % MAP_SIZE_Y;
				currX = rand() % MAP_SIZE_X;
				currY = rand() % MAP_SIZE_Y;

				//출구-플레이어 겹칠경우 플레이어 위치 재설정
				while ((currX == exitX) && (currY == exitY)) {
					currX = rand() % MAP_SIZE_X;
					currY = rand() % MAP_SIZE_Y;
				}

				//장애물, 적, 포션 초기화
				offset = -1;
				pattern = 0;
				currMonsterX = -100;
				currMonsterY = -100;
				potionX = -100;
				potionY = -100;

				//필드 이동 확률 계산(길20% 강30% 산50%)
				FieldSelector = (rand() % 10001);

				//필드 : 길
				if (FieldSelector <= 2000) {
					currMapField = MAP_FIELD_ROAD;

					//길일 경우 포션 생성
					potionX = rand() % MAP_SIZE_X;
					potionY = rand() % MAP_SIZE_Y;

					//포션-입구 겹칠 경우 재생성
					while ((potionX == exitX) && (potionY == exitY)) {
						potionX = rand() % MAP_SIZE_X;
						potionY = rand() % MAP_SIZE_Y;
					}
				}	//[main-while-switch-출구도착if-길if] end

				//필드 : 강
				else if (FieldSelector > 2000 && FieldSelector <= 5000) {
					currMapField = MAP_FIELD_RIVER;

					//필드가 강일 경우 장애물 생성
					int trapSum = 0;

					//2의 N승씩 증가하는 변수
					int binaryCount = 1;//1,2,4,8,16

					//n번씩 밀어줄 패턴
					offset = (rand() % 12)+4;
					for (int patternCursor = 0; patternCursor < MAP_SIZE_X; patternCursor++) {

						//남은 빈칸 == 남은 장애물칸 일시 다 장애물로 채움 
						if (MAP_SIZE_X - trapSum == MAP_SIZE_X - pattern) {
							trapSum++;
							pattern += binaryCount;
							binaryCount *= 2;
							continue;
						}

						//장애물 전부 생성시 종료
						if (trapSum >= TRAP_COUNT_MAX) {
							break;
						}

						//짝수/홀수 구분해서 홀수일 경우 장애물 생성
						int tmp = rand() % 2;
						if (tmp == 1) {
							trapSum++;
							pattern += binaryCount;
						}

						//2배수씩 증가한다.
						binaryCount *= 2;
					}
				}	//[main-while-switch-출구도착if-강elif] end

				//필드 : 산
				else if (FieldSelector > 5000 && FieldSelector <= 10000) {
					currMapField = MAP_FIELD_MOUNTAIN;

					//필드가 산일경우 몬스터 생성
					currMonsterX = rand() % MAP_SIZE_X;
					currMonsterY = rand() % MAP_SIZE_Y;
					monsterHP = MONSTER_HP_DEFAULT * (diffCalc(difficult, DIFFICULT_RANK_BATTLE)+1);
					monsterSTR = MONSTER_STR_DEFAULT * (diffCalc(difficult, DIFFICULT_RANK_BATTLE)+1);

					//플레이어-몬스터 겹칠경우 몬스터 위치 재설정
					while ((currMonsterX == currX) && (currMonsterY == currY)) {
						currMonsterX = rand() % MAP_SIZE_X;
						currMonsterY = rand() % MAP_SIZE_Y;
					}
				}	//[main-while-switch-출구도착if-산elif] end

			}	//[main-while-switch-출구도착if] end


			//포션 획득시 
			if ((potionX == currX) && (potionY == currY)) {
				potionCount += 1;
				if (potionCount > POTION_COUNT_MAX) {
					potionCount = POTION_COUNT_MAX;
				}

				potionX = -100;
				potionY = -100;
				isPotionEvent = true;
			}

			//움직이면 1점씩 오름
			score += SCORE_WALK;

			//이동후 체력처리
			//다른 이벤트 발생시 처리 안함
			if (!isBattleEvent && !isExitEvent && !isEdgeEvent && !isTrapEvent && !isPotionEvent) {
				playerHP -= currfieldDamage;
			}
			break;

			//포션사용
		case KEY_HEAL:
		case KEY_HEAL -32:
			action = 0;

			//포션 소지시 사용가능
			if (potionCount > 0) {
				playerHP += HEAL_POTION;
				if (playerHP > PLAYER_HP_MAX) {
					playerHP = PLAYER_HP_MAX;
				}
				potionCount--;
				isPotionUseEvent = true;
				break;
			} 

			isPotionEmptyEvent = true;
			break;

			//종료
		case KEY_QUIT:
		case KEY_QUIT - 32:
			printf("종료");
			return;

			//예외사항
		default:
			action = 0;
			printf("?");
			continue;
		}	//[main-while-switch] end


	}	//[main-while] end

	_getch();

}	//[main] end

/****************** 메인 종료 *********************/



/****************** 함수 정의 시작 *********************/

//x축 이동
int moveX(int movePosition) {
	//오른쪽 방향키
	if (movePosition == RIGHT) {
		return 1;
	}
	//왼쪽 방향키
	else if (movePosition == LEFT) {
		return -1;
	}
	//그외
	return 0;
}

//y축 이동
int moveY(int movePosition) {
	//위 방향키
	if (movePosition == UP) {
		return 1;
	}
	//아래 방향키
	else if (movePosition == DOWN) {
		return -1;
	}
	//그외
	return 0;
}

//가장 자리 이동처리
int edgeMove(int currAxis, int size) {
	if (currAxis == -1) {
		return size;
	}
	else if (currAxis == size) {
		return -size;
	}
	return 0;
}

int diffCalc(int difficult, float count) {
	return (int)(difficult * count);
}

//기본 정보 print
void printStatus(int movePosition, int HP, int score, int difficult, int potion) {
	printf("\n");
	printf("\t\t");
	//위 방향키
	if (movePosition == UP) {
		printf("[↑]");
	}
	//아래 방향키
	else if (movePosition == DOWN) {
		printf("[↓]");
	}
	else if (movePosition == LEFT) {
		printf("[←]");
	}
	//아래 방향키
	else if (movePosition == RIGHT) {
		printf("[→]");
	}
	else {
		printf("[　]");
	}

	printf(" [HP %3d] ", HP);
	printf(" [포션 %1d] ", potion);
	printf(" [점수 %4d] ", score);
	printf("\t");
	printf(" [난이도 %3d]", difficult);
	printf("\n");
}

void printEnterField(int field, int percent) {
	printf("\t");
	if (field == MAP_FIELD_ROAD) {
		printf("길에 입장(%.2f)", percent * 0.01f);
	}
	else if (field == MAP_FIELD_RIVER) {
		printf("강에 입장(%.2f)", percent * 0.01f);
	}
	else if (field == MAP_FIELD_MOUNTAIN) {
		printf("산에 입장(%.2f)", percent * 0.01f);
	}
	printf("\n");
}


void printField(int field) {
	printf("\t\t");
	if (field == MAP_FIELD_ROAD) {
		printf("[ 길 ] ");
		printf("[ 입장시 맵에 포션 생성. 포션 힐량 +%d]", HEAL_POTION);
	}
	else if (field == MAP_FIELD_RIVER) {
		printf("[ 강 ] ");
		printf("[ 맵에 장애물 생성. 장애물 데미지 -%d ]", TRAP_DAMAGE);
	}
	else if (field == MAP_FIELD_MOUNTAIN) {
		printf("[ 산 ] ");
		printf("[ 입장시 맵 어딘가에 적 생성. 2칸씩 이동. 난이도 높아질수록 강해짐 ]");
	}
	printf("\n");
}
/****************** 함수 정의 종료 *********************/
