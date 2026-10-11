namespace _3DRenderer.structure;

public class Scene
{
    public Camera Camera { get; set; }
    public List<RenderObject> Objects { get; set; }

    public Scene()
    {
        Camera = new Camera();
        Objects = [];
    }

    public void Update(float deltaTime)
    {
        Camera.Update(deltaTime);

        foreach (RenderObject renderObject in Objects)
            renderObject.Update(deltaTime);
    }
}