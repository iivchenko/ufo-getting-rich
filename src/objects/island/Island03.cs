using Godot;

public class Island03 : IslandBase
{
    public override void _Ready()
    {
        base._Ready();

        Tweening(GetNode<Sprite>("Ground/Sprite7"), 2.0f);
        Tweening(GetNode<Sprite>("Ground/Sprite8"), 2.1f);
        Tweening(GetNode<Sprite>("Ground/Sprite4"), 2.2f);
        Tweening(GetNode<Sprite>("Ground/Sprite9"), 2.0f);
        Tweening(GetNode<Sprite>("Ground/Sprite5"), 2.1f);
        //Tweening(GetNode<Sprite>("Ground/Sprite1"), 2.2f);
        Tweening(GetNode<Sprite>("Ground/Sprite6"), 2.0f);
        //Tweening(GetNode<Sprite>("Ground/Sprite2"), 2.1f);
        //Tweening(GetNode<Sprite>("Ground/Sprite3"), 2.2f);

        var alien = GetNode<Sprite>("Ground/Sprite3/Alien");
        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(alien.Material, "shader_param/deformation", new Vector2(0, 0.1f), 0.5f);
        tween.Chain().TweenProperty(alien.Material, "shader_param/deformation", new Vector2(0, 0f), 0.5f);
        tween.SetLoops();
        tween.Play();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);        
    }

    private void Tweening(Sprite sprite, float time)
    {
        var tween = CreateTween().SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(sprite, "position:y", sprite.Position.y - 15.0f, time);
        tween.Chain().TweenProperty(sprite, "position:y", sprite.Position.y, time);
        tween.SetLoops();
        tween.Play();
    }
}
