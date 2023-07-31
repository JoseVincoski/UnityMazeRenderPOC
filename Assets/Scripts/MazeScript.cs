using MazeGenerators.Generators;
using MazeGenerators.Generators.V2;
using Models;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    private Maze Maze;
    private GameObject MazeContainer;
    private MazeGenerator generator;
    private MazeGenV2 genVersion;

    public int mazeHeight = 5;
    public int mazeWidth = 5;

    void Start()
    {
        MazeContainer = GameObject.Find("MazeContainer");

        generator = Instantiate(Prefabs.MazeGenerator).GetComponent<MazeGenerator>();
        genVersion = Instantiate(Prefabs.V2).GetComponent<MazeGenV2>();
    }

    public void GenerateMaze(bool slowly)
    {
        if (Maze == null) GenerateBase(false);

        generator.GenerateMaze(slowly);
    }
    public void UnloadMaze()
    {
        generator.UnloadMaze();
        Maze = generator.Maze;
    }
    public void GenerateBase(bool slowly)
    {
        if (Maze == null) InitializeGenerator();

        generator.RenderBase(MazeContainer.transform);
    }
    public void GenerateInterestPoints()
    {
        if (Maze == null) throw new Exception("Can't generate points if maze is null");

        generator.GenerateInterestPoints();
    }
    private MazeGenerator InitializeGenerator()
    {
        generator.InitializeGenerator(genVersion, mazeHeight, mazeWidth);
        Maze = generator.Maze;

        return generator;
    }

    public void PrintMaze()
    {
        var sb = new StringBuilder();
        for (int row  = 0; row < generator.Maze.Height; row++)
        {
            for (int column = 0;  column < generator.Maze.Width; column++)
            {
                sb.Append((int)generator.Maze.Tiles[row, column].TileType);
            }
            sb.AppendLine();
        }

        Debug.Log(sb.ToString());
    }
}
