using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroServer
{
    public class Step
    {
        public char CurrentPlayer;
        public Position p;
        public Step()
        {
            p = new Position(-1, -1);
            CurrentPlayer = ' ';
        }
        public Step(Position pp, char cc)
        {
            CurrentPlayer = cc;
            p = new Position();
            p.Set(pp);
        }
    }
}
