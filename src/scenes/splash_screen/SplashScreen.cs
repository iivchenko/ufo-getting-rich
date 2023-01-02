using Godot;

public class SplashScreen : Control
{
    private TransitionScreen _transistor;

    public override void _Ready()
    {
        _transistor = GetNode<TransitionScreen>("Transistor");
        _transistor.Volume = 0;
        _transistor.Fade(new Color(0, 0, 0, 255), 2.0f, TransitionScreen.FadeType.In);
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnFadeInFinish));
    }

    private void OnFadeInFinish()
    {
        GetNode<AudioStreamPlayer>("CatMeow").Play();
        _transistor.Disconnect(nameof(TransitionScreen.FadeFinished), this, nameof(OnFadeInFinish));

        _transistor.Fade(new Color(0, 0, 0, 0), 1.0f, TransitionScreen.FadeType.Out);
        
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnFadeOutFinish));
    }

    private void OnFadeOutFinish()
    {
        GetTree().ChangeScene(MainMenu.Path);
    }
}
