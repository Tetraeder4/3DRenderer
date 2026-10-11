namespace _3DRenderer.structure;

public sealed class Mesh
{
    private readonly Vertex[] _vertices;
    private readonly int[] _triangleIndices;

    public ReadOnlySpan<Vertex> Vertices => _vertices;
    public ReadOnlySpan<int> TriangleIndices => _triangleIndices;

    public Mesh(Vertex[] vertices, int[] triangleIndices)
    {
        ArgumentNullException.ThrowIfNull(vertices);
        ArgumentNullException.ThrowIfNull(triangleIndices);

        _vertices = (Vertex[])vertices.Clone();
        _triangleIndices = (int[])triangleIndices.Clone();

        Validate();
    }

    private void Validate()
    {
        if (_triangleIndices.Length % 3 != 0)
            throw new ArgumentException("Triangle indices must be provided in groups of three.");

        foreach (int index in _triangleIndices)
        {
            if (index < 0 || index >= _vertices.Length)
                throw new ArgumentOutOfRangeException(nameof(TriangleIndices));
        }
    }
}