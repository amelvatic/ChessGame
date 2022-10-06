using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Pieces
{
    internal class Piece
    {
        private int _team; // 0 = white, 1 = black
        private string _name;

        public Piece(int team, string name)
        {
            _team = team;

            if(_team == 0)
            {
                _name = "W-";
            }
            else if (_team == 1)
            {
                _name = "B-";
            }
            else
            {
                throw new ArgumentException("The team must be 0 or 1!");
            }

            _name += name;
        }

        public void Move()
        {

        }

        public string getName()
        {
            return _name;
        }

        public int getTeam()
        {
            return _team;
        }
    }


}
