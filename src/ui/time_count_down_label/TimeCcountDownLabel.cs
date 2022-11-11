using Godot;

public class TimeCcountDownLabel : MarginContainer
{
    private TextureRect _first;
    private TextureRect _second;

    private int _time;

    [Export]
    public int Time 
    { 
        get => _time; 
        set
        {
            _time = value;

            _first.Visible = !_first.Visible;
            _second.Visible = !_second.Visible;

            Deactivate();


            if (value < 10)
            {
                GetNode<TextureRect>($"HBox/Count1/Count{value}").Visible = true;
            }
            else if (value < 100)
            {
                var a = value / 10;
                var b = value % 10;
                GetNode<TextureRect>($"HBox/Count1/Count{a}").Visible = true;
                GetNode<TextureRect>($"HBox/Count2/Count{b}").Visible = true;
            }
            else if (value < 1000)
            {
                var a = value / 100;
                var b = (value / 10) % 10;
                var c = value % 10;

                GetNode<TextureRect>($"HBox/Count1/Count{a}").Visible = true;
                GetNode<TextureRect>($"HBox/Count2/Count{b}").Visible = true;
                GetNode<TextureRect>($"HBox/Count3/Count{c}").Visible = true;
            }
            else
            {
                GetNode<TextureRect>($"HBox/Count1/Count0").Visible = true;
                GetNode<TextureRect>($"HBox/Count2/Count0").Visible = true;
                GetNode<TextureRect>($"HBox/Count3/Count0").Visible = true;
            }
        }
    }

    public override void _Ready()
    {
        _first = GetNode<TextureRect>("HBox/First");
        _second = GetNode<TextureRect>("HBox/Second");
    }

    private void Deactivate()
    {
        foreach (Control count in GetNode<Control>("HBox/Count1").GetChildren())
        {
            count.Visible = false;
        }

        foreach (Control count in GetNode<Control>("HBox/Count2").GetChildren())
        {
            count.Visible = false;
        }

        foreach (Control count in GetNode<Control>("HBox/Count3").GetChildren())
        {
            count.Visible = false;
        }
    }
}
