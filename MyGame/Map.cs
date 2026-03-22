using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

class Map : GameObject
{
    public Player player;
    TileType[,] tiles;
    private int width;
    private int height;

    public Map(Scene scene, int width, int height) : base(scene)
    {
        this.width = width;
        this.height = height;
        tiles = new TileType[width, height];
    }

    public void Create(string[] mapData)
    {
        height = mapData.Length;
        width = mapData[0].Length;

        tiles = new TileType[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char c = mapData[y][x];

                switch (c)
                {
                    case '#':
                        tiles[x, y] = TileType.Wall;
                        break;

                    case '.':
                        tiles[x, y] = TileType.Empty;
                        break;

                    case 'S':
                        tiles[x, y] = TileType.Empty;
                        player = new Player(this.Scene, this, x, y);
                        break;

                    case '$':
                        tiles[x, y] = TileType.Goal;
                        break;

                    default:
                        tiles[x, y] = TileType.Empty;
                        break;
                }
            }
        }
    }
    public bool IsWall(int x, int y)
    {
        return tiles[x, y] == TileType.Wall;
    }
    public bool IsGoal(int x, int y)
    {
        return tiles[x, y] == TileType.Goal;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == TileType.Wall)
                {
                    buffer.SetCell(x + 3, y + 5, '#', ConsoleColor.Cyan);
                }
                if (tiles[x, y] == TileType.Goal)
                {
                    buffer.SetCell(x + 3, y + 5, '$', ConsoleColor.Magenta);
                }
                if (tiles[x, y] == TileType.Empty)
                {
                    buffer.SetCell(x + 3, y + 5, ' ', ConsoleColor.Magenta);
                }
            }
        }
        player.Draw(buffer);
    }

    public override void Update(float deltaTime)
    {
        player.Update(deltaTime);
    }

}

enum TileType
{
    Empty,
    Wall,
    Goal
}