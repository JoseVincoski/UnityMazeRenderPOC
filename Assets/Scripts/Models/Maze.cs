using MazeGenerators.Models;
using Models.Enums;
using TMPro;

namespace Models
{
    public class Maze
    {
        public MazeTile[,] Tiles { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }

        public Maze(int _mazeHeight, int _mazeWidth)
        {
            Height = _mazeHeight * 3;
            Width = _mazeWidth * 3;

            Tiles = new MazeTile[Height, Width];
        }

        public TileType GetTileTypeInPosition(int y, int x)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return TileType.OutsideFrame;

            return Tiles[y, x].TileType;
        }
    }
}
