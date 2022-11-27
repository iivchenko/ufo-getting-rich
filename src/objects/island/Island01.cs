using Godot;

public class Island01 : IslandBase
{
    private float _pixels = 100;

    public override void _Ready()
    {
        base._Ready();

        var grassMan = GetNode<Sprite>("GrassMan");        

        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(grassMan.Material, "shader_param/deformation", new Vector2(0, 0.2f), 0.7f);
        tween.Chain().TweenProperty(grassMan.Material, "shader_param/deformation", new Vector2(0, 0f), 0.7f);

        tween.SetLoops();
        tween.Play();

        var bugMan = GetNode<Sprite>("BugMan");

        var tween2 = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween2.TweenProperty(bugMan.Material, "shader_param/deformation", new Vector2(0.1f, 0f), 0.7f);
        tween2.Chain().TweenProperty(bugMan.Material, "shader_param/deformation", new Vector2(-0.1f, 0f), 0.7f);
        tween2.SetLoops();
        tween2.Play();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        _pixels -= delta * 10;

        
        
    }
}
