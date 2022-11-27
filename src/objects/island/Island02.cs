using Godot;

public class Island02 : IslandBase
{
    public override void _Ready()
    {
        base._Ready();

        var grassMan = GetNode<Sprite>("Ground/GrassMan");        

        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(grassMan.Material, "shader_param/deformation", new Vector2(0, 0.2f), 0.7f);
        tween.Chain().TweenProperty(grassMan.Material, "shader_param/deformation", new Vector2(0, 0f), 0.7f);

        tween.SetLoops();
        tween.Play();       
    }

    public override void _Process(float delta)
    {
        base._Process(delta);        
    }
}
