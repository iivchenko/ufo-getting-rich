using Godot;
using System.Collections.Generic;

public class GamePlay : Node2D
{
    private List<Sprite> _clouds;
    private Player _player;

    public override void _Ready()
    {
        _player = GetNode<Player>("Scale/Player");
        _clouds = new List<Sprite>();

        foreach(Node node in GetNode<Node2D>("Scale/Clouds").GetChildren())
        {
            _clouds.Add(node as Sprite);
        }

        foreach (Node node in GetNode<Node2D>("Scale/Islands").GetChildren())
        {
            var island = node as Island;

            if (island != null)
            {
                island.Connect("OnClick", this, nameof(OnIslandClick));
            }
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

    public void OnIslandClick(Island island)
    {
        _player.Move(island.GlobalPosition);
    }
}
