using Framework.Engine;
using Framework.MyGame;
using System;

public class MyGame : GameApp
{
    private readonly SceneManager<Scene> _scenes;

    public MyGame() : base(40, 30)
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