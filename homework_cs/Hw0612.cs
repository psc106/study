using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_cs
{

    public class TicTacGame
    {
        TicTacPlayer player;
        TicTacPlayer computer;
        TicTacMap map;

        public TicTacGame()
        {
            map = new int[3, 3];
        }

        public void DrawMap(int x, int y, bool isPlayer)
        {

        }

        public void Start()
        {

        }


    }
    public class TicTacMap
    {
        private int[,] map;
        private readonly string[] MAP_CHARACTER = { "　", "Ⅹ", "㉧" };

        public TicTacMap()
        {
            map = new int[3,3];
        }

        public void DrawMap(int x, int y, bool isPlayer)
        {

        }

        public void Start()
        {

        }


    }

    public class TicTacPlayer
    {
        int position;

        public TicTacPlayer()
        {
        }

        public void Move()
        {

        }


    }


}
