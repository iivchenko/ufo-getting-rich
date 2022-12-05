using Godot;

public class Island04 : IslandBase
{
    public override void _Ready()
    {
        base._Ready();

        var ground = GetNode<Sprite>("Ground/Sprite5");
        var tween = CreateTween().SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(ground, "position:y", ground.Position.y - 50.0f, 1.0f);
        tween.Chain().TweenProperty(ground, "position:y", ground.Position.y + 50.0f, 1.0f);
        tween.SetLoops();
        tween.Play();

        var key = GetNode<Sprite>("Ground/Key");
        var keyTween = CreateTween().SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
        keyTween.TweenProperty(key, "position:x", key.Position.x + 300.0f, 1.0f);
        keyTween.TweenProperty(key, "flip_h", true, 0.01f);
        keyTween.Chain().TweenProperty(key, "position:x", key.Position.x, 1.0f);
        keyTween.TweenProperty(key, "flip_h", false, 0.01f);
        keyTween.SetLoops();
        keyTween.Play();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);        
    }
}
