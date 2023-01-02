using Godot;

public class TransitionScreen : MarginContainer
{
    [Signal]
    public delegate void FadeFinished();

    private ColorRect _background;
    private int _bus;

    public override void _Ready()
    {
        _background = GetNode<ColorRect>("Background");
        _bus = AudioServer.GetBusIndex("Master");
    }

    public float Volume
    {
        get => GD.Db2Linear(AudioServer.GetBusVolumeDb(_bus));
        set => AudioServer.SetBusVolumeDb(_bus, GD.Linear2Db(value));
    }

    public void Fade(Color color, float time, FadeType fade)
    {
        _background.Color = new Color(color, fade == FadeType.In ? 1 : 0);
        _background.Visible = true;

        var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(_background, "color:a", fade == FadeType.In ? 0.0f : 1.0f, time);
        tween.Parallel().TweenProperty(this, nameof(Volume), fade == FadeType.In ? 1.0f : 0.0f, time);
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
