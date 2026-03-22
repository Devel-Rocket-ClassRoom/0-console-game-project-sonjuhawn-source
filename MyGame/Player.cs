using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

class Player : GameObject
{
    private int x, y;
    private int dirX = 0;
    private int dirY = 0;
    private int count = 0;
    public int Count => count;
    private float moveTimer = 0f;
    private float moveDelay = 0.01f;
    private bool moving = false;
    public bool isGoal { get; private set; } = false;
    Map map;
    public Player(Scene scene, Map map, int x, int y) : base(scene)
    {
        this.x = x;
        this.y = y;
        this.map = map;
    }

    private void StartMove(int dx, int dy)
    {
        dirX = dx;
        dirY = dy;
        moving = true;
    }
    private void MoveStep()
    {
        int nx = x + dirX;
        int ny = y + dirY;

        if (map.IsWall(nx, ny))
        {
            moving = false;
            return;
        }
        x = nx;
        y = ny;
        if (map.IsGoal(nx, ny))
        {
            isGoal = true;
        }
    }
    public override void Update(float deltaTime)
    {
        if (!moving)
        {
            if (Input.IsKeyDown(ConsoleKey.UpArrow))
            {
                StartMove(0, -1);
            }
            if (Input.IsKeyDown(ConsoleKey.DownArrow))
            {
                StartMove(0, 1);
            }
            if (Input.IsKeyDown(ConsoleKey.RightArrow))
            {
                StartMove(1, 0);
            }
            if (Input.IsKeyDown(ConsoleKey.LeftArrow))
            {
                StartMove(-1, 0);
            }
        }
        if (moving)
        {
            moveTimer += deltaTime;

            if (moveTimer >= moveDelay)
            {
                moveTimer = 0f;
                MoveStep();
            }
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(x + 3, y + 5, '@', ConsoleColor.White);
    }
}
