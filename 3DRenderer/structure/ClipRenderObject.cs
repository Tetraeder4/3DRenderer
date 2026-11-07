namespace _3DRenderer.structure;

public class ClipRenderObject
{
    public shading.Shader Shader { get; }
    public ClipVertex[] Vertices { get; }
    public int[] TriangleIndices { get; }

    public ClipRenderObject(ClipVertex[] vertices, int[] triangleIndices, shading.Shader shader)
    {
        Shader = shader;
        Vertices = vertices;
        TriangleIndices = triangleIndices;
    }
}