using Godot;

public class MainMenu : CenterContainer
{
    private Button _startBtn;
    private Button _exitBtn;

    public override void _Ready()
    {
        _startBtn = GetNode<Button>("VBoxContainer/StartBtn");
        _startBtn.Connect("pressed", this, nameof(OnStartBtnClicked));

        _exitBtn = GetNode<Button>("VBoxContainer/ExitBtn");
        _exitBtn.Connect("pressed", this, nameof(OnExitBtnClicked));
    }

    private void OnStartBtnClicked()
    {
        GetTree().ChangeScene(GamePlay.Path);
    }

    private void OnExitBtnClicked()
    {
        GetTree().Quit();
    }
}
