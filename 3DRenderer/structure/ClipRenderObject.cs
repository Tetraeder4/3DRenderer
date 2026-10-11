namespace _3DRenderer.structure;

public class ClipRenderObject
{
    public ClipVertex[] Vertices { get; }
    public int[] TriangleIndices { get; }

    public ClipRenderObject(ClipVertex[] vertices, int[] triangleIndices)
    {
        Vertices = vertices;
        TriangleIndices = triangleIndices;
    }
}