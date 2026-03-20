using Framework.Engine;
using System;

namespace Framework.MyGame
{

    class PlayScene : Scene
    {
        private int _score;
        private bool _gameOver;
        private float _timer = 0f;
        private bool _timerstart = false;
        private Map map;

        public event GameAction PlayAgainRequested;

        public override void Load()
        {
            _score = 0;
            _gameOver = false;

            string[] tile1 = {
            "############################",
            "#............#.............#",
            "#..........................#",
            "#S...........#.............#",
            "##.........................#",
            "#................#.........#",
            "#...................#......#",
            "#..........................#",
            "#..........................#",
            "#..........................#",
            "#....................#.....#",
            "#...........#..............#",
            "#..........................#",
            "#..........................#",
            "#..........................#",
            "#.........................##",
            "#...............#..........#",
            "#..........................#",
            "#..........................#",
            "############################"
            };

            map = new Map(this, tile1.Length, tile1[0].Length);
            map.Create(tile1);
        }

        public override void Update(float deltaTime)
        {
            if ((Input.IsKeyDown(ConsoleKey.UpArrow) || Input.IsKeyDown(ConsoleKey.DownArrow) || Input.IsKeyDown(ConsoleKey.RightArrow) || Input.IsKeyDown(ConsoleKey.LeftArrow)))
            {
                if (!_timerstart)
                {
                    _timerstart = true;
                    _timer = 0f;
                }
            }
            if (_timerstart && !_gameOver)
            {
                _timer += deltaTime;
                _score = (int)_timer;
            }
            if (map.player.isGoal)
            {
                _gameOver = true;
            }
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


            buffer.WriteText(5, 2, "Time: " + _score, ConsoleColor.White);

            if (_gameOver)
            {
                buffer.WriteTextCentered(8,  "Clear! Time: " + _score, ConsoleColor.Yellow);
                buffer.WriteTextCentered(9,  "Press ENTER To Play", ConsoleColor.Yellow);
                buffer.WriteTextCentered(10, " Or End ESC", ConsoleColor.Yellow);
            }
        }

        public override void Unload()
        {
        }
    }
}
