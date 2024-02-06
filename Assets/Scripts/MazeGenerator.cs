using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MazeGenerator
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public struct Neighbour
    {
        public Position Position;
        public WallState SharedWall;
    }

    private static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }
    private static WallState[,] ApplyRecursiveDivision(WallState[,] maze, int width, int height)
    {
        Divide(maze, 0, 0, width - 1, height - 1);
        return maze;
    }

    private static void Divide(WallState[,] maze, int startX, int startY, int endX, int endY)
{
    if (endX - startX < 2 || endY - startY < 2)
    {
        // Base case: area is too small to divide further
        return;
    }

    // Ensure the division points are odd to avoid odd-sized passages
    int divideX = (UnityEngine.Random.Range(startX + 1, endX) / 2) * 2 + 1;
    int divideY = (UnityEngine.Random.Range(startY + 1, endY) / 2) * 2 + 1;

    // Create a passage at the division point
    for (int i = startX; i <= endX; i++)
    {
        if (i != divideX)
        {
            maze[i, divideY] &= ~WallState.DOWN;
            maze[i, divideY + 1] &= ~WallState.UP;
        }
    }

    for (int j = startY; j <= endY; j++)
    {
        if (j != divideY)
        {
            maze[divideX, j] &= ~WallState.RIGHT;
            maze[divideX + 1, j] &= ~WallState.LEFT;
        }
    }

    // Recursively divide the four sub-areas
    Divide(maze, startX, startY, divideX - 1, divideY - 1);
    Divide(maze, divideX + 1, startY, endX, divideY - 1);
    Divide(maze, startX, divideY + 1, divideX - 1, endY);
    Divide(maze, divideX + 1, divideY + 1, endX, endY);
}

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        // here we make changes
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        maze[position.X, position.Y] |= WallState.VISITED;  // 1000 1111
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbor = GetUnvisitedNeighbors(current, maze, width, height);

            if (neighbor.Count > 0)
            {
                positionStack.Push(current);

                var randIndex = rng.Next(0, neighbor.Count);
                var randomNeighbor = neighbor[randIndex];

                var nPosition = randomNeighbor.Position;
                maze[current.X, current.Y] &= ~randomNeighbor.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOppositeWall(randomNeighbor.SharedWall);
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    private static List<Neighbour> GetUnvisitedNeighbors(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0) // left
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.LEFT
                });
            }
        }

        if (p.Y > 0) // DOWN
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    SharedWall = WallState.DOWN
                });
            }
        }

        if (p.Y < height - 1) // UP
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    SharedWall = WallState.UP
                });
            }
        }

        if (p.X < width - 1) // RIGHT
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    SharedWall = WallState.RIGHT
                });
            }
        }

        return list;
    }
    
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        //Initially all the walls EXIST
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i, j] = initial; //1111
            }
        }
        // maze[0, UnityEngine.Random.Range(0, height)] &= ~WallState.LEFT; // Remove left wall of leftmost cell
        // maze[width - 1, UnityEngine.Random.Range(0, height)] &= ~WallState.RIGHT; // Remove right wall of rightmost cell
        //return ApplyRecursiveBacktracker(maze, width, height);
        return ApplyRecursiveDivision(maze, width, height);
    }
}
[Flags]
public enum WallState
{
    //0000 -> No Walls
    //1111 -> Walls all around
    LEFT = 1, // 0001
    RIGHT = 2, // 0010
    UP = 4, // 0100
    DOWN = 8, // 1000

    VISITED = 128, // 1000 0000
}