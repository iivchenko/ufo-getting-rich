using Godot;

public sealed class MainMenu : MarginContainer
{
    public static string Path = "res://scenes/main_menu/main_menu_scene.tscn";

    private Button _startBtn;
    private Button _creditsBtn;
    private Button _exitBtn;
    private Button _exitCreditsBtn;
    private TransitionScreen _transistor;

    public override void _Ready()
    {
        _transistor = GetNode<TransitionScreen>("%Transistor");
        _transistor.Fade(new Color(0, 0, 0, 255), 1.0f, TransitionScreen.FadeType.In);
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnFadeInFinished));

        _startBtn = GetNode<Control>("MainMenu").GetNode<Button>("%StartBtn");
        _startBtn.Connect("pressed", this, nameof(OnStartBtnClicked));

        _creditsBtn = GetNode<Control>("MainMenu").GetNode<Button>("%CreditsBtn");
        _creditsBtn.Connect("pressed", this, nameof(OnCreditsBtnClicked));

        _exitBtn = GetNode<Control>("MainMenu").GetNode<Button>("%ExitBtn");
        _exitBtn.Connect("pressed", this, nameof(OnExitBtnClicked));

        _exitCreditsBtn = GetNode<Control>("Credits").GetNode<Button>("%ExitCreditsBtn");
        _exitCreditsBtn.Connect("pressed", this, nameof(OnExitCreditsBtnClicked));

        var rect = GetViewportRect();
        var center = new Vector2(rect.End.x / 2, rect.End.y / 2);

        foreach (Node2D ufo in GetNode<Node2D>("Ufos").GetChildren())
        {
            ufo.GlobalPosition = center;
        }

        ScaleScene();

        GetTree().Root.Connect("size_changed", this, nameof(ScaleScene));
    }

    private void OnFadeInFinished()
    {
        _transistor.Visible = false;
    }

    private void OnStartBtnClicked()
    {
        _transistor.Visible = true;
        _transistor.Fade(new Color(0, 0, 0, 255), 1.0f, TransitionScreen.FadeType.Out);
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnStartFadeOutFinish));
    }

    private void OnCreditsBtnClicked()
    {
        GetNode<Control>("MainMenu").Visible = false;
        GetNode<Control>("Credits").Visible = true;
    }

    private void OnStartFadeOutFinish()
    {
        GetTree().ChangeScene(GamePlay.Path);
    }

    private void OnExitBtnClicked()
    {
        _transistor.Visible = true;
        _transistor.Fade(new Color(0, 0, 0, 255), 0.5f, TransitionScreen.FadeType.Out);
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnExitFadeOutFinish));        
    }

    private void OnExitFadeOutFinish()
    {
        GetTree().Quit();
    }

    private void OnExitCreditsBtnClicked()
    {
        GetNode<Control>("MainMenu").Visible = true;
        GetNode<Control>("Credits").Visible = false;
    }

    private void ScaleScene()
    {
        //var size = GetViewportRect();
        //RectScale = new Vector2(size.Size.y / 1080.0f, size.Size.y / 1080.0f);
    }
}
