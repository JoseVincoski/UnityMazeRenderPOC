using System;
using System.Collections;
using System.Linq;
using MazeGenerators.Generators.V2.Models;
using Models;
using Models.Enums;
using UnityEngine;

namespace MazeGenerators.Generators.V2
{
    public class MazeGenV2 : MonoBehaviour, IGenerator
    {
        public Maze maze;
        private System.Random RNG = new System.Random((int)DateTime.Now.Ticks);

        public void GenerateMaze(ref Maze maze, float renderSpeed)
        {

            for (int row = 1; row < maze.Height - 1; row += 2)
            {
                for (int column = 1; column < maze.Width - 1; column += 2)
                {
                    var tilesAroundInfo = new TilesAroundInfo(maze, row, column);

                    if (tilesAroundInfo.MovableWalls.Count > 1)
                    {
                        var numberOfWallsToCarve = RNG.Next(1, tilesAroundInfo.MovableWalls.Count);

                        for (int i = 0; i < numberOfWallsToCarve; i++)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.OrderByDescending(p => p.Priority).First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.Path;
                        }

                        //Set last wall as solid wall
                        if (tilesAroundInfo.MovableWalls.Count == 1)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.SolidWall;
                        }
                        //Set current path as verified path
                        maze.Tiles[row, column].TileType = TileType.VerifiedPath;
                    }
                }
            }
        }

        public IEnumerator SlowlyGenerateMaze(Maze maze, float renderSpeed)
        {
            for (int row = 1; row < maze.Height - 1; row += 2)
            {
                for (int column = 1; column < maze.Width - 1; column += 2)
                {
                    var tilesAroundInfo = new TilesAroundInfo(maze, row, column);

                    if (tilesAroundInfo.MovableWalls.Count > 1)
                    {
                        var numberOfWallsToCarve = RNG.Next(1, tilesAroundInfo.MovableWalls.Count);

                        for (int i = 0; i < numberOfWallsToCarve; i++)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.OrderByDescending(p => p.Priority).First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.Path;

                            yield return new WaitForSeconds(renderSpeed / 2);
                        }

                        //Set last wall as solid wall
                        if (tilesAroundInfo.MovableWalls.Count == 1)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.SolidWall;
                        }
                        //Set current path as verified path
                        maze.Tiles[row, column].TileType = TileType.VerifiedPath;
                    }
                }
                yield return new WaitForSeconds(renderSpeed);
            }
            yield return new WaitForSeconds(renderSpeed);
        }
    }
}
