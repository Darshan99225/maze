using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallState
{
    LEFT=1,
    RIGHT=2,
    UP=4,
    DOWN=8,
    VISITED = 128,
}


public struct Position
{
    public int X;
    public int Y;
}
public struct Neigbour
{
    public Position Position;
    public WallState ShareWall;
}
public static class MazeGenerator
{
    private static WallState GetoppositeWall(WallState wall)
    {
       switch(wall)
        {
            case WallState.LEFT:return WallState.RIGHT;
            case WallState.RIGHT:return WallState.LEFT;
            case WallState.UP:return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }
    private static WallState[,] ApplyRecurisiveBacktracker(WallState[,] maze, int width,int height)
    {
        var rng = new System.Random();
        var postionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };
        maze[position.X, position.Y] |= WallState.VISITED;
        postionStack.Push(position);
        while(postionStack.Count>0)
        {
            var current = postionStack.Pop();
            var neighbours = GetNeigbours(current, maze, width, height);
            if(neighbours.Count>0)
            {
                postionStack.Push(current);
                var ranIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[ranIndex];
                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.ShareWall;
                maze[nPosition.X, nPosition.Y] &= ~GetoppositeWall(randomNeighbour.ShareWall);
                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;
                postionStack.Push(nPosition);
            }
        }
        return maze;
    }
    private static List<Neigbour> GetNeigbours(Position p, WallState[,] maze, int width, int height)
    {
        var list = new List<Neigbour>();
        if (p.X > 0)
        {
            if (!maze[p.X - 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neigbour
                {
                    Position = new Position
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    ShareWall = WallState.LEFT
                });

            }
        }
        if (p.Y > 0)
        {
            if (!maze[p.X, p.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neigbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y - 1
                    },
                    ShareWall = WallState.DOWN
                });

            }
        }
        if (p.Y < height-1)
        {
            if (!maze[p.X, p.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neigbour
                {
                    Position = new Position
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    ShareWall = WallState.UP
                });

            }
        }
        if (p.X < width-1)
        {
            if (!maze[p.X + 1, p.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neigbour
                {
                    Position = new Position
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    ShareWall = WallState.RIGHT
                });

            }
        }
        return list;
    }
        public static WallState[,] Generate(int width, int height)
        {
            WallState[,] maze = new WallState[width, height];
            WallState initial = WallState.LEFT | WallState.RIGHT | WallState.UP | WallState.DOWN;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    maze[i, j] = initial;
                }
            }
            return ApplyRecurisiveBacktracker(maze,width,height);
        }
    
}
