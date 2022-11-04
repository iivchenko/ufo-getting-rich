using Godot;

public class IslandTarget : Sprite
{
    private SceneTreeTween _tween;

    public override void _Ready()
    {
        base._Ready();

        ResetScale();
    }

    public void Start()
    {
        Visible = true;

        _tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        _tween.TweenProperty(this, "scale", new Vector2(2.5f, 2.5f), 0.2f);
        _tween.Chain().TweenProperty(this, "scale", new Vector2(1f, 1f), 0.7f);
        _tween.Play();
    }

    public void Stop()
    {
        Visible = false;

        _tween.Stop();
        ResetScale();
    }

    private void ResetScale()
    {
        Scale = new Vector2(0.25f, 0.25f);
    }
}
