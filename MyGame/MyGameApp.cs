using Framework.Engine;
using Framework.MyGame;
using System;

class Player : GameObject
{
    private int x, y;
    public Player(Scene scene, int x, int y) : base(scene)
    {
        this.x = x;
        this.y = y;
    }

    void Slice(int x, int y)
    {
        int dx = x + this.x;
        int dy = y + this.y;

        while (true)
        {
            if (true /*벽에 닿을경우*/)
            {
                break;
            }
            x = dx;
            y = dy;
        }
    }
    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.UpArrow))
        {

        }
        if (Input.IsKeyDown(ConsoleKey.DownArrow))
        {

        }
        if (Input.IsKeyDown(ConsoleKey.RightArrow))
        {

        }
        if (Input.IsKeyDown(ConsoleKey.LeftArrow))
        {

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
        for(int y = 0; y < height;)
        {
            tiles[0,y] = TileType.Wall;
            tiles[width-1,y] = TileType.Wall;
        }
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