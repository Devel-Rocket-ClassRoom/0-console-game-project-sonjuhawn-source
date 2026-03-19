using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.MyGame
{

    class PlayScene : Scene
    {
        private int _score;
        private bool _gameOver;
        private Map map;

        public event GameAction PlayAgainRequested;

        public override void Load()
        {
            _score = 0;
            _gameOver = false;

            string[] tile1 = {
            "##################",
            "#S...............#",
            "#................#",
            "#.......#........#",
            "#................#",
            "#....#...........#",
            "#...............##",
            "#.....#.....$....#",
            "#...#............#",
            "##################",
            };

            map = new Map(this,tile1.Length, tile1[0].Length);
            map.Create(tile1);
        }

        public override void Update(float deltaTime)
        {
            if (_gameOver)
            {
                if (Input.IsKeyDown(ConsoleKey.Enter))
                {
                    PlayAgainRequested?.Invoke();
                }
                return;
            }

            // 게임 로직...
            map.player.Update(deltaTime);
            
        }

        public override void Draw(ScreenBuffer buffer)
        {
            map.Draw(buffer);


            buffer.WriteText(5, 2, "Score: " + _score, ConsoleColor.White);

            if (_gameOver)
            {
                buffer.WriteText(10, 10, "Game Over! Press ENTER", ConsoleColor.Red);
            }
        }

        public override void Unload()
        {
        }
    }
}
