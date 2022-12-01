using Godot;

public class PngSave : ViewportContainer
{
    [Export]
    public string PngPath { get;set; }

    public override async void _Ready()
    {
        await ToSignal(VisualServer.Singleton, "frame_post_draw");
        var img = GetNode<Viewport>("Viewport").GetTexture().GetData();
        img.FlipY();

        img.SavePng(PngPath);
    }
}
