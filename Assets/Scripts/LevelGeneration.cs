using System.Collections.Generic;
using UnityEngine;

namespace ShareefSoftware
{
    public class LevelGeneration : MonoBehaviour
    {
        [SerializeField] TileStyle tileStyle;
        [SerializeField] int numberOfRows = 10;
        [SerializeField] int numberOfColumns = 10;
        [SerializeField] List<GameObject> barrierPrefabs;
        [SerializeField] List<GameObject> pathPrefabs;
        [SerializeField] private float cellWidth;
        [SerializeField] private float cellHeight;
        [SerializeField] private Transform parentForNewObjects;
        [SerializeField] int randomSeed = 0;

        // Declare prefabs
        [SerializeField] GameObject coinPrefab;
        [SerializeField] GameObject exitPrefab;

        private void Awake()
        {
            System.Random random = CreateRandom();
            var maze = new Maze(numberOfRows, numberOfColumns, random);
            IGridGraph<bool> occupancyGrid = ConvertMazeToOccupancyGraph(maze);
            CreatePrefabs(random, occupancyGrid);

            // Place coins, exit, and player prefabs.
            CreateCoinPrefabs(maze);
            CreateExitPrefab(maze);
        }

        // Place the coin prefab to deadends by calculating the position.
        private void CreateCoinPrefabs(Maze maze)
        {
            foreach (var deadEnd in MazeQuery.DeadEnds(maze))
            {
                float x = (2 * deadEnd.Column + 1) * cellWidth;
                float y = 3.0f;
                float z = (2 * deadEnd.Row + 1) * cellHeight;

                Vector3 deadEndPosition = new Vector3(x, y, z);
                Instantiate(coinPrefab, deadEndPosition, Quaternion.identity, parentForNewObjects);
            }
        }

        // Place the exit text to the position of the exit in the maze.
        private void CreateExitPrefab(Maze maze)
        {
            // Find the position of the exit
            float x = (2 * (numberOfColumns - 1) + 1) * cellWidth;
            float y = 3.0f;
            float z = (2 * (numberOfRows - 1) + 1) * cellHeight + cellHeight;

            Vector3 exitPosition = new Vector3(x, y, z);
            Instantiate(exitPrefab, exitPosition, Quaternion.identity, parentForNewObjects);
        }

        // Return the position of the center of the starting cell of the maze
        public Vector3 GetStartingCellCenterPosition()
        {
            float x = cellWidth;
            float y = 3f;
            float z = 0;
            return new Vector3(x, y, z);
        }

        private void CreatePrefabs(System.Random random, IGridGraph<bool> occupancyGrid)
        {
            var pathFactory = new GameObjectFactoryRandomFromList(pathPrefabs, random) { Parent = parentForNewObjects };
            var wallFactory = new GameObjectFactoryRandomFromList(barrierPrefabs, random) { Parent = parentForNewObjects };
            var gridFactory = new GridGameObjectFactory(cellWidth, cellHeight)
            {
                PrefabFactoryIfTrue = pathFactory,
                PrefabFactoryIfFalse = wallFactory
            };
            gridFactory.CreatePrefabs(occupancyGrid);
        }

        private System.Random CreateRandom()
        {
            if (randomSeed == 0)
            {
                randomSeed = (int)System.DateTime.Now.Ticks & 0x0000FFFF;
            }
            Debug.Log("Random Seed: " + randomSeed);
            System.Random random = new System.Random(randomSeed);
            return random;
        }

        private IGridGraph<bool> ConvertMazeToOccupancyGraph(Maze maze)
        {
            IGridGraph<bool> occupancyGrid;
            if (tileStyle == TileStyle.Small2x2)
                occupancyGrid = MazeUtility.Create2x2OccupancyGridFromMaze(maze);
            else
                occupancyGrid = MazeUtility.Create3x3OccupancyGridFromMaze(maze, tileStyle);
            PrintOccupancyGrid(occupancyGrid);
            return occupancyGrid;
        }

        private static void PrintOccupancyGrid(IGridGraph<bool> occupancyGrid)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int row = occupancyGrid.NumberOfRows - 1; row >= 0; row--)
            {
                for (int column = 0; column < occupancyGrid.NumberOfColumns; column++)
                {
                    char symbol = (occupancyGrid.GetCellValue(row, column) == true) ? ' ' : '█';
                    stringBuilder.Append(symbol);
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
            Debug.Log(stringBuilder.ToString());
        }
    }
}