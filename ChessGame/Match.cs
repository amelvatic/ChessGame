using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ChessGame.Pieces;

namespace ChessGame
{
    internal class Match
    {

        private Piece?[,] starting_pos;
        private Piece?[,] current_pos;
        private int[] moves;
        private bool ended;
        private bool white_to_move;

        private string[] rowNumbers = { "1", "2", "3", "4", "5", "6", "7", "8" };
        private string[] columnNames = { "a", "b", "c", "d", "e", "f", "g", "h" };

        public Match()
        {
            ended = false;
            starting_pos = new Piece[8, 8];

            // create white's pieces
            {
                for (int i = 0; i < 8; i++)
                {
                    Pawn pawn = new Pawn(0);
                    starting_pos[i, 1] = pawn;
                }

                for (int i = 0; i < 2; i++)
                {
                    Rook rook = new Rook(0);
                    starting_pos[0 + i * 7, 0] = rook;
                }

                for (int i = 0; i < 2; i++)
                {
                    Knight knight = new Knight(0);
                    starting_pos[1 + i * 5, 0] = knight;
                }

                for (int i = 0; i < 2; i++)
                {
                    Bishop bishop = new Bishop(0);
                    starting_pos[2 + i * 3, 0] = bishop;
                }

                Queen queen = new Queen(0);
                starting_pos[3, 0] = queen;
                King king = new King(0);
                starting_pos[4, 0] = king;
            }

            // create black's pieces
            {
                for (int i = 0; i < 8; i++)
                {
                    Pawn pawn = new Pawn(1);
                    starting_pos[i, 6] = pawn;
                }

                for (int i = 0; i < 2; i++)
                {
                    Rook rook = new Rook(1);
                    starting_pos[0 + i * 7, 7] = rook;
                }

                for (int i = 0; i < 2; i++)
                {
                    Knight knight = new Knight(1);
                    starting_pos[1 + i * 5, 7] = knight;
                }

                for (int i = 0; i < 2; i++)
                {
                    Bishop bishop = new Bishop(1);
                    starting_pos[2 + i * 3, 7] = bishop;
                }

                Queen queen = new Queen(1);
                starting_pos[3, 7] = queen;
                King king = new King(1);
                starting_pos[4, 7] = king;
            }
        }

        public void startMatch()
        {
            current_pos = starting_pos;
            white_to_move = true;

            string? input;
            bool next_move_executed;
            while (!ended)
            {
                drawBoard();

                if (white_to_move)
                {
                    Console.Write("White to move: ");
                }
                else
                {
                    Console.Write("Black to move: ");
                }

                next_move_executed = false;
                while (!next_move_executed)
                {
                    input = Console.ReadLine();
                    if (input == null)
                    {
                        // weird but okay
                    }
                    else if (input == "q")
                    {
                        Console.WriteLine("Match abandoned");
                        ended = true;
                        next_move_executed = true;
                        break;
                    }
                    else
                    {
                        int response = performMove(input, white_to_move);
                        if (response == 0)
                        {
                            next_move_executed = true;
                            white_to_move = !white_to_move;
                        }
                        else if (response == 1)
                        {
                            Console.WriteLine("-- Cannot perform move: " + input + " --");
                        }
                        else
                        {
                            Console.WriteLine("-- Cannot understand: " + input + " --");
                        }
                    }
                }
                
            }
        }

        private int performMove(string input, bool white_to_move)
        {
            if (input.Length != 5)
            {
                return -1;
            }
            string piecename = input.Substring(0, 1);
            if (white_to_move)
            {
                piecename = "W-" + piecename;
            }
            else
            {
                piecename = "B-" + piecename;
            }

            string pos_from = input.Substring(1, 2);

            int i_from;
            int j_from;
            if (columnNames.Contains(pos_from.Substring(0,1))){
                i_from = Array.IndexOf(columnNames, pos_from.Substring(0, 1));
            }
            else
            {
                return -1;
            }

            if (rowNumbers.Contains(pos_from.Substring(1, 1)))
            {
                j_from = int.Parse(pos_from.Substring(1, 1)) - 1;
            }
            else
            {
                return -1;
            }

            Piece? piece;

            if (current_pos[i_from, j_from] != null && current_pos[i_from, j_from].getName() == piecename)
            {
                piece = current_pos[i_from, j_from];
            }
            else
            {
                return 1;
            }

            string pos_to = input.Substring(3, 2);
            int i_to;
            int j_to;
            if (columnNames.Contains(pos_to.Substring(0, 1)))
            {
                i_to = Array.IndexOf(columnNames, pos_to.Substring(0, 1));
            }
            else
            {
                return -1;
            }

            if (rowNumbers.Contains(pos_to.Substring(1, 1)))
            {
                j_to = int.Parse(pos_to.Substring(1, 1)) - 1;
            }
            else
            {
                return -1;
            }

            // check that there is no other same team piece on that file
            if (current_pos[i_to, j_to] != null && current_pos[i_to, j_to].getTeam() == piece.getTeam())
            {
                return 1;
            }

            // TODO: check that the piece can move that way
            // check that no piece is in the way

            // TODO: check for checks
            // check for pins
            // check for mates and draws

            current_pos[i_from, j_from] = null;
            current_pos[i_to, j_to] = piece;

            return 0;
        }

        private void drawBoard()
        {
            int length_of_board = 52;


            string nextline;
            Console.WriteLine("");

            for (int j = 0; j < 19; j++)
            {
                if(j == 0 || j == 18)
                {
                    nextline = " |";

                    for (int i = 0; i < 8; i++)
                    {
                        nextline += "  ";
                        nextline += columnNames[i];
                        nextline += "  |";
                    }

                    nextline += "   ";
                    Console.WriteLine(nextline);
                    nextline = "";
                }
                else if(j % 2 == 1)
                {
                    nextline = "-";
                    for(int i = 1; i < length_of_board; i++)
                    {
                        nextline += "-";
                    }
                    Console.WriteLine(nextline);
                }
                else
                {
                    int k = (j - 2) / 2;
                    nextline = rowNumbers[7 - k] + "|";

                    for (int i = 0; i < 8; i++)
                    {
                        if (current_pos[i, 7-k] != null)
                        {
                            nextline += " ";
                            nextline += current_pos[i, 7 - k].getName();
                            nextline += " |";
                        }
                        else
                        {
                            nextline += "     |";
                        }
                    }

                    nextline += rowNumbers[7-k];
                    Console.WriteLine(nextline);
                }
                
            }

            Console.WriteLine("");
        }
    }
}
