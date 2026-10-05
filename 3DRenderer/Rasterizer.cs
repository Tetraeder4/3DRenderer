namespace _3DRenderer;

public static class Rasterizer
{
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