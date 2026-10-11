namespace _3DRenderer.structure;

public sealed class ClipScene
{
    public ClipRenderObject[] Objects { get; }

    public ClipScene(ClipRenderObject[] objects)
    {
        Objects = objects;
    }
}