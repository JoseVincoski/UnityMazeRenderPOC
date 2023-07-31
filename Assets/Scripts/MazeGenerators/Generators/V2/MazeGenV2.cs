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
        public float renderSpeed = 1;
        private System.Random RNG = new System.Random();

        public IEnumerator GenerateMaze(Maze maze, bool slowly = false)
        {
            for (int row = 1; row < maze.Height; row += 2)
            {
                for (int column = 1; column < maze.Width; column += 2)
                {




                    maze.Tiles[row, column].GameObject.transform.position = new Vector3(
                        maze.Tiles[row, column].GameObject.transform.position.x,
                        maze.Tiles[row, column].GameObject.transform.position.y,
                        0.5f);
                    maze.Tiles[row, column].GameObject.GetComponent<MeshRenderer>().material.color = Color.red; 
                    if (slowly) yield return new WaitForSeconds(renderSpeed);
                    else yield return null;





                    var tilesAroundInfo = new TilesAroundInfo(maze, row, column);

                    if (tilesAroundInfo.MovableWalls.Count > 1)
                    {
                        var numberOfWallsToCarve = RNG.Next(1, tilesAroundInfo.MovableWalls.Count);


                        for (int i = 0; i < numberOfWallsToCarve; i++)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.OrderByDescending(p => p.Priority).First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.Path;

                            if (slowly) yield return new WaitForSeconds(renderSpeed);
                            else yield return null;
                        }

                        if (tilesAroundInfo.MovableWalls.Count == 1 && RNG.Next(11) < 7)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.SolidWall;

                            if (slowly) yield return new WaitForSeconds(renderSpeed);
                            else yield return null;
                        }

                        if (tilesAroundInfo.SolidWalls.Count + tilesAroundInfo.MovableWalls.Count > 2)
                        {
                            var wallToChange = tilesAroundInfo.MovableWalls.OrderByDescending(p => p.Priority).FirstOrDefault()?.Position;
                            if (wallToChange == null) wallToChange = tilesAroundInfo.SolidWalls.OrderByDescending(p => p.Priority).First().Position;
                            maze.Tiles[wallToChange.Y, wallToChange.X].TileType = TileType.Path;

                            if (slowly) yield return new WaitForSeconds(renderSpeed);
                            else yield return null;
                        }



                        maze.Tiles[row, column].GameObject.transform.position = new Vector3(
                        maze.Tiles[row, column].GameObject.transform.position.x,
                        maze.Tiles[row, column].GameObject.transform.position.y,
                        1f);
                        maze.Tiles[row, column].GameObject.GetComponent<MeshRenderer>().material.color = Color.grey;





                    }
                }
                if (slowly) yield return new WaitForSeconds(renderSpeed);
                else yield return null;
            }
            if (slowly) yield return new WaitForSeconds(renderSpeed);
            else yield return null;
        }
    }
}
