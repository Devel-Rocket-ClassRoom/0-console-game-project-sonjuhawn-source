using Framework.Engine;
using Framework.MyGame;
using System;

class Player : GameObject
{
    private int x, y;
    Map map;
    public Player(Scene scene, Map map, int x, int y) : base(scene)
    {
        this.x = x;
        this.y = y;
        this.map = map;
    }

    void Slice(int x, int y)
    {
        while (true)
        {
            int dx = x + this.x;
            int dy = y + this.y;
            if (map.IsWall(dx, dy)/*벽에 도달시 탈출*/)
            {
                break;
            }
            else if(true/*골에 도달할 시*/)
            {

            }
            this.x = dx;
            this.y = dy;
        }
    }
    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.UpArrow))
        {
            Slice(0, 1);
        }
        if (Input.IsKeyDown(ConsoleKey.DownArrow))
        {
            Slice(0, -1);
        }
        if (Input.IsKeyDown(ConsoleKey.RightArrow))
        {
            Slice(1, 0);
        }
        if (Input.IsKeyDown(ConsoleKey.LeftArrow))
        {
            Slice(-1, 0);
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(x, y, '@', ConsoleColor.White);
    }
}

class Map : GameObject
{
    Player player;
    TileType[,] tiles;
    private int width;
    private int height;

    public Map(Scene scene) : base(scene)
    {
        this.width = 20;
        this.height = 20;
        tiles = new TileType[10, 10];
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
                        player = new Player(null, this, x, y);
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
    //void Create()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            tiles[x, y] = TileType.Empty;
    //        }
    //    }
    //    for (int x = 0; x < width; x++)
    //    {
    //        tiles[x, 0] = TileType.Wall;
    //        tiles[x, height - 1] = TileType.Wall;
    //    }
    //    for (int y = 0; y < height; y++)
    //    {
    //        tiles[0, y] = TileType.Wall;
    //        tiles[width - 1, y] = TileType.Wall;
    //    }
    //}

    public bool IsWall(int x, int y)
    {
        return tiles[x, y] == TileType.Wall;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (tiles[x, y] == TileType.Wall)
                {
                    buffer.SetCell(x, y, '#', ConsoleColor.Cyan);
                }
                if (tiles[x, y] == TileType.Goal)
                {
                    buffer.SetCell(x, y, '$', ConsoleColor.Magenta);
                }
                if (tiles[x, y] == TileType.Empty)
                {
                    buffer.SetCell(x, y, ' ', ConsoleColor.Magenta);
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

public class MyGame : GameApp
{
    private readonly SceneManager<Scene> _scenes;

    public MyGame() : base(40, 20)
    {
        _scenes = new SceneManager<Scene>();
    }

    protected override void Initialize()
    {
        ChangeToTitle();
    }

    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }
        _scenes.CurrentScene?.Update(deltaTime);
    }

    protected override void Draw()
    {
        _scenes.CurrentScene?.Draw(Buffer);
    }

    private void ChangeToTitle()
    {
        TitleScene title = new TitleScene();
        title.StartRequested += () => ChangeToPlay();
        _scenes.ChangeScene(title);
    }

    private void ChangeToPlay()
    {
        PlayScene play = new PlayScene();
        play.PlayAgainRequested += () => ChangeToPlay();
        _scenes.ChangeScene(play);
    }
}