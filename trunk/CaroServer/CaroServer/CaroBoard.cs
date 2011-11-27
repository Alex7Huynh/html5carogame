using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaroServer
{
    public class CaroBoard
    {
        #region Attribute
        public char[,] cells;
        public bool XPlaying;
        public char CurrentPlayer
        {
            get
            {
                return XPlaying ? 'x' : 'o';
            }
        }
        public int size { get; private set; }
        private int[] dx = { 0, 1, -1, 1, 0, -1, 1, -1 };
        private int[] dy = { 1, 0, 1, 1, -1, 0, -1, -1 };
        public Position PrevMove = new Position(-1, -1);
        public Position CurrMove = new Position(-1, -1);
        private bool zingLaw;
        #endregion

        #region Init
        public CaroBoard()
            : this(14, false)
        {
        }
        public CaroBoard(int n, bool zingLaw)
        {
            this.zingLaw = zingLaw;
            New(n);
        }
        private void New(int n)
        {
            XPlaying = true;
            size = n;
            cells = new char[size, size];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    cells[i, j] = ' ';
        }
        #endregion

        /// <summary>
        /// Check valid position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool CheckPosition(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < size && y < size);
        }

        /// <summary>
        /// Check for game over
        /// Usage:
        /// if (IsGameOver[0]>=5)
        ///     game is over
        /// else
        ///     game is not over
        /// </summary>
        public int[] IsGame0ver
        {
            get
            {
                char CurrPlayer = XPlaying ? 'x' : 'o';
                int[] win = new int[5];
                win[0] = 1;
                for (int i = 0; i < 4; i++)
                {
                    win = Check5Row(CurrMove.x, CurrMove.y, i, CurrPlayer);
                    if (win[0] >= 5) return win;
                }
                return win;
            }
        }

        public int[] Check5Row(int x, int y, int type, char CurrPlayer)
        {
            bool next = true, prev = true;
            bool tag = false, tail = false;
            //int count = 1;
            int u, v;
            int[] win = new int[5];
            win[0] = 1;
            win[1] = win[3] = x;
            win[2] = win[4] = y;
            for (int i = 1; i < 5; i++)
            {
                u = x + i * dx[type];
                v = y + i * dy[type];
                if (CheckPosition(u, v) && cells[u, v] == CurrPlayer && next)
                {
                    win[0]++;
                    win[1] = u;
                    win[2] = v;
                }
                else
                {
                    if (CheckPosition(u, v) && cells[u, v] != CurrPlayer && cells[u, v] != ' ')
                        tag = true;
                    next = false;
                }
                u = x + i * dx[type + 4];
                v = y + i * dy[type + 4];
                if (CheckPosition(u, v) && cells[u, v] == CurrPlayer && prev)
                {
                    win[0]++;
                    win[3] = u;
                    win[4] = v;
                }
                else
                {
                    prev = false;
                    if (CheckPosition(u, v) && cells[u, v] != CurrPlayer && cells[u, v] != ' ')
                        tail = true;
                }
            }
            if (tail && tag && zingLaw && win[0] == 5)
            {
                win[0] = 0;
            }

            return win;
        }
    }
}
