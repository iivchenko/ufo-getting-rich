using Godot;

public class IslandTarget : Sprite
{
    private SceneTreeTween _tween;
    //public override void _Ready()
    //{
    //    _tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
    //    _tween.TweenProperty(this, "scale", new Vector2(2, 2), 0.2f);
    //    _tween.Chain().TweenProperty(this, "scale", new Vector2(1f, 1f), 0.7f);
    //    _tween.Stop();
    //}

    public void Start()
    {
        _tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        _tween.TweenProperty(this, "scale", new Vector2(2, 2), 0.2f);
        _tween.Chain().TweenProperty(this, "scale", new Vector2(1f, 1f), 0.7f);
        _tween.Play();
    }

    public void Stop()
    {
        _tween.Stop();
    }
}
