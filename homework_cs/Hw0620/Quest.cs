using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0620
{
    public class Quest
    {
        public int type;
        public string explane;
        public bool isSuccess;

        public Quest() { }
        public Quest(Quest quest)
        {
            Init(quest);
        }
        public Quest(int type, string explane)
        {
            Init(type, explane);
        }

        protected void Init(Quest quest)
        {
            this.isSuccess = false;
            this.type = quest.type;
            this.explane = quest.explane;
        }
        protected void Init(int type, string explane)
        {
            this.isSuccess = false;
            this.type = type;
            this.explane = explane;
        }
    }


    public class KillQuest :Quest
    {
        public int monsterID;
        public int killMax;
        public int killCount;

        public KillQuest()
        {
            type = 1;
        }

        public KillQuest(KillQuest quest)
        {
            base.Init(quest);
            this.Init(quest);
        }
        public KillQuest(int type, string explane, int monsterID, int killCount)
        {
            base.Init(type, explane);
            this.Init(monsterID, killCount);
        }
        private void Init(KillQuest quest)
        {
            this.monsterID = quest.monsterID;
            this.killCount = quest.killCount;
        }
        private void Init(int monsterID, int killCount)
        {
            this.monsterID = monsterID;
            this.killCount = killCount;
        }

    }

    public class ArriveQuest : Quest
    {
        public int arriveX;
        public int arriveY;

        public ArriveQuest()
        {
            type = 2;
        }

        public ArriveQuest(ArriveQuest quest)
        {
            base.Init(quest);
            this.Init(quest);
        }
        public ArriveQuest(int type, string explane, int arriveX, int arriveY)
        {
            base.Init(type, explane);
            this.Init(arriveX, arriveY);
        }
        private void Init(ArriveQuest quest)
        {
            this.arriveX = quest.arriveX;
            this.arriveY = quest.arriveY;
        }
        private void Init(int arriveX, int arriveY)
        {
            this.arriveX = arriveX;
            this.arriveY = arriveY;
        }
    }
}

//퀘스트매니저
//퀘스트 id, 퀘스트 조건

//조건 조각 달성시 퀘스트 매니저로?
//