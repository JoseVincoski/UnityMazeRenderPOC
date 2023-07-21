using Models;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace MazeGenerators.Generators
{
    public class MazeGenerator : MonoBehaviour
    {
        public Maze Maze;
        private IGenerator Generator;

        public void InitializeGenerator(IGenerator _generator, int _mazeHeight, int _mazeWidth)
        {
            Maze = new Maze(_mazeHeight, _mazeWidth);
            Generator = _generator;
        }
        public void RenderBase(Transform parent, float renderSpeed)
        {
            Generator.RenderBase(ref Maze, parent, renderSpeed);
        }
        public void GenerateInterestPoints()
        {
            Generator.GenerateInterestPoints(ref Maze);
        }

        public void GenerateMaze(float renderSpeed)
        {
            Generator.GenerateMaze(ref Maze, renderSpeed);
        }
        public void SlowlyRenderBase(Transform parent, float renderSpeed)
        {
            StartCoroutine(Generator.SlowlyRenderBase(Maze, parent, renderSpeed));
        }
        public void SlowlyGenerateMaze(float renderSpeed)
        {
            StartCoroutine(Generator.SlowlyGenerateMaze(Maze, renderSpeed));
        }

        public void UnloadMaze()
        {
            if (Maze == null) return;

            for (int row = 0; row < Maze.Height; row++)
            {
                for (int column = 0; column < Maze.Width; column++)
                {
                    Destroy(Maze.Tiles[row, column].GameObject);
                }
            }

            Maze = null;
        }
    }
}
