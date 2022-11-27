using Godot;
using System;

public class YellowUfo : KinematicBody2D
{
    private Sprite _ufo;

    private Vector2 _target;
    private Vector2 _dead = new Vector2(10, 10);
    private Random _random;

    public override void _Ready()
    {
        _ufo = GetNode<Sprite>("Sprite");

        _random = new Random();

        UpdateTarget();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if ((GlobalPosition - _target).Abs() < _dead)
        {
            UpdateTarget();
        }
        var direction = (_target - GlobalPosition).Normalized();

        _ufo.FlipH = _ufo.FlipH = direction.x > 0;

        MoveAndSlide(direction * 100);
    }

    private void UpdateTarget()
    {
        var rect = GetViewportRect();

        _target = new Vector2(_random.Next((int)rect.Position.x, (int)rect.End.x), _random.Next((int)rect.Position.y, (int)rect.End.y));
    }
}
