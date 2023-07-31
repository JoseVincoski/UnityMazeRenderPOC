using Models;
using System.Text;
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
        public void RenderBase(Transform parent)
        {
            Generator.RenderBase(ref Maze, parent);
        }
        public void GenerateInterestPoints()
        {
            Generator.GenerateInterestPoints(ref Maze);
        }

        public void GenerateMaze(bool slowly)
        {
            StartCoroutine(Generator.GenerateMaze(Maze, slowly));
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
