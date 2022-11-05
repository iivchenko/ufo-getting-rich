using Godot;
using System;

public sealed class Player : KinematicBody2D
{
    [Signal]
    public delegate void MovementFinished();

    private Single _time = 1.0f;

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
        tween.TweenProperty(this, "global_position", globalPosition, _time);
        tween.Connect("finished", this, nameof(OnMovementFinished));

        _time -= 0.01f;
    }

    private void OnMovementFinished()
    {
        IsMoving = false;
        EmitSignal(nameof(MovementFinished));
    }
}
