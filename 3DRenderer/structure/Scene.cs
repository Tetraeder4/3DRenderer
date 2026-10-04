namespace _3DRenderer.structure;

public class Scene
{
    public Camera Camera { get; set; }
    public Object[]? Objects { get; set; }

    public Scene()
    {
        Camera = new Camera();
        Objects = null;
    }
}