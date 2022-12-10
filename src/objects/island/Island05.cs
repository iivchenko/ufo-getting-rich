using Godot;

public class Island05 : IslandBase
{
    public override void _Ready()
    {
        base._Ready();

        TweenFlag();
        TweenFlower();
        TweenMouse();
    }

    public override void _Process(float delta)
    {
        base._Process(delta);        
    }

    private void TweenFlag()
    {
        var flagFrame1 = GetNode<Sprite>("Flag/Frame1");
        var flagFrame2 = GetNode<Sprite>("Flag/Frame2");
        var time = 0.15f;
        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear);
        tween.TweenProperty(flagFrame1, "visible", false, time);
        tween.Parallel().TweenProperty(flagFrame2, "visible", true, time);
        tween.TweenProperty(flagFrame1, "visible", true, time);
        tween.Parallel().TweenProperty(flagFrame2, "visible", false, time);
        tween.SetLoops();
        tween.Play();
    }

    private void TweenFlower()
    {
        var flagFrame1 = GetNode<Sprite>("Flower/Frame1");
        var flagFrame2 = GetNode<Sprite>("Flower/Frame2");
        var time = 1.0f;
        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear);
        tween.TweenProperty(flagFrame1, "visible", false, time);
        tween.Parallel().TweenProperty(flagFrame2, "visible", true, time);
        tween.TweenProperty(flagFrame1, "visible", true, time);
        tween.Parallel().TweenProperty(flagFrame2, "visible", false, time);
        tween.SetLoops();
        tween.Play();
    }

    private void TweenMouse()
    {
        var mouse = GetNode<Sprite>("Ground/Mouse");
        var tween = CreateTween().SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
        tween.TweenProperty(mouse.Material, "shader_param/deformation", new Vector2(0.1f, 0f), 0.7f);
        tween.Chain().TweenProperty(mouse.Material, "shader_param/deformation", new Vector2(-0.1f, 0f), 0.7f);
        tween.SetLoops();
        tween.Play();
    }
}
