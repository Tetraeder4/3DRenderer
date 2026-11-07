namespace _3DRenderer.lib;

public static class Interpolate
{
    public static Vector2? PerspectiveCorrect(Vector2? a, Vector2? b, Vector2? c, Vector3 weights, Vector3 clipW)
    {
        return a == null || b == null || c == null ?
            null :
            PerspectiveCorrect(a.Value, b.Value, c.Value, weights, clipW);
    }
    public static Vector2 PerspectiveCorrect(Vector2 a, Vector2 b, Vector2 c, Vector3 weights, Vector3 clipW)
    {
        Vector3 reciprocalW = Vector3.One / clipW;
        
        float q = Vector3.Dot(weights, reciprocalW);
        
        return (weights.X * a * reciprocalW.X +
                weights.Y * b * reciprocalW.Y +
                weights.Z * c * reciprocalW.Z) 
               / q;
    }
    
    public static Vector3? PerspectiveCorrect(Vector3? a, Vector3? b, Vector3? c, Vector3 weights, Vector3 reciprocalW)
    {
        return a == null || b == null || c == null ?
            null :
            PerspectiveCorrect(a.Value, b.Value, c.Value, weights, reciprocalW);
    }
    
    public static Vector3 PerspectiveCorrect(Vector3 a, Vector3 b, Vector3 c, Vector3 weights, Vector3 reciprocalW)
    {
        float q = Vector3.Dot(weights, reciprocalW);
        
        return (weights.X * a * reciprocalW.X +
                weights.Y * b * reciprocalW.Y +
                weights.Z * c * reciprocalW.Z) 
               / q;
    }
    
    public static float Linear(float a, float b, float c, Vector3 weights) // affine
    {
        return Vector3.Dot(new Vector3(a, b, c), weights );
    }
}
