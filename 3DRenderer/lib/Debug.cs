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
            new(new Vector3(h, -h, -h), -Vector3.UnitZ, new Vector2(1, 1)),
            new(new Vector3(h, h, -h), -Vector3.UnitZ, new Vector2(1, 0)),
            new(new Vector3(-h, h, -h), -Vector3.UnitZ, new Vector2(0, 0)),

            // Back face (+Z)
            new(new Vector3(h, -h, h), Vector3.UnitZ, new Vector2(0, 1)),
            new(new Vector3(-h, -h, h), Vector3.UnitZ, new Vector2(1, 1)),
            new(new Vector3(-h, h, h), Vector3.UnitZ, new Vector2(1, 0)),
            new(new Vector3(h, h, h), Vector3.UnitZ, new Vector2(0, 0)),

            // Left face (-X)
            new(new Vector3(-h, -h, h), -Vector3.UnitX, new Vector2(0, 1)),
            new(new Vector3(-h, -h, -h), -Vector3.UnitX, new Vector2(1, 1)),
            new(new Vector3(-h, h, -h), -Vector3.UnitX, new Vector2(1, 0)),
            new(new Vector3(-h, h, h), -Vector3.UnitX, new Vector2(0, 0)),

            // Right face (+X)
            new(new Vector3(h, -h, -h), Vector3.UnitX, new Vector2(0, 1)),
            new(new Vector3(h, -h, h), Vector3.UnitX, new Vector2(1, 1)),
            new(new Vector3(h, h, h), Vector3.UnitX, new Vector2(1, 0)),
            new(new Vector3(h, h, -h), Vector3.UnitX, new Vector2(0, 0)),

            // Top face (+Y)
            new(new Vector3(-h, h, -h), Vector3.UnitY, new Vector2(0, 1)),
            new(new Vector3(h, h, -h), Vector3.UnitY, new Vector2(1, 1)),
            new(new Vector3(h, h, h), Vector3.UnitY, new Vector2(1, 0)),
            new(new Vector3(-h, h, h), Vector3.UnitY, new Vector2(0, 0)),

            // Bottom face (-Y)
            new(new Vector3(-h, -h, h), -Vector3.UnitY, new Vector2(0, 1)),
            new(new Vector3(h, -h, h), -Vector3.UnitY, new Vector2(1, 1)),
            new(new Vector3(h, -h, -h), -Vector3.UnitY, new Vector2(1, 0)),
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

    public static Vector3 GetRandomColor(int index)
    {
        uint hash = (uint)index;

        hash ^= hash >> 16;
        hash *= 0x7feb352d;
        hash ^= hash >> 15;
        hash *= 0x846ca68b;
        hash ^= hash >> 16;

        float r = (hash & 0xFF) / 255f;
        float g = ((hash >> 8) & 0xFF) / 255f;
        float b = ((hash >> 16) & 0xFF) / 255f;

        r = (r + 0.5f) / 1.5f;
        g = (g + 0.5f) / 1.5f;
        b = (b + 0.5f) / 1.5f;

        return new Vector3(r, g, b);
    }

    public static RenderTarget<bool> CheckerboardTestTarget(int w, int h)
    {
        var target = new RenderTarget<bool>(800, 600);

        for (int y = 0; y < target.Height; y++)
        {
            for (int x = 0; x < target.Width; x++)
            {
                target.ColorBuffer[x, y] = (x + y) % 2 == 0;
            }
        }

        return target;
    }

    public static RenderTarget<Vector3> HelloTriangleTestTarget(int w, int h)
    {
        var target = new RenderTarget<Vector3>(100, 100);
        
        Vector2 a = new Vector2(0.5f * (target.Width - 1), 1f * (target.Height - 1));
        Vector2 b = new Vector2(1f * (target.Width - 1), 0);
        Vector2 c = new Vector2(0f * (target.Width - 1), 0);
        
        for (int y = 0; y < target.Height; y++)
        {
            for (int x = 0; x < target.Width; x++)
            {
                if (Rasterizer.PointInTriangle(a, b, c, new Vector2(x,y), out _ ))
                { 
                    target.ColorBuffer[x, y] = new Vector3(
                        (float)x / target.Width,
                        (float)y / target.Height,
                        0.5f
                    );   
                }
                
            }
        }

        return target;
    }
}
