namespace _3DRenderer.structure;

public struct Vertex
{
    public readonly Vector3 Position;
    public readonly Vector3? Normal;
    public readonly Vector2? TexCoord;
    public Vertex(Vector3 position, Vector3? normal, Vector2? texCoord)
    {
        Position = position;
        Normal = normal;
        TexCoord = texCoord;
    }
}

