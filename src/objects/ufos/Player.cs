using Godot;
public class Player : Sprite
{
    private bool _isMoving;

    public override void _Ready()
    {
        base._Ready();
        _isMoving = false;
    }

    public void Move(Vector2 globalPosition)
    {
        if (_isMoving)
            return;
        _isMoving = true;
        var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(this, "global_position", globalPosition, 1.0f);
        tween.Connect("finished", this, nameof(OnStop));
    }

    private void OnStop()
    {
        _isMoving = false;
    }
}
