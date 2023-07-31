using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerators.Generators.V2.Models
{
    public class TilesAroundInfo
    {
        private MazePosition basePosition;
        private Maze maze;
        private Random RNG = new System.Random(); 

        public List<TileInfo> TileInfos = new List<TileInfo>();

        public List<TileInfo> MovableWalls { get { UpdateTiles(); return GetTileType(TileType.MovableWall); } }
        public List<TileInfo> Paths { get { UpdateTiles(); return GetTileType(TileType.Path); } }
        public List<TileInfo> SolidWalls { get { UpdateTiles(); return GetTileType(TileType.SolidWall); } }

        public TilesAroundInfo(Maze maze, int Y, int X)
        {
            basePosition = new MazePosition(Y, X);
            this.maze = maze;

            UpdateTiles();
        }
        private List<TileInfo> GetTileType(TileType tileType)
        {
            return TileInfos.FindAll(x => x.TileType == tileType);
        }

        private void UpdateTiles()
        {
            List<int> rng = new List<int>() { 0, 1, 2, 3 };

            TileInfos.Clear();
            TileInfos.Add(new TileInfo()
            {
                Position = new MazePosition(basePosition.Y - 1, basePosition.X),
                NextPathPosition = new MazePosition(basePosition.Y - 2, basePosition.X),
                TileType = maze.GetTileTypeInPosition(basePosition.Y - 1, basePosition.X),
                Direction = TileDirection.N,
                Priority = GetRandomDecreasingDirection(ref rng),
            });

            TileInfos.Add(new TileInfo()
            {
                Position = new MazePosition(basePosition.Y, basePosition.X + 1),
                NextPathPosition = new MazePosition(basePosition.Y, basePosition.X + 2),
                TileType = maze.GetTileTypeInPosition(basePosition.Y, basePosition.X + 1),
                Direction = TileDirection.E,
                Priority = GetRandomDecreasingDirection(ref rng),
            });

            TileInfos.Add(new TileInfo()
            {
                Position = new MazePosition(basePosition.Y + 1, basePosition.X),
                NextPathPosition = new MazePosition(basePosition.Y + 2, basePosition.X),
                TileType = maze.GetTileTypeInPosition(basePosition.Y + 1, basePosition.X),
                Direction = TileDirection.S,
                Priority = GetRandomDecreasingDirection(ref rng),
            });

            TileInfos.Add(new TileInfo()
            {
                Position = new MazePosition(basePosition.Y, basePosition.X - 1),
                NextPathPosition = new MazePosition(basePosition.Y, basePosition.X - 2),
                TileType = maze.GetTileTypeInPosition(basePosition.Y, basePosition.X - 1),
                Direction = TileDirection.W,
                Priority = GetRandomDecreasingDirection(ref rng),
            });
        }

        private int GetRandomDecreasingDirection(ref List<int> rng)
        {
            int index = RNG.Next(0, rng.Count);

            var numberToReturn = rng.ElementAt(index);
            rng.RemoveAt(index);

            return numberToReturn;
        }
    }
}
