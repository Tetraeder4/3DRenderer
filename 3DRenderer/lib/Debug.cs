using Mesh = _3DRenderer.structure.Mesh;

namespace _3DRenderer.lib;

public static class Debug
{
    public static RenderObject CreateCube()
    {
        const float h = 0.5f;
    
        var vertices = new Vertex[]
        {
            // Front face (-Z)
            new(new Vector3(-h, -h, -h), -Vector3.UnitZ, new Vector2(0, 1)),
            new(new Vector3( h, -h, -h), -Vector3.UnitZ, new Vector2(1, 1)),
            new(new Vector3( h,  h, -h), -Vector3.UnitZ, new Vector2(1, 0)),
            new(new Vector3(-h,  h, -h), -Vector3.UnitZ, new Vector2(0, 0)),
    
            // Back face (+Z)
            new(new Vector3( h, -h,  h), Vector3.UnitZ, new Vector2(0, 1)),
            new(new Vector3(-h, -h,  h), Vector3.UnitZ, new Vector2(1, 1)),
            new(new Vector3(-h,  h,  h), Vector3.UnitZ, new Vector2(1, 0)),
            new(new Vector3( h,  h,  h), Vector3.UnitZ, new Vector2(0, 0)),
    
            // Left face (-X)
            new(new Vector3(-h, -h,  h), -Vector3.UnitX, new Vector2(0, 1)),
            new(new Vector3(-h, -h, -h), -Vector3.UnitX, new Vector2(1, 1)),
            new(new Vector3(-h,  h, -h), -Vector3.UnitX, new Vector2(1, 0)),
            new(new Vector3(-h,  h,  h), -Vector3.UnitX, new Vector2(0, 0)),
    
            // Right face (+X)
            new(new Vector3( h, -h, -h), Vector3.UnitX, new Vector2(0, 1)),
            new(new Vector3( h, -h,  h), Vector3.UnitX, new Vector2(1, 1)),
            new(new Vector3( h,  h,  h), Vector3.UnitX, new Vector2(1, 0)),
            new(new Vector3( h,  h, -h), Vector3.UnitX, new Vector2(0, 0)),
    
            // Top face (+Y)
            new(new Vector3(-h,  h, -h), Vector3.UnitY, new Vector2(0, 1)),
            new(new Vector3( h,  h, -h), Vector3.UnitY, new Vector2(1, 1)),
            new(new Vector3( h,  h,  h), Vector3.UnitY, new Vector2(1, 0)),
            new(new Vector3(-h,  h,  h), Vector3.UnitY, new Vector2(0, 0)),
    
            // Bottom face (-Y)
            new(new Vector3(-h, -h,  h), -Vector3.UnitY, new Vector2(0, 1)),
            new(new Vector3( h, -h,  h), -Vector3.UnitY, new Vector2(1, 1)),
            new(new Vector3( h, -h, -h), -Vector3.UnitY, new Vector2(1, 0)),
            new(new Vector3(-h, -h, -h), -Vector3.UnitY, new Vector2(0, 0))
        };
    
        var triangles = new int[]
        {
            0, 2, 1,
            0, 3, 2,

            4, 6, 5,
            4, 7, 6,

            8, 10, 9,
            8, 11, 10,

            12, 14, 13,
            12, 15, 14,

            16, 18, 17,
            16, 19, 18,

            20, 22, 21,
            20, 23, 22
        };
    
        var mesh = new Mesh(vertices, triangles);
    
        return new RenderObject(mesh);
    }
    
    
}