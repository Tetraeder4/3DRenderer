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
            // Front
             0,  1,  2,
             0,  2,  3,
    
            // Back
             4,  5,  6,
             4,  6,  7,
    
            // Left
             8,  9, 10,
             8, 10, 11,
    
            // Right
            12, 13, 14,
            12, 14, 15,
    
            // Top
            16, 17, 18,
            16, 18, 19,
    
            // Bottom
            20, 21, 22,
            20, 22, 23
        };
    
        var mesh = new Mesh(vertices, triangles);
    
        return new RenderObject(mesh);
    }
    
    
}