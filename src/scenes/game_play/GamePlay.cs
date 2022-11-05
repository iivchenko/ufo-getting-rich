using Godot;
using System;
using System.Collections.Generic;

public class GamePlay : Node2D
{
    private Node2D _scale;
    private List<Cloud> _clouds;
    private List<Island> _islands;
    private Player _player;
    private IslandTarget _target;
    private Island _island;
    private PathDrawerTool _pathDrawer;
    private List<Position2D> _coinSpawners;
    private Timer _timer;

    private Label _scoreLabel;
    private Label _timeLabel;

    private int _score = 0;
    private int _time = 60;
    private Random _random;

    public override void _Ready()
    {
        _scale = GetNode<Node2D>("Scale");
        _player = GetNode<Player>("Scale/Player");
        _player.Connect(nameof(Player.MovementFinished), this, nameof(OnPlayerFinishedMovement));

        _target = GetNode<IslandTarget>("Scale/Islands/Target");
        _target.Visible = false;

        _clouds = new List<Cloud>();
        foreach (Node node in GetNode<Node2D>("Scale/Clouds").GetChildren())
        {
            _clouds.Add(node as Cloud);
        }

        _islands = new List<Island>();
        foreach (Node node in GetNode<Node2D>("Scale/Islands").GetChildren())
        {
            var island = node as Island;

            if (island != null)
            {
                _islands.Add(island);

                island.Connect(nameof(Island.MouseClicked), this, nameof(OnIslandClick));
                island.Connect(nameof(Island.MouseEntered), this, nameof(OnIslandMouseEntered));
                island.Connect(nameof(Island.MouseExited), this, nameof(OnIslandMouseExited));
            }
        }

        _coinSpawners = new List<Position2D>();
        foreach(var node in GetNode<Node2D>("Scale/CoinSpawners").GetChildren())
        {
            var spawner = node as Position2D;

            if (spawner != null)
            {
                _coinSpawners.Add(spawner);
            }
        }

        _random = new Random();

        _scoreLabel = GetNode<Label>("Ui/VBoxContainer/Score");
        _timeLabel = GetNode<Label>("Ui/VBoxContainer/Time");

        var startCoin = _scale.GetNode<Coin>("Coin");
        startCoin.Connect(nameof(Coin.UfoCollided), this, nameof(OnPlayerCollidedCoin));

        _timer = GetNode<Timer>("Timer");
        _timer.Connect("timeout", this, nameof(OnTime));
    }

    public override void _Process(float delta)
    {
        _clouds.ForEach(cloud =>
            {
                if (cloud.GlobalPosition.x + cloud.Size.x <= 0)
                {
                    cloud.GlobalPosition = new Vector2(GetViewport().Size.x + 25, cloud.GlobalPosition.y);
                }                    
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

    private void OnPlayerCollidedCoin()
    {
        if (_timer.IsStopped())
        {
            _timer.Start();
        }

        _score += 1;
        _scoreLabel.Text = $"Score: {_score}";

        var position = _coinSpawners[_random.Next(_coinSpawners.Count - 1)].GlobalPosition;

        var coin = Coin.Create();
        coin.Name = "Coin";
        coin.GlobalPosition = position;
        coin.Connect(nameof(Coin.UfoCollided), this, nameof(OnPlayerCollidedCoin));

        _scale.AddChild(coin);
    }

    private void OnTime()
    {
        if (_time == 0)
        {
            // Stop Game
        }

        _time--;
        _timeLabel.Text = $"Time: {_time}s";
    }
}
