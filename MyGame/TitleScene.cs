using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.MyGame
{
    public class TitleScene : Scene
    {
        public event GameAction StartRequested;

        public override void Load()
        {
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(ConsoleKey.Enter))
            {
                StartRequested?.Invoke();
            }
        }

        public override void Draw(ScreenBuffer buffer)
        {
            buffer.WriteTextCentered(5, "ICD SLICE PUZZLE", ConsoleColor.Blue);
            buffer.WriteTextCentered(8, "Press ENTER to Start", ConsoleColor.Gray);
            buffer.WriteTextCentered(10, "Press ESC to Quit", ConsoleColor.DarkGray);

        }

        public override void Unload()
        {
        }
    }
}
