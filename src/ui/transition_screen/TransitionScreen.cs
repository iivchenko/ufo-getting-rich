using Godot;

public class TransitionScreen : MarginContainer
{
    [Signal]
    public delegate void FadeFinished();

    private ColorRect _background;

    public override void _Ready()
    {
        _background = GetNode<ColorRect>("Background");
    }

    public void Fade(Color color, float time, FadeType fade)
    {
        _background.Color = new Color(color, fade == FadeType.In ? 1 : 0);
        _background.Visible = true;

        var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(_background, "color:a", fade == FadeType.In ? 0.0f : 1.0f, time);
        tween.Connect("finished", this, nameof(OnFinish));
    }

    private void OnFinish()
    {
        EmitSignal(nameof(FadeFinished));
    }

    public enum FadeType
    {
        In, 
        Out
    }
}
