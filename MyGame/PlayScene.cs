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
        string[] tile1 = {
            "############################",
            "#....#.......#.............#",
            "#..................#.......#",
            "#S...........#.............#",
            "##.........................#",
            "#..........................#",
            "#...................#......#",
            "#..........................#",
            "#..........................#",
            "#..........................#",
            "#....................#.....#",
            "#...........#..............#",
            "#..........................#",
            "#..........................#",
            "#..............#...........#",
            "#...#.....................##",
            "#...............#..........#",
            "#.............#............#",
            "#.................#......#$#",
            "############################"
            };
        string[] tile2 = {
            "############################",
            "#...................#......#",
            "#..........................#",
            "#...........#..............#",
            "#...................#......#",
            "#.............#............#",
            "#........#.................#",
            "##.........................#",
            "#.........................##",
            "#..........................#",
            "#.#..........$.............#",
            "#............#...........#.#",
            "#..................#.......#",
            "#....#.....................#",
            "#.....#...#................#",
            "#..........................#",
            "#..........................#",
            "#............#.............#",
            "#..........#.S.#...........#",
            "############################"
            };
        string[] tile3 = {
            "############################",
            "#..#.......................#",
            "#....$..................#..#",
            "#....#.....................#",
            "#.............#............#",
            "#........#..........#......#",
            "#...............#..........#",
            "#.........................##",
            "#.......#..................#",
            "#..................#.......#",
            "#..........................#",
            "#....................#.....#",
            "##...........#.............#",
            "#................#.........#",
            "#........#.................#",
            "#.....................#....#",
            "#.....#....................#",
            "#.............#............#",
            "#....................#..S..#",
            "############################"
            };

        public event GameAction PlayAgainRequested;

        public override void Load()
        {
            _score = 0;
            _gameOver = false;
            int random = new Random().Next(1, 4);
            switch (random)
            {
                case 1:
                    map = new Map(this, tile1.Length, tile1[0].Length);
                    map.Create(tile1);
                    break;
                case 2:
                    map = new Map(this, tile2.Length, tile2[0].Length);
                    map.Create(tile2);
                    break;
                case 3:
                    map = new Map(this, tile3.Length, tile3[0].Length);
                    map.Create(tile3);
                    break;
            }
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
            if (Input.IsKeyDown(ConsoleKey.Spacebar))
            {
                Load();

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
