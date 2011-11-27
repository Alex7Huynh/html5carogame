using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroServer
{
    public class Position
    {
        public int x;
        public int y;
        public Position()
        {
        }
        public void Set(Position p)
        {
            x = p.x; y = p.y;
        }
        public void Set(int a, int b)
        {
            x = a; y = b;
        }
        public Position(int a, int b)
        {
            x = a; y = b;
        }
    }
}
