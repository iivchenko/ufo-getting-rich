using Godot;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

[Tool]
public class PathDrawerTool : Node2D
{
    private List<Node2D> _islands;

    [Export]
    public bool Switch { get; set; }

    public override void _Ready()
    {
        base._Ready();

        _islands = new List<Node2D>();

        foreach (Node2D node in this.GetChildren())
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
        if (Engine.EditorHint)
        {
            foreach (var from in _islands)
            {
                foreach (var to in _islands.Where(x => x != from))
                {
                    DrawLine(from.Position, to.Position, new Color(255, 0, 0), 1);
                }
            }
        }
    }
}
