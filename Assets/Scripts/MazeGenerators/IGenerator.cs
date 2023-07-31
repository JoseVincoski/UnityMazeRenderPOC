using MazeGenerators.Models;
using Models;
using Models.Enums;
using System;
using System.Collections;
using UnityEngine;

namespace MazeGenerators.Generators
{
    public interface IGenerator
    {
        public IEnumerator GenerateMaze(Maze maze, bool slowly) { throw new NotImplementedException(); }
        public void RenderBase(ref Maze maze, Transform parent)
        {
            for (int row = 0; row < maze.Height; row++)
            {
                for (int column = 0; column < maze.Width; column++)
                {
                    var tile = new MazeTile(parent);
                    tile.GameObject.transform.position = new Vector3(parent.position.x + column * 2, parent.position.y + row * 2, parent.position.z);

                    if (row == 0 || row == maze.Height - 1)
                    {
                        if (column == 0 || column == maze.Height - 1)
                        {
                            tile.GameObject.name = "PillarFrame";
                            tile.TileType = TileType.Pillar;
                        }
                        else
                        {
                            tile.GameObject.name = "HFrame";
                            tile.TileType = TileType.MazeFrame;
                        }
                    }
                    else if (column == 0 || column == maze.Width - 1)
                    {
                        if (row == 0 || row == maze.Height - 1)
                        {
                            tile.GameObject.name = "PillarFrame";
                            tile.TileType = TileType.Pillar;
                        }
                        else
                        {
                            tile.GameObject.name = "VFrame";
                            tile.GameObject.transform.Rotate(0, 0, 90);
                            tile.TileType = TileType.MazeFrame;
                        }
                    }
                    else if (row % 2 == 0)
                    {
                        if (column % 2 == 0)
                        {
                            tile.GameObject.name = "FixedPillar";
                            tile.TileType = TileType.Pillar;
                        }
                        else
                        {
                            tile.GameObject.name = "HWall";
                            tile.TileType = TileType.MovableWall;
                        }
                    }
                    else if (column % 2 == 0)
                    {
                        if (row % 2 == 0)
                        {
                            tile.GameObject.name = "FixedPillar";
                            tile.TileType = TileType.Pillar;
                        }
                        else
                        {
                            tile.GameObject.name = "VWall";
                            tile.GameObject.transform.Rotate(0, 0, 90);
                            tile.TileType = TileType.MovableWall;
                        }
                    }
                    else
                    {
                        tile.GameObject.name = "Path";
                        tile.TileType = TileType.Path;
                    }

                    maze.Tiles[row, column] = tile;
                }
            }
        }
        public void GenerateInterestPoints(ref Maze maze)
        {
            int startRow = 1;
            int startColumn = 1;
            maze.Tiles[startRow, startColumn].TileType = TileType.Start;

            int targetRow = 1;
            int targetColumn = 1;
            while(startRow == targetRow && startColumn == targetColumn)
            {
                targetRow = UnityEngine.Random.Range(1, maze.Height / 2) * 2 + 1;
                targetColumn = UnityEngine.Random.Range(1, maze.Width / 2) * 2 + 1;
            }

            maze.Tiles[targetRow, targetColumn].TileType = TileType.Target;
        }
    }
}
