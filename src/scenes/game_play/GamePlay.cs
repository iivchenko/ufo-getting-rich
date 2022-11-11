using Godot;
using System;
using System.Collections.Generic;

public class GamePlay : Node2D
{
    public static string Path = "res://scenes/game_play/game_play.tscn";
    private Camera2D _camera;
    private Node2D _scale;
    private List<Island> _islands;
    private Player _player;
    private IslandTarget _target;
    private Island _island;
    private Node2D _spawner;
    private List<Position2D> _coinSpawners;
    private Timer _timer;

    // Game Play UI
    private Label _scoreLabel;
    private Label _timeLabel;
    private Control _gameOver;
    private Label _finalScoreLabel;
    private Button _restartBtn;
    private Button _exitBtn;

    private TransitionScreen _transistor;

    private int _score = 0;
    private int _time = 60;
    private Random _random;

    public override void _Ready()
    {
        var size = GetViewportRect();
        _camera = GetNode<Camera2D>("Camera2D");
        _camera.GlobalPosition = GetNode<Node2D>("Scale/Coin").GlobalPosition;
        _camera.Zoom = new Vector2(1080.0f / size.Size.y, 1080.0f / size.Size.y);

        _scale = GetNode<Node2D>("Scale");

        _player = GetNode<Player>("Scale/Player");
        _player.Connect(nameof(Player.MovementFinished), this, nameof(OnPlayerFinishedMovement));

        _target = GetNode<IslandTarget>("Scale/Islands/Target");
        _target.Visible = false;

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

        _spawner = GetNode<Node2D>("Scale/CoinSpawners");
        _coinSpawners = new List<Position2D>();
        foreach(var node in _spawner.GetChildren())
        {
            var spawner = node as Position2D;

            if (spawner != null)
            {
                _coinSpawners.Add(spawner);
            }
        }

        _random = new Random();

        _scoreLabel = GetNode<Label>("UI/GamePlayUi/Score");
        _timeLabel = GetNode<Label>("UI/GamePlayUi/Time");
        _gameOver = GetNode<Control>("UI/GameOver");
        _finalScoreLabel = GetNode<Label>("UI/GameOver/Center/VBox/FinalScoreLabel");
        _restartBtn = GetNode<Button>("UI/GameOver/Center/VBox/ReStartBtn");
        _restartBtn.Connect("pressed", this, nameof(OnRestartClicked));
        _exitBtn = GetNode<Button>("UI/GameOver/Center/VBox/ExitBtn");
        _exitBtn.Connect("pressed", this, nameof(OnExitClicked));
        var startCoin = _scale.GetNode<Coin>("Coin");
        startCoin.Connect(nameof(Coin.UfoCollided), this, nameof(OnPlayerCollidedCoin));

        _timer = GetNode<Timer>("Timer");
        _timer.Connect("timeout", this, nameof(OnTime));

        _transistor = GetNode<TransitionScreen>("Transition/Transistor");
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnFadeInFinished));
        _transistor.Fade(new Color(0, 0, 0, 255), 1.0f, TransitionScreen.FadeType.In);
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

        var position = _coinSpawners[_random.Next(_coinSpawners.Count - 1)].Position;

        var coin = Coin.Create();
        coin.Name = "Coin";
        coin.Position = position;
        coin.Connect(nameof(Coin.UfoCollided), this, nameof(OnPlayerCollidedCoin));

        _spawner.AddChild(coin);
    }

    private void OnTime()
    {
        if (_time == 0)
        {
            _timer.Stop();
            _gameOver.Visible = true;
            _finalScoreLabel.Text = $"Your score is: {_score}";
            return;
        }

        _time--;
        _timeLabel.Text = $"Time: {_time} s";
    }

    private void OnRestartClicked()
    {
        GetTree().ReloadCurrentScene();
    }

    private void OnExitClicked()
    {
        _transistor.Connect(nameof(TransitionScreen.FadeFinished), this, nameof(OnExitFadeOutFinished));
        _transistor.Visible = true;
        _transistor.Fade(new Color(0, 0, 0, 255), 1.0f, TransitionScreen.FadeType.Out);
    }

    private void OnExitFadeOutFinished()
    {
        GetTree().ChangeScene(MainMenu.Path);
    }

    private void OnFadeInFinished()
    {
        _transistor.Visible = false;
    }
}
