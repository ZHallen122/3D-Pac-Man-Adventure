using System.Collections.Generic;
using System;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;

        /*
         * Replace this with your documentation
         * 
         * Define your instance variables here
         */

        // Random variable for selecting edges
        private Random random = new Random();

        // Parents for representing parent of index i
        private int[] parents;

        // Ranks for path compression
        private int[] ranks;
        
        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }

        /*
         * Replace this with your documentation
         * 
         * DO NOT change the method signature
         * Define helper methods as 'private'
         */
        // Generate a maze using randomized Kruskal's algorithm
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            /*
             * Implement your maze generation algorithm here
             * Use helper methods as needed
             */

            // Initialize union find
            this.parents = new int[grid.NumberOfRows * grid.NumberOfColumns];
            this.ranks = new int[grid.NumberOfRows * grid.NumberOfColumns];

            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = i; // Initialize itself as its parent
                ranks[i] = 0;   // Initialize as no child
            }

            // List for storing edges
            List<((int Row, int Column) From, (int Row, int Column) To)> edges = new List<((int, int), (int, int))>();
            for (int row = 0; row < grid.NumberOfRows; row++)
            {
                for (int col = 0; col < grid.NumberOfColumns; col++)
                {
                    if (row > 0)
                    {
                        edges.Add(((row - 1, col), (row, col))); // Add edges connecting the cell above to the current cell
                    }
                    if (col > 0)
                    {
                        edges.Add(((row, col - 1), (row, col))); // Add edges connecting the left cell to the current cell
                    }
                }
            }

            // Randomly select edges and evaluate the set cells belong to 
            while (edges.Count > 0)
            {
                // Randomly select an edge and remove it from stored edges
                int randomIndex = random.Next(edges.Count);
                var edge = edges[randomIndex];
                edges.RemoveAt(randomIndex);

                // Determine if the cells are in the same set
                int cellX = edge.From.Row * grid.NumberOfColumns + edge.From.Column;
                int cellY = edge.To.Row * grid.NumberOfColumns + edge.To.Column;
                if (FindSet(cellX) != FindSet(cellY))
                {
                    UnionSet(cellX, cellY);
                    yield return edge;
                }
            }
        }

        // Find the root of the disjoint set
        private int FindSet(int cell)
        {
            if (parents[cell] != cell)
            {
                parents[cell] = FindSet(parents[cell]); // Path compression
            }

            return parents[cell];
        }

        // Link the root of smaller rank to the root of larger rank
        private void UnionSet(int cellX, int cellY)
        {
            int rootX = FindSet(cellX);
            int rootY = FindSet(cellY);
            if (rootX == rootY)
            {
                return;
            }
            else if (ranks[rootX] > ranks[rootY])
            {
                parents[rootY] = rootX;
            }
            else if (ranks[rootX] < ranks[rootY])
            {
                parents[rootX] = rootY;
            }
            else // If tie, increase rank of new root by 1
            {
                parents[rootX] = rootY;
                ranks[rootY]++;
            }
        }

        // Prim's Algorithm
        //private bool[,] visited; // Create 2D visited array of booleans
        //public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        //{
        //    // Initialize visited
        //    visited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
        //    visited[startRow, startColumn] = true;

        //    // List for storing edges
        //    List<((int Row, int Column) Cell, (int Row, int Column) Neighbour)> edges = new List<((int, int), (int, int))>();
        //    foreach (var neighbour in grid.Neighbors(startRow, startColumn))
        //    {
        //        edges.Add(((startRow, startColumn), neighbour));
        //    }

        //    // Randomly select edges
        //    while (edges.Count > 0)
        //    {
        //        int randomIndex = random.Next(edges.Count);
        //        var edge = edges[randomIndex];
        //        edges.RemoveAt(randomIndex);

        //        (int NeiRow, int NeiCol) = edge.Neighbour;

        //        // Check if only one cell is visited
        //        if (visited[edge.Cell.Row, edge.Cell.Column] ^ visited[NeiRow, NeiCol])
        //        {
        //            yield return (edge.Cell, edge.Neighbour);
        //            visited[NeiRow, NeiCol] = true;

        //            // Add the neighbor edge of the current cell to edge list
        //            foreach (var neighbour in grid.Neighbors(NeiRow, NeiCol))
        //            {
        //                if (!visited[neighbour.Row, neighbour.Column])
        //                {
        //                    edges.Add((edge.Neighbour, neighbour));
        //                }
        //            }
        //        }
        //    }

        //}
    }
}