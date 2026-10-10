namespace _3DRenderer.structure;

public class Scene
{
    public Camera Camera { get; set; }
    public RenderObject[] Objects { get; set; }

    public Scene()
    {
        Camera = new Camera();
        Objects = [];
    }
}