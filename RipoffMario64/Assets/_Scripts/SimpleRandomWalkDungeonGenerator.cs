using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{


    [SerializeField]
    private SimpleRandomWalkSO randomWalkParameters;
   
    protected override void RunProcedualGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            var path = ProceudalGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }

}
