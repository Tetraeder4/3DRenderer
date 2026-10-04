namespace _3DRenderer.structure;

public class Mesh
{
    public Vertex[] Vertices { get; set; }
    public int[] TriangleIndices { get; set; }

    public Mesh(Vertex[] vertices, int[] triangleIndices)
    {
        Vertices = vertices;
        TriangleIndices = triangleIndices;
    }
}