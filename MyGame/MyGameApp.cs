using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

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
            if(Input.IsKeyDown(ConsoleKey.UpArrow))
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

        public override void Draw(ScreenBuffer buffer)
        {
            throw new NotImplementedException();
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

        public override void Draw(ScreenBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
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
}
