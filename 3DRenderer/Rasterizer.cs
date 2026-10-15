namespace _3DRenderer;

public static class Rasterizer
{
    public static RenderTarget<Vector3> Render(Scene scene, RenderTarget<Vector3> target)
    {
        target.Clear();
        ClipScene clipScene = lib.SceneTransforms.TransformToClipSpace(scene);
        Rasterize(clipScene, target);
        return target;
    }


    public static RenderTarget<Vector3> Rasterize(ClipScene scene, RenderTarget<Vector3> target)
    {
        List<ClipVertex> allVertices = new List<ClipVertex>();
        List<int> allTriangleIndices = new List<int>();
        
        foreach (ClipRenderObject renderObject in scene.Objects)
        {
            int startIndex = allVertices.Count;
            
            foreach (var vertex in renderObject.Vertices )
            {
                allVertices.Add(vertex);
            }

            foreach (var index in renderObject.TriangleIndices)
            {
                allTriangleIndices.Add(index + startIndex);
            }
        }

        for (int i = 0; i < allTriangleIndices.Count - 2; i += 3)
        {
            ClipVertex vertexA = allVertices[allTriangleIndices[i]];
            ClipVertex vertexB = allVertices[allTriangleIndices[i + 1]];
            ClipVertex vertexC = allVertices[allTriangleIndices[i + 2]];

            
            
            ScreenVertex screenA = ToScreenVertex(vertexA, target.Width, target.Height);
            ScreenVertex screenB = ToScreenVertex(vertexB, target.Width, target.Height);
            ScreenVertex screenC = ToScreenVertex(vertexC, target.Width, target.Height);
            
            Vector3 color = new Vector3(vertexA.Position.X, vertexB.Position.Y, vertexC.Position.Z);
            
            for (int y = 0; y < target.Height; y++)
            {
                for (int x = 0; x < target.Width; x++)
                {
                    Vector2 pixelPosition = new Vector2(x, y);

                    if (PointInTriangle(
                            screenA.Position,
                            screenB.Position,
                            screenC.Position,
                            pixelPosition))
                    {
                        target.Buffer[x, y] = color;
                    }
                }
            }
        }
        return target;
    }
    
    public static bool TryPerspectiveDivide(ClipVertex vertex, out Vector3 ndc)
    {
        Vector4 position = vertex.Position;

        if (position.W <= 0.000001f)
        {
            ndc = Vector3.Zero;
            return false;
        }

        ndc = new Vector3(
            position.X / position.W,
            position.Y / position.W,
            position.Z / position.W
            );

        return true;
    }
    
    public static ScreenVertex ToScreenVertex(
        ClipVertex vertex,
        int width,
        int height)
    {
        Vector4 clip = vertex.Position;

        if (clip.W <= 0.000001f)
            throw new ArgumentException("Invalid clip-space position.");

        Vector3 ndc = new Vector3(
            clip.X / clip.W,
            clip.Y / clip.W,
            clip.Z / clip.W);

        Vector2 screen = ToScreenCoordinates(ndc, width, height);

        return new ScreenVertex(screen, ndc.Z);
    }
    
    public static Vector2 ToScreenCoordinates(Vector3 ndc, int width, int height)
    {
        return new Vector2(
            (ndc.X + 1.0f) * 0.5f * (width - 1),
            (1.0f - ndc.Y) * 0.5f * (height - 1)
            );
    }
    
    public static bool PointInTriangle(Vector2 vertexA, Vector2 vertexB, Vector2 vertexC, Vector2 pointP)
    {
        // check if point is on the same side of every line
        float d1 = EdgeFunction(vertexA, vertexB, pointP);
        float d2 = EdgeFunction(vertexB, vertexC, pointP);
        float d3 = EdgeFunction(vertexC, vertexA, pointP);

        bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(hasNeg && hasPos);
    }
        
    public static float EdgeFunction(Vector2 a, Vector2 b, Vector2 p)
    {
        return (p.X - a.X) * (b.Y - a.Y) - (p.Y - a.Y) * (b.X - a.X); // =signed determinant of 2x2 mat
    }
}