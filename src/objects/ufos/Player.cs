using Godot;

public class Player : Sprite
{
    [Signal]
    public delegate void MovementFinished();

    public bool IsMoving { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        IsMoving = false;
    }

    public void Move(Vector2 globalPosition)
    {
        if (IsMoving)
            return;
        IsMoving = true;
        var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(this, "global_position", globalPosition, 1.0f);
        tween.Connect("finished", this, nameof(OnStop));
        tween.Connect("finished", this, nameof(OnMovementFinished));
    }

    private void OnStop()
    {
        IsMoving = false;
    }

    private void OnMovementFinished()
    {
        EmitSignal(nameof(MovementFinished));
    }
}
