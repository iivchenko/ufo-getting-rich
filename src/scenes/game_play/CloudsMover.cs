using Godot;
using System.Collections.Generic;

public class CloudsMover : Node2D
{
    private Position2D _start;
    private Position2D _end;
    private List<Cloud> _clouds;

    public override void _Ready()
    {
        _start = GetNode<Position2D>("Start");
        _end = GetNode<Position2D>("End");

        _clouds = new List<Cloud>();
        foreach (Node node in GetChildren())
        {
            var cloud = node as Cloud;
            if (cloud != null)
            {
                _clouds.Add(node as Cloud);
            }
        }
    }

    public override void _Process(float delta)
    {
        _clouds.ForEach(cloud =>
        {
            if (cloud.GlobalPosition.x + cloud.Size.x <= _end.GlobalPosition.x)
            {
                cloud.GlobalPosition = new Vector2(_start.GlobalPosition.x + cloud.Size.x, cloud.GlobalPosition.y);
            }
        });
    }
}
