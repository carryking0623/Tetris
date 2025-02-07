using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x; 
            this.y = y;
        }
        public static bool operator == (Position p1, Position p2) => p1.x == p2.x && p1.y == p2.y;
        public static bool operator !=(Position p1, Position p2) => p1.x != p2.x || p1.y != p2.y;
        public static Position operator+(Position p1, Position p2)=> new Position(p1.x + p2.x, p1.y + p2.y);


    }
}
