using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.MyGame
{
    public abstract class GameApp
    {
        protected GameApp(int width, int height);

        protected ScreenBuffer Buffer { get; }

        public event GameAction GameStarted;
        public event GameAction GameStopped;

        public void Run();
        protected void Quit();

        protected abstract void Initialize();
        protected abstract void Update(float deltaTime);
        protected abstract void Draw();
    }
}
