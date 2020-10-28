using BattleSweeperServer.DesignPatternClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

            Tile tile = new Tile();
            Tiles.Add(tile);

            for (int i = 1; i < this.Size * this.Size; i++)
                Tiles.Add((Tile)tile.Clone());
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

        public List<ChangePoint> RevealTile(int x, int y)
        {
            List<ChangePoint> points = new List<ChangePoint>();
            Tile currentTile = Tiles[GetIndex(x, y)];
            if (currentTile.State >= 0)
                return points; // already revealed dumbass
            
            currentTile.State = 0;

            if (currentTile.Mine != null)
            {
                return points.Concat(currentTile.Mine.OnReveal(this, x, y)).ToList();
            }

            List<Tile> neighbours = GetNeighbours(x, y);
            foreach (Tile tile in neighbours)
            {
                if (tile.Mine != null)
                    currentTile.State++;
            }
            
            points.Add(new ChangePoint(x, y));
            return points;
        }

        public bool WithinBounds(int x , int y)
        {
            return 0 <= x && x < Size && 0 <= y && y < Size;
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
                    if (WithinBounds(xx,yy) && index != currentIndex)
                        neighbours.Add(Tiles[index]);
                }
            }

            return neighbours;
        }

        internal ChangePoint CycleMine(int positionX, int positionY, bool undo = false)
        {
            MineFactory mineFactory = new MineFactory();
            Tile tile = Tiles[GetIndex(positionX, positionY)];
            if (tile.Mine == null)
                tile.Mine = undo ? mineFactory.CreateMine(2) : mineFactory.CreateMine(0);
            else if (tile.Mine is SimpleMine)
                tile.Mine = undo ? null : mineFactory.CreateMine(1);
            else if (tile.Mine is WideMine)
                tile.Mine = undo ? mineFactory.CreateMine(0) : mineFactory.CreateMine(2);
            else
                tile.Mine = undo ? mineFactory.CreateMine(1) : null;

            return new ChangePoint(positionX, positionY);
        }
    }
}
