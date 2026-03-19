using Framework.Engine;
using System;

namespace Framework.MyGame
{
    class Player : GameObject
    {
        private int x, y;
        public Player(Scene scene, int x, int y) : base(scene)
        {
            this.x = x;
            this.y = y;
        }

        public override void Update(float deltaTime)
        {
            if (/*움직이는 동안에는 입력해도 움직일수 없음*/true)
            {
                if (Input.IsKeyDown(ConsoleKey.UpArrow))
                {
                    y--;
                }
                if (Input.IsKeyDown(ConsoleKey.DownArrow))
                {
                    y++;
                }
                if (Input.IsKeyDown(ConsoleKey.RightArrow))
                {
                    x++;
                }
                if (Input.IsKeyDown(ConsoleKey.LeftArrow))
                {
                    x--;
                }
            }
        }

        public override void Draw(ScreenBuffer buffer)
        {
            buffer.SetCell(x, y, '@', ConsoleColor.White);
        }
    }

    public class Wall : GameObject
    {
        private int x, y;

        public Wall(Scene scene, int x, int y) : base(scene)
        {
            this.x = x;
            this.y = y;
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
        public override void Draw(ScreenBuffer buffer)
        {
            buffer.SetCell(x, y, '#', ConsoleColor.Cyan);
        }
    }
    class Goal : GameObject
    {
        private int x, y;

        public Goal(Scene scene, int x, int y) : base(scene)
        {
            this.x = x;
            this.y = y;
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(ScreenBuffer buffer)
        {
            buffer.SetCell(x, y, '$', ConsoleColor.Magenta);
        }
    }

    class Map
    {
        Player player;
        Goal goal;
        Wall[] wall;

        private int width;
        private int height;

        public Map(int  width, int height)
        {
            this.width = width;
            this.height = height;
        }


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
}
