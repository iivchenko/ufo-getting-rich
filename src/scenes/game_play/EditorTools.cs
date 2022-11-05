using Godot;
using System.Collections.Generic;
using System.Linq;

[Tool]
public class EditorTools : Node2D
{
    private List<Node2D> _islands;

    [Export]
    public ToggleValues Toggle { get; set; } = ToggleValues.Off;

    public override void _Ready()
    {
        base._Ready();

        _islands = new List<Node2D>();

        foreach (Node2D node in GetParent().FindNode("Islands").GetChildren())
        {
            if (node.Name.StartsWith("Island"))
            {
                _islands.Add(node);
            }
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        Update();
    }

    public override void _Draw()
    {
        base._Draw();

        var draw = false;

        switch (Toggle)
        {
            case ToggleValues.Off:
                draw = false;
                break;

            case ToggleValues.Editor:   
                draw = Engine.EditorHint;
                break;

            case ToggleValues.Game:
                draw = true;
                break;
        }

        if (draw)
        {
            foreach (var from in _islands)
            {
                foreach (var to in _islands.Where(x => x != from))
                {
                    DrawLine(from.GlobalPosition, to.GlobalPosition, new Color(255, 0, 0), 1);
                }
            }
        }
    }

    public enum ToggleValues
    {
        Off, 
        Editor, 
        Game
    }
}
