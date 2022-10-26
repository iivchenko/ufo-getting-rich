using Godot;
using System.Collections.Generic;

public class GamePlay : Control
{
    private List<Sprite> _clouds;
    private Node2D _player;

    public override void _Ready()
    {
        _player = GetNode<Node2D>("Scale/Player");
        _clouds = new List<Sprite>();

        foreach(Node node in GetNode<Node2D>("Scale/Clouds").GetChildren())
        {
            _clouds.Add(node as Sprite);
        }
    }

    public override void _Process(float delta)
    {
        _clouds.ForEach(cloud => cloud.Position -= Vector2.Right);
        
        _clouds.ForEach(cloud => 
            { 
                if(cloud.GlobalPosition.x + cloud.RegionRect.Size.x <= 0) 
                    cloud.GlobalPosition = new Vector2(GetViewport().Size.x + 10, cloud.GlobalPosition.y);
            });
    }

    public override void _Input (InputEvent @event)
    {
        if (@event is InputEventMouseButton mouse && mouse.IsPressed() && mouse.ButtonIndex == (int)ButtonList.Left)
        {
            var tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.InOut);
            tween.TweenProperty(_player, "global_position", GetGlobalMousePosition(), 1.0f);
        }
    }
}
