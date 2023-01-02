using Godot;
using System;
using System.Collections.Generic;

public class GamePlay : Node2D
{
    public static string Path = "res://scenes/game_play/game_play.tscn";
    private Camera2D _camera;
    private Node2D _scale;
    private List<IslandBase> _islands;
    private Player _player;
    private IslandTarget _target;
    private IslandBase _island;
    private Node2D _spawner;
    private List<Position2D> _coinSpawners;
    private Timer _timer;

    // Game Play UI
    private Control _gamePlayUi;
    private CoinCountLabel _coinsLbl;
    private TimeCcountDownLabel _timeLabel;
    private Control _gameOver;
    private Label _finalScoreLabel;
    private Button _restartBtn;
    private Button _exitBtn;

    private TransitionScreen _transistor;

    private int _score = 0;
    private int _time = 5;// 60;
    private Random _random;

    public override void _Ready()
    {
        _camera = GetNode<Camera2D>("Camera2D");
        _gamePlayUi = GetNode<Control>("UI/GamePlayUi");

        ScaleScene();
        GetTree().Root.Connect("size_changed", this, nameof(ScaleScene));

        _scale = GetNode<Node2D>("Scale");

        _player = GetNode<Player>("Scale/Player");
        _player.Connect(nameof(Player.MovementFinished), this, nameof(OnPlayerFinishedMovement));

        _target = GetNode<IslandTarget>("Scale/Islands/Target");
        _target.Visible = false;

        _islands = new List<IslandBase>();
        foreach (Node node in GetNode<Node2D>("Scale/Islands").GetChildren())
        {
            var island = node as IslandBase;

            if (island != null)
            {
                _islands.Add(island);

                island.Connect(nameof(IslandBase.MouseClicked), this, nameof(OnIslandClick));
                island.Connect(nameof(IslandBase.MouseEntered), this, nameof(OnIslandMouseEntered));
                island.Connect(nameof(IslandBase.MouseExited), this, nameof(OnIslandMouseExited));
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
        _coinsLbl = GetNode<CoinCountLabel>("UI/GamePlayUi/Coins");
        _timeLabel = GetNode<TimeCcountDownLabel>("UI/GamePlayUi/Time");
        _timeLabel.Time = _time;
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

        _camera.GlobalPosition = GetNode<Node2D>("Scale/Coin").GlobalPosition;
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

    private void OnIslandClick(IslandBase island)
    {
        if (!_player.IsMoving)
        {
            _player.Move(island.GlobalPosition);
        }
    }

    private void OnIslandMouseEntered(IslandBase island)
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

    private void OnIslandMouseExited(IslandBase _)
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
        _coinsLbl.Value = _score;

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

            var restartBtn = _gameOver.GetNode<Button>("%ReStartBtn");
            var exitBtn = _gameOver.GetNode<Button>("%ExitBtn");

            var tween = CreateTween();
            tween.TweenProperty(restartBtn, "disabled", false, 2.0f);
            tween.Parallel().TweenProperty(exitBtn, "disabled", false, 2.0f);
            tween.Play();

            return;
        }

        _time--;
        _timeLabel.Time = _time;
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

    private void ScaleScene()
    {
        var size = GetViewportRect();        
        
        _camera.Zoom = new Vector2(1080.0f / size.Size.y, 1080.0f / size.Size.y);
        _gamePlayUi.RectScale = new Vector2(size.Size.y / 1080.0f, size.Size.y / 1080.0f);
    }
}
