using Godot;
using System;

public class Bee : KinematicBody2D
{
    private Sprite _frame1;
    private Sprite _frame2;
    private Timer _timer;

    private Vector2 _target;
    private Vector2 _dead = new Vector2(10, 10);
    private Random _random;
    
    public override void _Ready()
    {
        _frame1 = GetNode<Sprite>("Frame1");
        _frame2 = GetNode<Sprite>("Frame2");
        _timer = GetNode<Timer>("Timer");
        _timer.Connect("timeout", this, nameof(OnTime));

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

        _frame1.FlipH = _frame2.FlipH = direction.x > 0;

        MoveAndSlide(direction * 100);
    }

    private void OnTime()
    {
        _frame1.Visible = !_frame1.Visible;
        _frame2.Visible = !_frame2.Visible;
    }

    private void UpdateTarget()
    {
        var rect = GetViewportRect();

        _target = new Vector2(_random.Next((int)rect.Position.x, (int)rect.End.x), _random.Next((int)rect.Position.y, (int)rect.End.y));
    }
}
