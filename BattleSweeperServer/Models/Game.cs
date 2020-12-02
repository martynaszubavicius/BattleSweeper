using BattleSweeperServer.DesignPatternClasses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BattleSweeperServer.Models
{
    public class Game
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Key")]
        public string Key { get { return this.Id.ToString(); } } // TODO: use this instead of id in requests

        [JsonIgnore] //[JsonProperty("State")]
        public GameState State { get; set; }

        [JsonProperty("Player1")]
        public Player Player1 { get; set; }

        [JsonProperty("Player2")]
        public Player Player2 { get; set; }

        //------------------------------------------------------------------------------
        //TODO: My part 
        private List<Memento> MementoStateList = new List<Memento>();


        //----------------------------------------------------------------------------
        [JsonProperty("Settings")]
        public GameSettings Settings { get; set; }

        [JsonIgnore]
        public Observer HistoryObserver { get; set; }
        
        // only for use by the client and json serialising. Server should not use this anywhere else
        [JsonProperty("RedrawPoints")]
        public List<ClientChangePoint> RedrawPoints { get; set; } // TODO: THIS IS DIRTY AND UGLY, FIX IT PLS

        [JsonProperty("HistoryLastIndex")]
        public int HistoryLastIndex { get; set; }

        public Game()
        {
            //this.History = new List<Command>();
            this.State = new GameStateNew();

        }

        // Returns a sanitized Game object that only has information that the player with specified identifier should have
        public Game GetPlayerView(string playerIdentifier, int historyStartIndex = -1)
        {
            if (!HasPlayerWithIdentifier(playerIdentifier))
                return null; // not your game, scrub

            Game playerView = new Game 
            { 
                Id = this.Id,
                Settings = this.Settings
            };
            
            playerView.Player1 = Player1.Identifier == playerIdentifier ? Player1 : Player2;
            if (Player2 != null)
                playerView.Player2 = Player1.Identifier == playerIdentifier ? Player2.GetEnemyView() : Player1.GetEnemyView();
            if (historyStartIndex >= 0)
                playerView.RedrawPoints = State.HistoryObserver.GetPlayerViewCommands(playerIdentifier, historyStartIndex)
                    .Select(x => { if (x != null) return new ClientChangePoint(x.X, x.Y); else return null; })
                    //.Where(x => x != null)
                    .ToList(); // TODO: UGLY UGLY UGLY
            else
                playerView.RedrawPoints = new List<ClientChangePoint>();
            playerView.HistoryLastIndex = State.HistoryObserver.CommandCount;

            return playerView;
        }

        public bool HasPlayerWithIdentifier(string playerIdentifier)
        {
            return ( Player1 != null && Player1.Identifier == playerIdentifier) || (Player2 != null && Player2.Identifier == playerIdentifier);
        }

        public Player GetPlayerByIdentifier(string playerIdentifier)
        {
            if (HasPlayerWithIdentifier(playerIdentifier))
                return Player1.Identifier == playerIdentifier ? Player1 : Player2;
            else
                return null;
        }

        public Player GetEnemyByIdentifier(string playerIdentifier)
        {
            if (HasPlayerWithIdentifier(playerIdentifier))
                return Player1.Identifier == playerIdentifier ? Player2 : Player1;
            else
                return null;
        }

        //TODO: add command given from controller to observer
        public void AddExecuteCommand(Command command)
        {
            State.ExecuteCommand(this, command);
            //command.Execute(this);
            //State.HistoryObserver.Add(command);
        }

        public void UndoLastPlayerCommand(string playerIdentifier)
        {
            State.HistoryObserver.Undo(this, playerIdentifier);
        }
        
        public void AddMemento(Memento memento)
        {
            MementoStateList.Add(memento);
        }

        public void RebuildGame(Player player)
        {
            foreach(Memento m in MementoStateList)
            {
                //check if there is such identifier between participated people
                if (m.NameState() == player.Name)
                {
                    //joining player
                    Player1.SetMemento(m);
                }
                else
                {
                    // fake player
                    Player2 = new Player();
                    Player2.SetMemento(m);
                }
            }
        }

        public string GetTextOutput()
        {
            return State.HistoryObserver.GetTextOutput();
        }
    }
}
