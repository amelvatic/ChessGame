using System;
using System.Threading;

namespace ChessGame

{
    internal class ChessGame
    {

        static void Main()
        {
            /*
             * Game Loop:
             *  1. Start here
             *  2. Start a match
             *  3. End a match
             *  4. Go to 1.
             */

            string? input;
            while (true)
            {
                Console.WriteLine("To start a match type 'snm' (or  'start new match'), to quit 'q'.");
                input = Console.ReadLine();
                if(input == null)
                {
                    // weird but okay
                }
                else if(input == "snm" || input == "start new match")
                {
                    Match match = new Match();
                    Thread matchThread = new Thread(new ThreadStart(match.startMatch));
                    matchThread.Start();
                    matchThread.Join();
                }
                else if (input == "q")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // nothing
                }
            }


        }
    }
}
