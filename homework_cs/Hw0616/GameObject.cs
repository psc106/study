using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs.Hw0616
{
    //오브젝트 담당(부모 클래스)
    public class GameObject
    {
        public int X;
        public int Y;
        public int objectID;
    }

    public abstract class GameMoveObject : GameObject
    {
        public int direction;
        protected int[] AXIS_X = { 0, 0, -1, 0, 1 };
        protected int[] AXIS_Y = { 0, -1, 0, 1, 0 };

        public abstract void Move();

        public void Hold(int size)
        {
            if (X < 0)
            {
                X = 0;
            }
            else if (X >= size)
            {
                X = size - 1;
            }
            else if (Y < 0)
            {
                Y = 0;
            }
            else if (Y >= size)
            {
                Y = size - 1;
            }
        }
    }

    public class GamePlayer : GameMoveObject
    {
        public List<GameItem> inventory { get; private set; }
        public int gold;

        public GamePlayer(int x, int y, int dir)
        {
            this.X = x;
            this.Y = y;
            this.direction = dir;
            this.objectID = 5;
            this.gold = 0;
            this.inventory = new List<GameItem>();
        }

        public override void Move()
        {
            this.X += AXIS_X[this.direction];
            this.Y += AXIS_Y[this.direction];
        }

    }

    public class GameEnemy : GameMoveObject
    {
        public GameEnemy(int x, int y, int dir)
        {
            this.X = x;
            this.Y = y;
            this.direction = dir;
            this.objectID = 5;
        }

        public override void Move()
        {
            this.X += AXIS_X[this.direction];
            this.Y += AXIS_Y[this.direction];
        }

    }

    public class Portal : GameObject
    {
        public Portal(int x, int y, int nextMode)
        {
            this.X = x;
            this.Y = y;
            this.objectID = nextMode;
        }

    }
}
