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
        List<ClipVertex> allVertices = new ();
        List<int> allTriangleIndices = new ();
        
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
            
            ScreenVertex screenA = SceneTransforms.ToScreenVertex(vertexA, target.Width, target.Height);
            ScreenVertex screenB = SceneTransforms.ToScreenVertex(vertexB, target.Width, target.Height);
            ScreenVertex screenC = SceneTransforms.ToScreenVertex(vertexC, target.Width, target.Height);

            Vector3 color = Debug.GetRandomColor(i);
            
            for (int y = 0; y < target.Height; y++)
            {
                for (int x = 0; x < target.Width; x++)
                {
                    if (PointInTriangle(screenA.Position, screenB.Position, screenC.Position, new Vector2(x, y), out Vector3 barycentricWeights))
                    {
                        //interpolate Vertex Depths
                        Vector3 depths = new (screenA.Depth, screenB.Depth, screenC.Depth);
                        float pixelDepth = Vector3.Dot(depths, barycentricWeights);
                        
                        if (pixelDepth > target.DepthBuffer[x, y]) continue;

                        target.DepthBuffer[x, y] =  pixelDepth;
                        target.ColorBuffer[x, y] = color;
                    }
                }
            }
        }
        return target;
    }
    
    public static bool PointInTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 p, out Vector3 barycentricCoords)
    {
        // check if point is on the same side of every line
        float areaAbp = EdgeFunction(a, b, p);
        float areaBcp = EdgeFunction(b, c, p);
        float areaCap = EdgeFunction(c, a, p);

        bool hasNeg = (areaAbp < 0) || (areaBcp < 0) || (areaCap < 0);
        bool hasPos = (areaAbp > 0) || (areaBcp > 0) || (areaCap > 0);
        
        //calculate normalized baricentric coordinates
        float totalArea = areaAbp + areaBcp + areaCap;
        
        if (totalArea == 0)
        {
            barycentricCoords = Vector3.Zero;
            return false;
        }
        
        barycentricCoords = new(
            areaBcp / totalArea,
            areaCap / totalArea,
            areaAbp / totalArea
            );
        
        return !(hasNeg && hasPos);
    }
        
    public static float EdgeFunction(Vector2 a, Vector2 b, Vector2 p)
    {
        return (p.X - a.X) * (b.Y - a.Y) - (p.Y - a.Y) * (b.X - a.X); // =signed determinant of 2x2 mat
    }
}