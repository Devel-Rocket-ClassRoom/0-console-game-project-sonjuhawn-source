using Framework.Engine;
using Framework.MyGame;
using System;
using System.Security.Cryptography.X509Certificates;

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

class Map
{
    Player player;
    TileType[,] tiles;
    private int width;
    private int height;

    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
        tiles = new TileType[width, height];

        Create();
    }

    void Create()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = TileType.Empty;
            }
        }
        for (int x = 0; x < width; x++)
        {
            tiles[x, 0] = TileType.Wall;
            tiles[x, height - 1] = TileType.Wall;
        }
        for (int y = 0; y < height; y++)
        {
            tiles[0, y] = TileType.Wall;
            tiles[width - 1, y] = TileType.Wall;
        }
        int a = Random.Shared.Next(0, width - 1);
        int b = Random.Shared.Next(0, height - 1);
        tiles[a, b] = TileType.Goal;
        
        Random ranx = new Random();

    }
    public bool IsWall(int x, int y)
    {
        return tiles[x,y] == TileType.Wall;
    }

    public void Draw(ScreenBuffer buffer)
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
            }
        }
        player.Draw(buffer);
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

    public MyGame() : base(60, 30)
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