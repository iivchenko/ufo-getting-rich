using Godot;

public class Island06 : IslandBase
{
    public override void _Ready()
    {
        base._Ready();

        TweenSlime("Slime1");
        TweenSlime("Slime2");
    }

    public override void _Process(float delta)
    {
        base._Process(delta);        
    }

    private void TweenSlime(string name)
    {
        var slime = GetNode<Sprite>(name);
        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(slime.Material, "shader_param/deformation", new Vector2(0.1f, 0f), 0.7f);
        tween.Chain().TweenProperty(slime.Material, "shader_param/deformation", new Vector2(-0.1f, 0f), 0.7f);
        tween.SetLoops();
        tween.Play();
    }
}
