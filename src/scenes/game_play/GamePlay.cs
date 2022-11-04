using Godot;
using System.Collections.Generic;

public class GamePlay : Node2D
{
    private List<Sprite> _clouds;
    private Player _player;
    private IslandTarget _target;
    private Island _island;

    public override void _Ready()
    {
        _player = GetNode<Player>("Scale/Player");
        _player.Connect(nameof(Player.MovementFinished), this, nameof(OnPlayerFinishedMovement));

        _target = GetNode<IslandTarget>("Scale/Islands/Target");
        _target.Visible = false;

        _clouds = new List<Sprite>();
        foreach (Node node in GetNode<Node2D>("Scale/Clouds").GetChildren())
        {
            _clouds.Add(node as Sprite);
        }

        foreach (Node node in GetNode<Node2D>("Scale/Islands").GetChildren())
        {
            var island = node as Island;

            if (island != null)
            {
                island.Connect(nameof(Island.MouseClicked), this, nameof(OnIslandClick));
                island.Connect(nameof(Island.MouseEntered), this, nameof(OnIslandMouseEntered));
                island.Connect(nameof(Island.MouseExited), this, nameof(OnIslandMouseExited));
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

    private void OnPlayerFinishedMovement()
    {
        if (_island != null)
        {
            _target.GlobalPosition = _island.GlobalPosition;
            _target.Start();
            _island = null;
        }
    }

    private void OnIslandClick(Island island)
    {
        if (!_player.IsMoving)
        {
            _player.Move(island.GlobalPosition);
        }
    }

    private void OnIslandMouseEntered(Island island)
    {
        if (_player.IsMoving)
        {
            _island = island;
        }
        else
        {
            _target.GlobalPosition = island.GlobalPosition;
            _target.Start();
        }
    }

    private void OnIslandMouseExited(Island _)
    {
        if (_player.IsMoving)
        {
            _island = null;
        }
        else
        {
            _target.Stop();
        }
    }
}
