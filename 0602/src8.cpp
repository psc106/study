#include <iostream>
#include <windows.h>
#include <conio.h>
#include <stdlib.h>

/****************** ��� ���� ���� *********************/
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
/****************** ��� ���� ���� *********************/


/****************** �Լ� ���� ���� *********************/
int moveX(int);//x�� �̵�
int moveY(int);//y�� �̵�
int edgeMove(int, int);//���� �ڸ� �̵�ó��

int diffCalc(int difficult, float count); //���̵� ����� �°� ���

void printStatus(int, int, int, int, int);//�⺻ ���� print
void printEnterField(int, int);			//
void printField(int);						//���� �ʵ� ���� print
/****************** �Լ� ���� ���� *********************/

//���ھ� ����. �����ڸ��� ���� �ݴ������� ����
//�̵��� ü���� ������ ����(���̵� ���������� ü�� ���� ����)
//�ʸ��� ������ �ٸ�
//�� : ���ǵ��  
//�� : ��ֹ� 
//�� : ������
//������ �����߿��� ���԰� ����Ű�� �Է��ؾ� �Ծ���
//��ֹ��� �ึ�� ������ �Ȱ���.
//���� ���� �̵��Ҷ����� ���� �������, ���̵��� ���� ���� ������

/****************** ���� ���� *********************/
void main() {

	/*	
	isStart : ���� ���� ����
	isTrapEvent : ��ֹ� ��Ҵ���
	isEdgeEvent : �����ڸ��� �̵��ߴ���
	isDeadEvent : �׾�����
	isBattleEvent : �����ߴ���
	isExitEvent : Ż���ߴ���
	isPotionEvent : ���� ȹ��
	isPotionUseEvent : ���� ���
	isPotionEmptyEvent : ���� ����
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

	int difficult = 0;	//���̵�
	int score = 0;		//����
	int action = -1;	//���� Ű �� ����
	int playerDamageSum = 0;	//�������� ���� ������
	int potionCount = 0;		//���� ����
	int currfieldDamage = 1;	//���� �ȱ� ������

	//���� ����
	int playerHP = PLAYER_HP_DEFAULT;
	int playerSTR = PLAYER_STR_DEFAULT;
	int monsterHP = MONSTER_HP_DEFAULT;
	int monsterSTR = MONSTER_STR_DEFAULT;

	//��ġ����
	int currX = 0, currY = 0;
	int currMonsterX = -100, currMonsterY = -100;
	int exitX = 6, exitY = 0;
	int potionX = -100, potionY = -100;

	int currMapField = 0;	//���� �� �ʵ� ����
	int FieldSelector = 0;//���� �ʵ� ���� ���ϴ� Ȯ�� �� ����
	unsigned short pattern = 0;	//��ֹ� ����. 2^17��-1 ���� �����ؼ� ��Ʈ�� ��
	unsigned int offset = 0;		//��ֹ� ������ �پ缺�� ����� ����. offset��ŭ ���������� �̵���Ŵ


	srand((unsigned int)time(NULL));

	while (true) {
		//�ֿܼ� �����ִ� ���ڵ� ���ֱ�
		if (!isStart) {
			Sleep(50);
			system("cls");
			printf("\n");
		}

		//�� ���� ���
		printField(currMapField);
		printf("\t");
		printf(" �ȱ�[+%d] ", SCORE_WALK);
		printf(" Ż��[+%d] ", SCORE_EXIT);
		printf(" �����¸�[+%d]\t\t", SCORE_BATTLE);
		printf("�ȱ� ������[-%2d]", currfieldDamage);
		printf("\n");

		//�� �׸���
		//(vertical, horizen)		 
		//(3,0) (3,1) ...
		//(2,0) (2,1) ...
		//(1,0) (1,1) ...
		//(0,0) (0,1) ...
		for (int vertical = MAP_SIZE_Y - 1; vertical >= 0; vertical--) {
			printf("\t\t\t");

			//��Ʈ �˻�� ����
			int PatternChecKer;

			//��ֹ� ������ ���� ��ä�Ӱ� �Ϸ��� offset��ŭ �о���
			//�ʵ尡 ���� ���� ���
			if (currMapField == MAP_FIELD_RIVER) {
				PatternChecKer = 1 << (offset * vertical) % MAP_SIZE_X;
			}

			for (int horizen = 0; horizen < MAP_SIZE_X; horizen++) {
				bool isTrap = false;

				//��Ʈ �������� ��ֹ� �Ǻ�
				// 0001 1101 0000 0011
				// 1000 0000 0000 0000
				// ������ �ϳ��� ���ؼ� ���� ������ ��ֹ���
				//�ʵ尡 ���� ���� ���
				if (currMapField == MAP_FIELD_RIVER) {
					//���� ��ġ�� ��ֹ����� Ȯ��
					isTrap = (pattern & PatternChecKer) == PatternChecKer ? true : false;

					//��Ʈ �˻�� ���� ó��
					PatternChecKer = PatternChecKer >> 1;
					if (PatternChecKer == 0) {
						PatternChecKer = 1 << 16;
					}
				}

				//���� ��ġ���� �´� �ؽ�Ʈ ���

				//�÷��̾�
				if (horizen == currX && vertical == currY) {
					//Ż���� �ٷ� ��°� ������ ó�� ����
					if (isTrap&&!isExitEvent) {
						playerHP -= TRAP_DAMAGE- FIELD_DAMAGE;
						isTrapEvent = true;
					}
					if (playerHP <= 0) {
						printf("��");
						isDeadEvent = true;
					}
					else {
						printf("��");
					}
				}
				//����
				else if (monsterHP > 0 && (horizen == currMonsterX && vertical == currMonsterY)) {
					printf("��");
				}
				//Ż�ⱸ
				else if (horizen == exitX && vertical == exitY) {
					printf("��");
				}
				//����
				else if (horizen == potionX && vertical == potionY) {
					printf("��");
				}
				//��ֹ�
				else if (isTrap) {
					printf("��");
				}
				//���� �⺻ �̹���
				else if (currMapField == MAP_FIELD_ROAD) {
					printf("��");
				}
				//�� �⺻ �̹���
				else if (currMapField == MAP_FIELD_RIVER) {
					printf("��");
				}
				//�� �⺻ �̹���
				else if (currMapField == MAP_FIELD_MOUNTAIN) {
					printf("��");
				}
			}	//[main-while-for(horizen)] end
			printf("\n");

		}	//[main-while-for(vertical)] end


		//���� ���� ���
		printStatus(action, playerHP, score, difficult, potionCount);

		printf("\t�̵�:����Ű ����:h ����:q");
		printf("\n");

		//���� �̺�Ʈ �˶� ���
		if (isEdgeEvent) {
			printf("\t���� �̵�. -%d\n", EDGE_DAMAGE);
			isEdgeEvent = false;
		}

		if (isTrapEvent) {
			printf("\t��ֹ��� ��Ҵ�. -%d\n", TRAP_DAMAGE);
			isTrapEvent = false;
		}

		if (isBattleEvent) {
			printf("\t���� �߻�. -%d\n", playerDamageSum);
			playerDamageSum = 0;
			isBattleEvent = false;
		}
		if (isExitEvent) {
			printf("\tŻ�� ����. +%d\n", SCORE_EXIT);
			printEnterField(currMapField, FieldSelector);
			isExitEvent = false;
		}
		if (isPotionEvent) {
			printf("\t���� ȹ��. \n");
			isPotionEvent = false;
		}
		if (isPotionUseEvent) {
			printf("\t���� ���. [ü�� +%d]\n",HEAL_POTION);
			isPotionUseEvent = false;
		}
		if (isPotionEmptyEvent) {
			printf("\t������ �����ϴ�. \n");
			isPotionEmptyEvent = false;
		}

		//���� ��� ����
		if (isDeadEvent) {
			printf("����� �׾����ϴ�.\n");
			break;
		}

		//[main-while-�̺�Ʈ ó��] end

		//Ŭ���� �ٷ� �̵�
		action = _getch();

		switch (action) {
		case 224:
			//����Ű �Է��� �ƴҰ�쿣 ó�������ʴ´�.
			action = _getch();
			if (!(action == UP || action == DOWN || action == LEFT || action == RIGHT)) {
				break;
			}

			//�̵�ó��
			currX += moveX(action);
			currY += moveY(action);

			//�����ڸ� �̵�
			if (currX == -1 || currX == MAP_SIZE_X || currY == -1 || currY == MAP_SIZE_Y) {
				isEdgeEvent = true;
				playerHP -= EDGE_DAMAGE;
				currX += edgeMove(currX, MAP_SIZE_X);
				currY += edgeMove(currY, MAP_SIZE_Y);
			}
			//[main-while-switch-�����ڸ�if] end


			//����(����)
			if (currX == currMonsterX && currY == currMonsterY) {
				isBattleEvent = true;
				printf("\t!!!!!!!!!!!!!!!!! [��� ����] !!!!!!!!!!!!!!!!!!!\n");
				printf("\t!!!!!!!!!!!!!!! [�÷��̾� ����] !!!!!!!!!!!!!!!!!\n");
				Sleep(1000);

				float monsterAttck, playerAttck;
				playerDamageSum = 0;

				//�� �ϸ��� ������ �ְ� �޴´�.
				//�÷��̾�->����
				while (true) {
					system("cls");
					printf("\n\n");
					printf("�÷��̾� : [ü�� %d] [���ݷ� 0~%d]\t", playerHP, playerSTR * 2);
					printf("���� : [ü�� %d] [���ݷ� 0~%d]\n\n", monsterHP, monsterSTR * 2);
					Sleep(1000);

					//���� ������ ���
					monsterAttck = monsterSTR * ((rand() % 20)) * 0.1f;
					playerAttck = playerSTR * ((rand() % 20)) * 0.1f;

					printf("\n\n");

					//�÷��̾� ����
					monsterHP -= (int)playerAttck;
					printf("\t\t�÷��̾��� ����! [%d]\n", (int)playerAttck);
					printf("\t\t\t���� ���� ü�� [%d]\n", monsterHP>=0? monsterHP:0);
					Sleep(1000);

					//���� ü�� 0�Ǹ� ��
					if (monsterHP <= 0) {
						printf("\n\t\t\t\t [���� �¸� +%d] \n", SCORE_BATTLE);
						score += SCORE_BATTLE;
						Sleep(1200);
						break;
					}
					printf("\n");

					//���� ����
					playerHP -= (int)monsterAttck;
					playerDamageSum += (int)monsterAttck;
					printf("\t\t������ ����! [%d]\n", (int)monsterAttck);
					printf("\t\t\t�÷��̾� ���� ü�� [%d]\n", playerHP >= 0 ? playerHP : 0);
					Sleep(1500);

					//�÷��̾� ü�� 0�Ǹ� ��
					if (playerHP <= 0) {
						printf("\n\t\t\t\t [���� �й�] \n");
						Sleep(1000);
						break;
					}

				}	//[main-while-switch-��������if-while] end
			}	//[main-while-switch-��������if] end

			//���ʹ� �� �ʵ忡���� ����, �տ��� ������ ���Ͱ� ���׾��� ���
			//���ʹ� ������ 2ĭ�� �̵�
			//���� x,y���ϰ�� �������� x,y���� �̵��Ѵ�.
			if (currMapField == MAP_FIELD_MOUNTAIN && monsterHP>0) {
				//���� X�� �̵�ó��
				if ((currX - currMonsterX) < 0) {
					currMonsterX -= 1;
				}
				else if ((currX - currMonsterX) > 0) {
					currMonsterX += 1;
				}
				//���� x���϶� ó��
				else {
					if ((currY - currMonsterY) < 0) {
						currMonsterY -= 1;
					}
					else if ((currY - currMonsterY) > 0) {
						currMonsterY += 1;
					}
				}

				//���� Y�� �̵�ó��
				if ((currY - currMonsterY) < 0) {
					currMonsterY -= 1;
				}
				else if ((currY - currMonsterY) > 0) {
					currMonsterY += 1;
				}
				//���� y���϶� ó��
				else {
					if ((currX - currMonsterX) < 0) {
						currMonsterX -= 1;
					}
					else if ((currX - currMonsterX) > 0) {
						currMonsterX += 1;
					}
				}
			}	//[main-while-switch-�����̵�if] end


			//����(�İ�)
			if (!isBattleEvent && currX == currMonsterX && currY == currMonsterY) {
				isBattleEvent = true;
				printf("\t!!!!!!!!!!!!!!!!! [���� �߻�] !!!!!!!!!!!!!!!!!!!\n");
				printf("\t!!!!!!!!!!!!!!! [�÷��̾� �İ�] !!!!!!!!!!!!!!!!!\n");
				Sleep(1000);

				float monsterAttck, playerAttck;
				playerDamageSum = 0;

				//�������� ����->�÷��̾�
				while (true) {
					system("cls");
					printf("\n\n");
					printf("�÷��̾� : [ü�� %d] [���ݷ� 0~%d]\t", playerHP, playerSTR * 2);
					printf("���� : [ü�� %d] [���ݷ� 0~%d]\n\n", monsterHP, monsterSTR * 2);

					Sleep(1000);

					
					//���� ������ ���
					monsterAttck = monsterSTR * ((rand() % 20)) * 0.1f;
					playerAttck = playerSTR * ((rand() % 20)) * 0.1f;

					//���� ����
					playerHP -= (int)monsterAttck;
					playerDamageSum += (int)monsterAttck;
					printf("\t\t������ ����! [%d]\n", (int)monsterAttck);
					printf("\t\t\t�÷��̾� ���� ü�� [%d]\n", playerHP >= 0 ? playerHP : 0);
					Sleep(1000);

					//���� �й�
					if (playerHP <= 0) {
						printf("\n\t\t\t\t [���� �й�] \n");
						Sleep(1200);
						break;
					}
					printf("\n");

					//�÷��̾� ����
					monsterHP -= (int)playerAttck;
					printf("\t\t�÷��̾��� ����! [%d]\n", (int)playerAttck);
					printf("\t\t\t���� ���� ü�� [%d]\n", monsterHP >= 0 ? monsterHP : 0);
					Sleep(1500);

					//���� �¸�
					if (monsterHP <= 0) {
						printf("\n\t\t\t\t [���� �¸� +%d] \n", SCORE_BATTLE);
						score += SCORE_BATTLE;
						Sleep(1000);
						break;
					}
				}	//[main-while-switch-�����İ�if-while] end
			}	//[main-while-switch-�����İ�if] end


			//�׾����� ���� ��ɹ� �������
			if (playerHP <= 0) {
				continue;
			}

			//�ⱸ����
			if (currX == exitX && currY == exitY) {
				isExitEvent = true;

				//���� �ö�
				score += SCORE_EXIT;

				//���̵� �ö�
				difficult += 1;
				currfieldDamage = FIELD_DAMAGE * (diffCalc(difficult, DIFFICULT_RANK_FIELD) + 1);

				//Ż��� �÷��̾� ȸ��
				playerHP += HEAL_EXIT;
				if (playerHP > PLAYER_HP_MAX) {
					playerHP = PLAYER_HP_MAX;
				}

				//�ⱸ��ġ, �÷��̾� ��ġ �缳��
				exitX = rand() % MAP_SIZE_X;
				exitY = rand() % MAP_SIZE_Y;
				currX = rand() % MAP_SIZE_X;
				currY = rand() % MAP_SIZE_Y;

				//�ⱸ-�÷��̾� ��ĥ��� �÷��̾� ��ġ �缳��
				while ((currX == exitX) && (currY == exitY)) {
					currX = rand() % MAP_SIZE_X;
					currY = rand() % MAP_SIZE_Y;
				}

				//��ֹ�, ��, ���� �ʱ�ȭ
				offset = -1;
				pattern = 0;
				currMonsterX = -100;
				currMonsterY = -100;
				potionX = -100;
				potionY = -100;

				//�ʵ� �̵� Ȯ�� ���(��20% ��30% ��50%)
				FieldSelector = (rand() % 10001);

				//�ʵ� : ��
				if (FieldSelector <= 2000) {
					currMapField = MAP_FIELD_ROAD;

					//���� ��� ���� ����
					potionX = rand() % MAP_SIZE_X;
					potionY = rand() % MAP_SIZE_Y;

					//����-�Ա� ��ĥ ��� �����
					while ((potionX == exitX) && (potionY == exitY)) {
						potionX = rand() % MAP_SIZE_X;
						potionY = rand() % MAP_SIZE_Y;
					}
				}	//[main-while-switch-�ⱸ����if-��if] end

				//�ʵ� : ��
				else if (FieldSelector > 2000 && FieldSelector <= 5000) {
					currMapField = MAP_FIELD_RIVER;

					//�ʵ尡 ���� ��� ��ֹ� ����
					int trapSum = 0;

					//2�� N�¾� �����ϴ� ����
					int binaryCount = 1;//1,2,4,8,16

					//n���� �о��� ����
					offset = (rand() % 12)+4;
					for (int patternCursor = 0; patternCursor < MAP_SIZE_X; patternCursor++) {

						//���� ��ĭ == ���� ��ֹ�ĭ �Ͻ� �� ��ֹ��� ä�� 
						if (MAP_SIZE_X - trapSum == MAP_SIZE_X - pattern) {
							trapSum++;
							pattern += binaryCount;
							binaryCount *= 2;
							continue;
						}

						//��ֹ� ���� ������ ����
						if (trapSum >= TRAP_COUNT_MAX) {
							break;
						}

						//¦��/Ȧ�� �����ؼ� Ȧ���� ��� ��ֹ� ����
						int tmp = rand() % 2;
						if (tmp == 1) {
							trapSum++;
							pattern += binaryCount;
						}

						//2����� �����Ѵ�.
						binaryCount *= 2;
					}
				}	//[main-while-switch-�ⱸ����if-��elif] end

				//�ʵ� : ��
				else if (FieldSelector > 5000 && FieldSelector <= 10000) {
					currMapField = MAP_FIELD_MOUNTAIN;

					//�ʵ尡 ���ϰ�� ���� ����
					currMonsterX = rand() % MAP_SIZE_X;
					currMonsterY = rand() % MAP_SIZE_Y;
					monsterHP = MONSTER_HP_DEFAULT * (diffCalc(difficult, DIFFICULT_RANK_BATTLE)+1);
					monsterSTR = MONSTER_STR_DEFAULT * (diffCalc(difficult, DIFFICULT_RANK_BATTLE)+1);

					//�÷��̾�-���� ��ĥ��� ���� ��ġ �缳��
					while ((currMonsterX == currX) && (currMonsterY == currY)) {
						currMonsterX = rand() % MAP_SIZE_X;
						currMonsterY = rand() % MAP_SIZE_Y;
					}
				}	//[main-while-switch-�ⱸ����if-��elif] end

			}	//[main-while-switch-�ⱸ����if] end


			//���� ȹ��� 
			if ((potionX == currX) && (potionY == currY)) {
				potionCount += 1;
				if (potionCount > POTION_COUNT_MAX) {
					potionCount = POTION_COUNT_MAX;
				}

				potionX = -100;
				potionY = -100;
				isPotionEvent = true;
			}

			//�����̸� 1���� ����
			score += SCORE_WALK;

			//�̵��� ü��ó��
			//�ٸ� �̺�Ʈ �߻��� ó�� ����
			if (!isBattleEvent && !isExitEvent && !isEdgeEvent && !isTrapEvent && !isPotionEvent) {
				playerHP -= currfieldDamage;
			}
			break;

			//���ǻ��
		case KEY_HEAL:
		case KEY_HEAL -32:
			action = 0;

			//���� ������ ��밡��
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

			//����
		case KEY_QUIT:
		case KEY_QUIT - 32:
			printf("����");
			return;

			//���ܻ���
		default:
			action = 0;
			printf("?");
			continue;
		}	//[main-while-switch] end


	}	//[main-while] end

	_getch();

}	//[main] end

/****************** ���� ���� *********************/



/****************** �Լ� ���� ���� *********************/

//x�� �̵�
int moveX(int movePosition) {
	//������ ����Ű
	if (movePosition == RIGHT) {
		return 1;
	}
	//���� ����Ű
	else if (movePosition == LEFT) {
		return -1;
	}
	//�׿�
	return 0;
}

//y�� �̵�
int moveY(int movePosition) {
	//�� ����Ű
	if (movePosition == UP) {
		return 1;
	}
	//�Ʒ� ����Ű
	else if (movePosition == DOWN) {
		return -1;
	}
	//�׿�
	return 0;
}

//���� �ڸ� �̵�ó��
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

//�⺻ ���� print
void printStatus(int movePosition, int HP, int score, int difficult, int potion) {
	printf("\n");
	printf("\t\t");
	//�� ����Ű
	if (movePosition == UP) {
		printf("[��]");
	}
	//�Ʒ� ����Ű
	else if (movePosition == DOWN) {
		printf("[��]");
	}
	else if (movePosition == LEFT) {
		printf("[��]");
	}
	//�Ʒ� ����Ű
	else if (movePosition == RIGHT) {
		printf("[��]");
	}
	else {
		printf("[��]");
	}

	printf(" [HP %3d] ", HP);
	printf(" [���� %1d] ", potion);
	printf(" [���� %4d] ", score);
	printf("\t");
	printf(" [���̵� %3d]", difficult);
	printf("\n");
}

void printEnterField(int field, int percent) {
	printf("\t");
	if (field == MAP_FIELD_ROAD) {
		printf("�濡 ����(%.2f)", percent * 0.01f);
	}
	else if (field == MAP_FIELD_RIVER) {
		printf("���� ����(%.2f)", percent * 0.01f);
	}
	else if (field == MAP_FIELD_MOUNTAIN) {
		printf("�꿡 ����(%.2f)", percent * 0.01f);
	}
	printf("\n");
}


void printField(int field) {
	printf("\t\t");
	if (field == MAP_FIELD_ROAD) {
		printf("[ �� ] ");
		printf("[ ����� �ʿ� ���� ����. ���� ���� +%d]", HEAL_POTION);
	}
	else if (field == MAP_FIELD_RIVER) {
		printf("[ �� ] ");
		printf("[ �ʿ� ��ֹ� ����. ��ֹ� ������ -%d ]", TRAP_DAMAGE);
	}
	else if (field == MAP_FIELD_MOUNTAIN) {
		printf("[ �� ] ");
		printf("[ ����� �� ��򰡿� �� ����. 2ĭ�� �̵�. ���̵� ���������� ������ ]");
	}
	printf("\n");
}
/****************** �Լ� ���� ���� *********************/
