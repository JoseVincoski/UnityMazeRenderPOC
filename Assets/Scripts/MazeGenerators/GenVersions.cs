using UnityEngine;

namespace MazeGenerators.Generators
{
    public static class Prefabs {

        public static Object MazeGenerator
        {
            get
            {
                return Resources.Load("MazeGeneratorScript");
            }
        }

        public static Object V2
        {
            get
            {
                return Resources.Load("MazeGenV2Script");
            }
        }

    }
}
