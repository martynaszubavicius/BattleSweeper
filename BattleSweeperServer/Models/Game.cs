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

        [JsonProperty("Player1")]
        public Player Player1 { get; set; }

        [JsonProperty("Player2")]
        public Player Player2 { get; set; }
        
        [JsonProperty("Settings")]
        public GameSettings Settings { get; set; }

        [JsonIgnore]
        public Observer HistoryObserver { get; set; }
        
        // only for use by the client and json serialising. Server should not use this anywhere else
        [JsonProperty("RedrawPoints")]
        public List<ChangePoint> RedrawPoints { get; set; }

        [JsonProperty("HistoryLastIndex")]
        public int HistoryLastIndex { get; set; }

        public Game()
        {
            //this.History = new List<Command>();
            this.HistoryObserver = new Observer();
        }

        // Returns a sanitized Game object that only has information that the player with specified identifier should have
        public Game GetPlayerView(string playerIdentifier, int historyStartIndex = -1)
        {
            if (!HasPlayerWithIdentifier(playerIdentifier))
                return null; // not your game, scrub

            Game playerView = new Game 
            { 
                Id = this.Id
            };
            
            playerView.Player1 = Player1.Identifier == playerIdentifier ? Player1 : Player2;
            if (Player2 != null)
                playerView.Player2 = Player1.Identifier == playerIdentifier ? Player2.GetEnemyView() : Player1.GetEnemyView();
            if (historyStartIndex >= 0)
                playerView.RedrawPoints = HistoryObserver.GetPlayerViewCommands(playerIdentifier, historyStartIndex);
            else
                playerView.RedrawPoints = new List<ChangePoint>();
            playerView.HistoryLastIndex = this.HistoryObserver.CommandCount;

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
            command.Execute(this);
            this.HistoryObserver.Add(command);
        }
    }
}
