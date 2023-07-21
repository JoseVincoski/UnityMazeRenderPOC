using MazeGenerators.Generators;
using MazeGenerators.Generators.V2;
using Models;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class MazeScript : MonoBehaviour
{
    public float renderSpeed;
    private Maze Maze;
    private GameObject MazeContainer;
    private MazeGenerator generator;
    private IGenerator genVersion;

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

        if (slowly) generator.SlowlyGenerateMaze(renderSpeed);
        else generator.GenerateMaze(renderSpeed);
    }
    public void UnloadMaze()
    {
        generator.UnloadMaze();
        Maze = generator.Maze;
    }
    public void GenerateBase(bool slowly)
    {
        if (Maze == null) InitializeGenerator();

        if (slowly) generator.SlowlyRenderBase(MazeContainer.transform, renderSpeed);
        else generator.RenderBase(MazeContainer.transform, renderSpeed);
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
}
