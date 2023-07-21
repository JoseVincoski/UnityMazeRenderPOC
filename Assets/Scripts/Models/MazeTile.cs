using Models.Enums;
using UnityEngine;

namespace MazeGenerators.Models
{
    public class MazeTile
    {
        private static Color SolidWallColor = Color.blue;
        private static Color MovableWallColor = Color.red;
        private static Color StartPathColor = Color.yellow;
        private static Color TargetPathColor = Color.green;
        private static Color PathColor = Color.gray;
        private static Color CurrentPathColor = Color.black;

        public GameObject GameObject;

        public MazeTile(Transform parent)
        {
            this.GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.GameObject.transform.SetParent(parent);

            this.GameObject.transform.localScale = Vector3.zero;
        }

        private TileType _tileType;
        public TileType TileType
        {
            get { return _tileType; }
            set
            {
                _tileType = value;
                ChangeTileType(value);
            }
        }

        private void ChangeTileType(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Path:
                    this.GameObject.transform.localScale = new Vector3(4, 4, 1);
                    this.GameObject.transform.position = new Vector3(
                        this.GameObject.transform.position.x,
                        this.GameObject.transform.position.y,
                        this.GameObject.transform.position.z + 1
                        );
                    this.GameObject.GetComponent<MeshRenderer>().material.color = PathColor;
                    break;
                case TileType.SolidWall:
                    this.GameObject.transform.localScale = new Vector3(3, 1, 1);
                    this.GameObject.GetComponent<MeshRenderer>().material.color = SolidWallColor;
                    break;
                case TileType.MovableWall:
                    this.GameObject.transform.localScale = new Vector3(3, 1, 1);
                    this.GameObject.GetComponent<MeshRenderer>().material.color = MovableWallColor;
                    break;
                case TileType.Pillar:
                    this.GameObject.transform.localScale = new Vector3(1, 1, 1);
                    this.GameObject.GetComponent<MeshRenderer>().material.color = SolidWallColor;
                    break;
                case TileType.Start:
                    this.GameObject.transform.localScale = new Vector3(4, 4, 1);
                    this.GameObject.GetComponent<MeshRenderer>().material.color = StartPathColor;
                    break;
                case TileType.Target:
                    this.GameObject.transform.position = new Vector3(
                        this.GameObject.transform.position.x,
                        this.GameObject.transform.position.y,
                        this.GameObject.transform.position.z - 0.5f
                        );
                    this.GameObject.transform.localScale = new Vector3(4, 4, 1);
                    this.GameObject.GetComponent<MeshRenderer>().material.color = TargetPathColor;
                    break;
            }
        }
    }
}
