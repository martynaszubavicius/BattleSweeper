using BattleSweeperServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleSweeperServer.DesignPatternClasses
{
    public class Interpreter
    {
        public void ParseMessage(Message message, Game game)
        {
            string[] messageParts = message.Content.Split(' ');

            // 1st cmd: /putMine playerNumber x y type
            // 2nd cmd: /revealTile playerNumber x y

            string cmd = messageParts[0];
            string playerNumber = messageParts[1];
            int x = int.Parse(messageParts[2]);
            int y = int.Parse(messageParts[3]);

            if (playerNumber == "player1")
            {
                if (cmd == "/putMine")
                {
                    MineFactory mineFactory = new MineFactory();
                    game.Player1.Board.Tiles[game.Player1.
                        Board.GetIndex(x, y)].Mine = mineFactory.CreateMine(int.Parse(messageParts[4]));
                }
                else if (messageParts[0] == "/revealTile")
                {
                    game.Player1.Board.RevealTile(x, y);
                }
            }
            else
            { 
                if (cmd == "/putMine")
                {
                    MineFactory mineFactory = new MineFactory();
                    game.Player2.Board.Tiles[game.Player2.
                        Board.GetIndex(x, y)].Mine = mineFactory.CreateMine(int.Parse(messageParts[4]));
                }
                else if (cmd == "/revealTile")
                {
                    game.Player2.Board.RevealTile(x, y);
                }
            }
        }
    }
}
