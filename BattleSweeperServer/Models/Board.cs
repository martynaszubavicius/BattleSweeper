using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BattleSweeperServer.Models
{
    public class Board
    {
        [JsonProperty("Size")]
        public int Size { get; set; }

        [JsonProperty("Tiles")]
        public List<Tile> Tiles { get; set; }

        public Board()
        {

        }

        public Board(int size)
        {
            this.Size = size;
            this.Tiles = new List<Tile>();

            for (int i = 0; i < this.Size * this.Size; i++)
                Tiles.Add(new Tile(-1));
        }

        public int GetIndex(int x, int y)
        {
            return y * this.Size + x;
        }

        public Board GetEnemyView()
        {
            Board enemyView = new Board
            {
                Size = this.Size,
                Tiles = new List<Tile>()
            };

            for (int i = 0; i < this.Size * this.Size; i++)
            {
                Tile tile = new Tile(this.Tiles[i].State);
                if (tile.State >= 0)
                    tile.Mine = this.Tiles[i].Mine;
                enemyView.Tiles.Add(tile);
            }
            
            return enemyView;
        }

        public void RevealTile(int x, int y)
        {
            Tile currentTile = Tiles[GetIndex(x, y)];
            if (currentTile.State >= 0)
                return; // already revealed dumbass
            
            currentTile.State = 0;

            if (currentTile.Mine != null)
                return;

            List<Tile> neighbours = GetNeighbours(x, y);
            foreach (Tile tile in neighbours)
            {
                if (tile.Mine != null)
                    currentTile.State++;
            }
        }

        private List<Tile> GetNeighbours(int x, int y)
        {
            List<Tile> neighbours = new List<Tile>();
            int sizeSquared = Size * Size;
            int currentIndex = GetIndex(x, y);

            for (int xx = x - 1; xx <= x+1; xx++)
            {
                for (int yy = y - 1; yy <= y + 1; yy++)
                {
                    int index = GetIndex(xx, yy);
                    if (0 <= xx && xx < Size && 0 <= yy && yy < Size && index != currentIndex)
                        neighbours.Add(Tiles[index]);
                }
            }

            return neighbours;
        }

        internal void CycleMine(int positionX, int positionY)
        {
            Tile tile = Tiles[GetIndex(positionX, positionY)];
            if (tile.Mine == null)
                tile.Mine = new Mine();
            else
                tile.Mine = null;
        }
    }
}
